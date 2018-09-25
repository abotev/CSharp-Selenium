using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriver
{
    public class TestData
    {
        public TestData()
        {
            this.Email = "emp2@fluxday.io";
            this.Password = "password";
            this.DefaultPassword = "password";
        }
        public string Email { get; set; }

        public string Password { get; set; }

        public string DefaultPassword { get; set; }
    }
}
