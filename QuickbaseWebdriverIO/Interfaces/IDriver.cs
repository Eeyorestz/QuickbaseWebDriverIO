using System;
using OpenQA.Selenium;

namespace QuickbaseWebdriverIO.Interfaces
{
    public interface IDriver
    {
        IAlert Alert { get; }
        IWebDriver NativeDriver { get; }
        string Title { get; }
        Uri Url { get; set; }

        void Back();
        void Close();
        IElement CreateElement(By locator);
        IElementsList CreateElements(By locator);
        string ExecuteJavaScript(string scriptToInvoke, params object[] elements);
        void GoToUrl(Uri url);
        void MaximizeScreen();
        void Refresh();
        void TakeScreenshot(string path);
    }
}
