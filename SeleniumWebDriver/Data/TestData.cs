using SeleniumWebDriver.Data.Models;

namespace SeleniumWebDriver.Data
{
    public static class TestData
    {
        public static User Employee2
        {
            get
            {
                return new User("emp2@fluxday.io", "password", "Employee 2", "Emp2");
            }
        }
    }
}
