using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QuickbaseWebdriverIO.Models;

namespace QuickbaseWebdriverIO.Services
{
    public class ElementFinderService
    {
        private readonly WebDriverWait _webDriverWait;

        public ElementFinderService(IWebDriver driver)
        {
            var timeout = TimeSpan.FromMilliseconds(ConfigurationService.GetSection<TimeoutSettings>().GeneralTimeout);
            var sleepInterval = TimeSpan.FromMilliseconds(ConfigurationService.GetSection<TimeoutSettings>().SleepInterval);
            _webDriverWait = new WebDriverWait(new SystemClock(), driver, timeout, sleepInterval);
        }

        public IWebElement Find(By by)
        {
            return _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
        }

        public IEnumerable<IWebElement> FindAll(By by)
        {
            try
            {
                return _webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
            }
            catch (WebDriverTimeoutException)
            {
                return new List<IWebElement>();
            }
        }
    }
}
