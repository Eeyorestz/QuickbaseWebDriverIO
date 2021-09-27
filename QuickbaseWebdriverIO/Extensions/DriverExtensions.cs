using System;
using OpenQA.Selenium.Support.UI;
using QuickbaseWebdriverIO.Interfaces;
using QuickbaseWebdriverIO.Models;
using QuickbaseWebdriverIO.Services;

namespace QuickbaseWebdriverIO.Extensions
{
    public static class DriverExtensions
    {
        public static void WaitForLoadedPage(this IDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver.NativeDriver, TimeSpan.FromMilliseconds(ConfigurationService.GetSection<TimeoutSettings>().GeneralTimeout));
            wait.Until(wd => driver.ExecuteJavaScript("return document.readyState") == "complete");
        }
    }
}
