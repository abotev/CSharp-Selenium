namespace SeleniumWebDriver
{
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class TestBase
    {
        public TestBase()
        {
            this.Email = "emp2@fluxday.io";
            this.Password = "password";
            this.Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://app.fluxday.io/users/sign_in");
        }

        protected IWebDriver Driver { get; set; }

        protected string Email { get; set; }

        protected string Password { get; set; }

        protected void Login(string email, string password)
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
            passwordTextBox.SendKeys(this.Password);

            IWebElement confirmPasswordTextBox = Driver.FindElement(By.Id("user_password_confirmation"));
            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys(this.Password);

            IWebElement saveButton = Driver.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();
        }
    }

    [TestClass]
    public class FluxdayTests : TestBase
    {
        [TestInitialize]
        public void TestSetup()
        {
            Login(this.Email, this.Password);
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
            var expectedResult = "Employee 2";
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCategory("FluxdayTests")]
        [TestMethod]
        public void Test002ChangeAccountPassword()
        {
            string newPassword = "password123";
            Driver.Navigate().GoToUrl("https://app.fluxday.io/users/FT4#");
            IWebElement dropDownListBox = Driver.FindElement(By.XPath("//*[@id=\"pane3\"]/div/div[1]/div[2]/a"));
            dropDownListBox.Click();
            IWebElement changePasswordOption = Driver.FindElement(By.XPath("//*[@id=\"drop1\"]/li[2]/a"));
            changePasswordOption.Click();
            Thread.Sleep(1000);

            IWebElement passwordTextBox = Driver.FindElement(By.Id("user_password"));
            passwordTextBox.Clear();
            passwordTextBox.SendKeys(newPassword);

            IWebElement confirmPasswordTextBox = Driver.FindElement(By.Id("user_password_confirmation"));
            confirmPasswordTextBox.Clear();
            confirmPasswordTextBox.SendKeys(newPassword);

            IWebElement saveButton = Driver.FindElement(By.XPath("//*[@id=\"edit_user_33\"]/div[3]/div[2]/input"));
            saveButton.Click();

            Login(this.Email, newPassword);

            IWebElement username = Driver.FindElement(By.CssSelector("body > div.row.ep-tracker > div.large-2.columns.pane1.show-for-large-up > ul.user-links.side-nav.sidebar-links > li:nth-child(1) > a"));
            var actualResult = username.Text;
            Assert.IsTrue(actualResult.Contains("Employee 2"));
            ResetPasswordToDefault();
        }
    }
}
