namespace SeleniumWebDriver.Tests
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using SeleniumWebDriver.Core;
    using SeleniumWebDriver.Data;

    [TestClass]
    public class FluxdayTests : BaseTest
    {
        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test001LogInAsAnEmployee2()
        {
            var employeeUser = TestData.Employee2;
            Login(employeeUser.Email, employeeUser.Password);

            IWebElement username = Driver.Browser.FindElement(By.CssSelector("body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(1) > a"));
            var actualResult = username.Text;
            Assert.IsTrue(actualResult.Contains(employeeUser.Name));
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test002ChangeAccountPassword()
        {
            var employeeUser = TestData.Employee2;
            Login(employeeUser.Email, employeeUser.Password);

            string currentPassword = employeeUser.Password;
            string newPassword = "password123";
            employeeUser.Password = newPassword
                ;
            Driver.Browser.Navigate().GoToUrl("https://app.fluxday.io/users/FT4#");
            IWebElement dropDownListBox = Driver.Browser.FindElement(By.XPath("//*[@id=\"pane3\"]/div/div[1]/div[2]/a"));
            dropDownListBox.Click();
            IWebElement changePasswordOption = Driver.Browser.FindElement(By.XPath("//*[@id=\"drop1\"]/li[2]/a"));
            changePasswordOption.Click();
            Thread.Sleep(1000);

            IWebElement passwordTextBox = Driver.Browser.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(employeeUser.Password);

            IWebElement confirmPasswordTextBox = Driver.Browser.FindElement(By.Id("user_password_confirmation"));
            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys(employeeUser.Password);

            IWebElement saveButton = Driver.Browser.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();
            Thread.Sleep(1000);

            Login(employeeUser.Email, employeeUser.Password);

            IWebElement username = Driver.Browser.FindElement(By.CssSelector("body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(1) > a"));
            var actualResult = username.Text;
            Assert.IsTrue(actualResult.Contains(employeeUser.Name));
            ResetPasswordToDefault(currentPassword);
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test003ViewDetailsAboutACompletedTask()
        {
            var employeeUser = TestData.Employee2;
            Login(employeeUser.Email, employeeUser.Password);

            Driver.Browser.Navigate().GoToUrl("https://app.fluxday.io/tasks#pane2");
            IWebElement completedTasks = Driver.Browser.FindElement(By.XPath("//*[@id=\"pane2\"]/div[1]/dl/dd[2]/a"));
            completedTasks.Click();
            Thread.Sleep(1000);

            IWebElement task = Driver.Browser.FindElement(By.XPath("//*[@id=\"paginator\"]/a[1]/div/div[1]"));
            task.Click();
            Thread.Sleep(1000);

            IWebElement taskTitle = Driver.Browser.FindElement(By.XPath("//*[@id=\"pane3\"]/div[2]/div/div[1]/div[2]"));
            var actualResult = taskTitle.Text;
            Assert.IsTrue(actualResult.Contains("Announcement"));
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test004ChangeNickname()
        {
            var employeeUser = TestData.Employee2;
            Login(employeeUser.Email, employeeUser.Password);
            Driver.Browser.Navigate().GoToUrl("https://app.fluxday.io/users/FT4#");

            IWebElement dropDownListIcon = Driver.Browser.FindElement(By.XPath("//*[@id=\"pane3\"]/div/div[1]/div[2]/a"));
            dropDownListIcon.Click();

            IWebElement editOption = Driver.Browser.FindElement(By.XPath("//*[@id=\"drop1\"]/li[1]/a"));
            editOption.Click();
            Thread.Sleep(1000);

            string newNickname = "Employee";
            IWebElement nicknameTextBox = Driver.Browser.FindElement(By.Id("user_nickname"));
            nicknameTextBox.Clear();
            nicknameTextBox.SendKeys(newNickname);

            IWebElement saveButton = Driver.Browser.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();
            Thread.Sleep(500);

            IWebElement nickname = Driver.Browser.FindElement(By.XPath("//*[@id=\"pane3\"]/div/div[1]/div[4]/div/div"));
            var actualResult = nickname.Text.Substring(1, nickname.Text.Length - 2);
            Assert.IsTrue(actualResult.Contains(newNickname));
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test005SearchByAKeyWord()
        {
            var employeeUser = TestData.Employee2;
            Login(employeeUser.Email, employeeUser.Password);
            string keyword = "team";
            IWebElement searchTextBox = Driver.Browser.FindElement(By.Id("search_keyword"));
            searchTextBox.Clear();
            searchTextBox.SendKeys(keyword);
            searchTextBox.Submit();

            Thread.Sleep(1000);
            IWebElement resultTitle = Driver.Browser.FindElement(By.XPath("//*[@id=\"pane2\"]/div/div[1]/div"));
            var actualResult = resultTitle.Text;
            var expectedResult = "Results for team";
            Assert.AreEqual(expectedResult, actualResult);
        }

        public void Login(string email, string password)
        {
            IWebElement emailTextBox = Driver.Browser.FindElement(By.Id("user_email"));
            emailTextBox.Clear();
            emailTextBox.SendKeys(email);

            IWebElement passwordTextBox = Driver.Browser.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(password);

            IWebElement loginButton = Driver.Browser.FindElement(By.XPath("//*[@id=\"new_user\"]/div[2]/div[3]/button"));
            loginButton.Click();
            Thread.Sleep(1000);
        }

        protected void ResetPasswordToDefault(string defaultPassword)
        {
            Driver.Browser.Navigate().GoToUrl("https://app.fluxday.io/users/change_password#pane3");
            Thread.Sleep(1000);
            IWebElement passwordTextBox = Driver.Browser.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(defaultPassword);

            IWebElement confirmPasswordTextBox = Driver.Browser.FindElement(By.Id("user_password_confirmation"));
            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys(defaultPassword);

            IWebElement saveButton = Driver.Browser.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();
            Thread.Sleep(1000);
        }
    }
}
