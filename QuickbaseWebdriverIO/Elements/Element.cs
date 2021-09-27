using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using QuickbaseWebdriverIO.Interfaces;
using QuickbaseWebdriverIO.Models;
using QuickbaseWebdriverIO.Services;

namespace QuickbaseWebdriverIO.Elements
{
    public class Element : IElement
    {
        private readonly IJavaScriptExecutor _js;
        private readonly ElementFinderService _elementFinder;
        private readonly Element _parent;
        private IWebElement _nativeElement;

        public Element(IWebDriver driver, Element parent = null)
        {
            NativeDriver = driver;
            _elementFinder = new ElementFinderService(driver);
            _js = (IJavaScriptExecutor)NativeDriver;
            _parent = parent;
        }

        public Element(IWebDriver driver, By by, Element parent = null)
            : this(driver, parent)
        {
            By = by;
        }

        public Element(IWebDriver driver, IWebElement element)
            : this(driver)
        {
            _nativeElement = element;
        }

        public IWebElement NativeElement
        {
            get
            {
                if (_nativeElement is null)
                {
                    if (_parent is not null)
                    {
                        _nativeElement = _parent.NativeElement.FindElement(By);
                    }
                    else
                    {
                        _nativeElement = _elementFinder.Find(By);
                    }
                }

                return _nativeElement;
            }
        }

        public By By { get; }

        public string Value => NativeElement?.GetAttribute("value");

        public string Text => NativeElement?.Text;

        public bool? Enabled => NativeElement?.Enabled;

        public bool? IsRequired => Convert.ToBoolean(NativeElement?.GetAttribute("required"));

        public string InnerText => GetAttribute("innerText");

        public string InnerHtml => GetAttribute("innerHTML");

        public int Width => NativeElement.Size.Width;

        public int Height => NativeElement.Size.Height;

        public Point ElementCoordinates => NativeElement.Location;

        public bool ElementIsChecked => (bool)_js.ExecuteScript("return arguments[0].checked;", NativeElement);

        public bool IsDisplayed
        {
            get
            {
                WebDriverWait wait = new(NativeDriver, TimeSpan.FromMilliseconds(ConfigurationService.GetSection<TimeoutSettings>().GeneralTimeout));
                try
                {
                    wait.Until(d => NativeElement.Displayed);
                }
                catch (Exception ex)
                {
                    if (ex is WebDriverTimeoutException || ex is NoSuchElementException)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public IWebDriver NativeDriver { get; }

        Point IElement.ElementCoordinates => throw new NotImplementedException();

        public void Focus()
        {
            _js.ExecuteScript("arguments[0].focus();", NativeElement);
        }

        public bool IsActiveElement()
        {
            return NativeElement.Equals(NativeDriver.SwitchTo().ActiveElement());
        }

        public string GetSelectedOption()
        {
            var select = new SelectElement(NativeElement);
            try
            {
                return select.SelectedOption.Text;
            }
            catch (NoSuchElementException e)
            {
                if (e.Message == "No option is selected")
                {
                    return string.Empty;
                }
                else
                {
                    throw;
                }
            }
        }

        public string GetAttribute(string attributeName) => NativeElement.GetAttribute(attributeName);

        public void Click()
        {
            WebDriverWait wait = new(NativeDriver, TimeSpan.FromMilliseconds(ConfigurationService.GetSection<TimeoutSettings>().GeneralTimeout));
            wait.Until(c=>SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(NativeElement) != null);

            if (ConfigurationService.GetSection<WebSettings>().ScrollToElements)
            {
                _js.ExecuteScript("var rect = arguments[0].getBoundingClientRect();window.scrollTo(rect.x, rect.y);", NativeElement);
            }

            _nativeElement.Click();
        }

        public void TypeText(string text)
        {
            NativeElement?.Clear();
            NativeElement?.SendKeys(text);
        }

        public void SelectOptionByText(string selectOption)
        {
            new SelectElement(NativeElement).SelectByText(selectOption);
        }

        public void SelectOptionByValue(string selectOption)
        {
            new SelectElement(NativeElement).SelectByValue(selectOption);
        }

        public void Check()
        {
            _js.ExecuteScript("var checkbox = arguments[0]; if (!checkbox.checked){checkbox.click()};", NativeElement);
        }

        public void Uncheck()
        {
            _js.ExecuteScript("var checkbox = arguments[0]; if (checkbox.checked){checkbox.click()};", NativeElement);
        }

        public void RemoveHiddenFromElement()
        {
            _js.ExecuteScript("arguments[0].hidden = false;", NativeElement);
        }

        public void ClearElementWithJavaScript()
        {
            _js.ExecuteScript("arguments[0].value = \"\";", NativeElement);
        }

        public IElement CreateElement(By locator)
        {
            return new Element(NativeDriver, locator, this);
        }

        public IElementsList CreateElements(By locator)
        {
            return new ElementsList(NativeDriver, locator);
        }

        public void JSClick()
        {
            _js.ExecuteScript("arguments[0].click();", NativeElement);
        }
    }
}
