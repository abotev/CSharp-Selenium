namespace SeleniumWebDriver.Core
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using System;

    public static class Driver
    {
        private static IWebDriver browser;

        public static IWebDriver Browser
        {
            get
            {
                if (browser == null)
                {
                    throw new NullReferenceException("WebDriver is not Started!");
                }

                return browser;
            }

            private set
            {
                browser = value;
            }
        }

        public static void StartBrowser(BrowserTypes browserType = BrowserTypes.Chrome)
        {
            switch (browserType)
            {
                case BrowserTypes.Chrome:
                    Browser = new ChromeDriver();
                    break;
                case BrowserTypes.Firefox:
                    Browser = new FirefoxDriver();
                    break;
                default:
                    break;
            }

            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("https://app.fluxday.io/users/sign_in");
        }

        public static void StopBrowser()
        {
            Browser.Quit();
            Browser = null;
        }
    }
}
