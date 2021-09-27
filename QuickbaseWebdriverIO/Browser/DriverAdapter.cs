using System;
using OpenQA.Selenium;
using QuickbaseWebdriverIO.Elements;
using QuickbaseWebdriverIO.Interfaces;

namespace QuickbaseWebdriverIO.Browser
{
    public class DriverAdapter : IDriver
    {
        public DriverAdapter()
        {
        }

        public DriverAdapter(IWebDriver driver)
        {
            NativeDriver = driver;
        }

        public IWebDriver NativeDriver { get; }

        public string Title => NativeDriver.Title;

        public IAlert Alert => NativeDriver.SwitchTo().Alert();

        public Uri Url
        {
            get => new(NativeDriver.Url);
            set => NativeDriver.Url = value.ToString();
        }

        public void Back()
        {
            NativeDriver.Navigate().Back();
        }

        public void GoToUrl(Uri url)
        {
            NativeDriver.Navigate().GoToUrl(url);
        }

        public IElement CreateElement(By locator)
        {
            return new Element(NativeDriver, locator);
        }

        public IElement CreateElement(IWebElement element)
        {
            return new Element(NativeDriver, element);
        }

        public IElementsList CreateElements(By locator)
        {
            return new ElementsList(NativeDriver, locator);
        }

        public void MaximizeScreen()
        {
            NativeDriver.Manage().Window.Maximize();
        }

        public void Refresh()
        {
            NativeDriver.Navigate().Refresh();
        }

        public void Close()
        {
            NativeDriver.Quit();
            NativeDriver.Dispose();
        }

        public void TakeScreenshot(string path)
        {
            Screenshot image = ((ITakesScreenshot)NativeDriver).GetScreenshot();
            image.SaveAsFile(path);
        }

        public string ExecuteJavaScript(string scriptToInvoke, params object[] elements)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)NativeDriver;
            return js.ExecuteScript(scriptToInvoke, elements)?.ToString();
        }
    }
}
