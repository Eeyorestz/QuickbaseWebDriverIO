using OpenQA.Selenium;

namespace QuickbaseWebdriverIO.Interfaces
{
    public interface IElement
    {
        By By { get; }
        string InnerHtml { get; }
        string InnerText { get; }
        bool IsDisplayed { get; }
        IWebDriver NativeDriver { get; }
        IWebElement NativeElement { get; }
        string Text { get; }  
        void Click();
        IElement CreateElement(By locator);
        IElementsList CreateElements(By locator);
        string GetAttribute(string attributeName);  
        void TypeText(string text);
    }
}
