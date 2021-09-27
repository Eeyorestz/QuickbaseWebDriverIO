using System.Drawing;
using OpenQA.Selenium;

namespace QuickbaseWebdriverIO.Interfaces
{
    public interface IElement
    {
        By By { get; }
       // ElementActions ElementActions { get; }
        Point ElementCoordinates { get; }
        bool ElementIsChecked { get; }
        bool? Enabled { get; }
        int Height { get; }
        string InnerHtml { get; }
        string InnerText { get; }
        bool IsDisplayed { get; }
        bool? IsRequired { get; }
        IWebDriver NativeDriver { get; }
        IWebElement NativeElement { get; }
        string Text { get; }
        string Value { get; }
        int Width { get; }

        void Check();
        void ClearElementWithJavaScript();
        void Click();
        IElement CreateElement(By locator);
        IElementsList CreateElements(By locator);
        void Focus();
        string GetAttribute(string attributeName);
        string GetSelectedOption();
        bool IsActiveElement();
        void JSClick();
        void RemoveHiddenFromElement();
        void SelectOptionByText(string selectOption);
        void SelectOptionByValue(string selectOption);
        void TypeText(string text);
        void Uncheck();
    }
}
