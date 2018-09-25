namespace SeleniumWebDriver
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    [TestClass]
    public class FluxdayTests
    {
        public TestData data = new TestData();

        public static IWebDriver Driver { get; set; }

        [TestInitialize]
        public void TestSetup()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://app.fluxday.io/users/sign_in");
            Login(data.Email, data.Password);
        }

        [TestCleanup]
        public void TestTeardown()
        {
            Driver.Quit();
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test001LogInAsAnEmployee2()
        {
            IWebElement username = Driver.FindElement(By.CssSelector("body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(1) > a"));
            var actualResult = username.Text;
            Assert.IsTrue(actualResult.Contains("Employee 2"));
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test002ChangeAccountPassword()
        {
            data.Password = "password123";
            Driver.Navigate().GoToUrl("https://app.fluxday.io/users/FT4#");
            IWebElement dropDownListBox = Driver.FindElement(By.XPath("//*[@id=\"pane3\"]/div/div[1]/div[2]/a"));
            dropDownListBox.Click();
            IWebElement changePasswordOption = Driver.FindElement(By.XPath("//*[@id=\"drop1\"]/li[2]/a"));
            changePasswordOption.Click();
            Thread.Sleep(1000);

            IWebElement passwordTextBox = Driver.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(data.Password);

            IWebElement confirmPasswordTextBox = Driver.FindElement(By.Id("user_password_confirmation"));
            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys(data.Password);

            IWebElement saveButton = Driver.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();

            Login(data.Email, data.Password);

            IWebElement username = Driver.FindElement(By.CssSelector("body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(1) > a"));
            var actualResult = username.Text;
            Assert.IsTrue(actualResult.Contains("Employee 2"));
            ResetPasswordToDefault();
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test005SearchByAKeyWord()
        {
            string keyword = "team";
            IWebElement searchTextBox = Driver.FindElement(By.Id("search_keyword"));
            searchTextBox.Clear();
            searchTextBox.SendKeys(keyword);
            searchTextBox.Submit();
            IWebElement resultTitle = Driver.FindElement(By.XPath("//*[@id=\"pane2\"]/div/div[1]/div"));
            var actualResult = resultTitle.Text;
            var expectedResult = "Results for team";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test003ViewInformationAboutASpecificTeam()
        {

        }

        public void Login(string email, string password)
        {
            IWebElement emailTextBox = Driver.FindElement(By.Id("user_email"));
            emailTextBox.Clear();
            emailTextBox.SendKeys(email);

            IWebElement passwordTextBox = Driver.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(password);

            IWebElement loginButton = Driver.FindElement(By.XPath("//*[@id=\"new_user\"]/div[2]/div[3]/button"));
            loginButton.Click();
            Thread.Sleep(500);
        }

        protected void ResetPasswordToDefault()
        {
            Driver.Navigate().GoToUrl("https://app.fluxday.io/users/change_password#pane3");
            Thread.Sleep(1000);
            IWebElement passwordTextBox = Driver.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(data.DefaultPassword);

            IWebElement confirmPasswordTextBox = Driver.FindElement(By.Id("user_password_confirmation"));
            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys(data.DefaultPassword);

            IWebElement saveButton = Driver.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();
        }
    }
}
