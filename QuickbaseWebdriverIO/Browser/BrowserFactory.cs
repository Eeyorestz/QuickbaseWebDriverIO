using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using QuickbaseWebdriverIO.Enums;
using QuickbaseWebdriverIO.Interfaces;
using QuickbaseWebdriverIO.Models;
using QuickbaseWebdriverIO.Services;

namespace QuickbaseWebdriverIO.Browser
{
    public class BrowserFactory
    {
        private static readonly ThreadLocal<IDriver> ThreadSafeWebDriver = new ThreadLocal<IDriver>();

        public IDriver Driver
        {
            set => ThreadSafeWebDriver.Value = value;
            get
            {
                return ThreadSafeWebDriver.Value;
            }
        }

        public void InitDriver(BrowserType browserType)
        {
            if (ThreadSafeWebDriver == null)
            {
                return;
            }

            ThreadSafeWebDriver.Value = InitBrowserLocally(browserType);
        }

        private ChromeOptions ChromeOptions()
        {
            var chromeOptions = new ChromeOptions();
            if (ConfigurationService.GetSection<HeadlessSettings>().IsEnabled)
            {
                chromeOptions = (ChromeOptions)ChangeToHeadless(chromeOptions);
            }

            chromeOptions.AddArguments("--disable-web-security");
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArguments("--allow-running-insecure-content");

            return chromeOptions;
        }


        protected internal IDriver InitBrowserLocally(BrowserType browser)
        {
            IDriver browserDriver;

            switch (browser)
            {
                case BrowserType.Chrome:
                    browserDriver = new DriverAdapter(new ChromeDriver(ChromeOptions()));
                    break;
             
                default:
                    browserDriver = new DriverAdapter(new ChromeDriver(ChromeOptions()));
                    break;
            }

            browserDriver.MaximizeScreen();

            return browserDriver;
        }
     
        private static ChromiumOptions ChangeToHeadless(ChromiumOptions options)
        {
            var resolution = ConfigurationService.GetSection<HeadlessSettings>().ScreenshotResolution;
            options.AddArguments("--headless");
            options.AddArguments($"--window-size={resolution}");

            return options;
        }
    }

}
