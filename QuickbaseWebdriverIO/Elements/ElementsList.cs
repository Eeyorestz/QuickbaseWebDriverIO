using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using QuickbaseWebdriverIO.Browser;
using QuickbaseWebdriverIO.Interfaces;
using QuickbaseWebdriverIO.Services;

namespace QuickbaseWebdriverIO.Elements
{
    public class ElementsList : IElementsList
    {
        private readonly By _by;
        private readonly ElementFinderService _elementFinder;
        private readonly IWebDriver _driver;
        private readonly Element _parent;

        public ElementsList(IWebDriver driver, By by, Element parent = null)
        {
            _driver = driver;
            _by = by;
            _elementFinder = new ElementFinderService(driver);
         
            _parent = parent;
        }

        public int Count => _elementFinder.FindAll(_by).Count();

        public IElement this[int i] => GetAndWaitWebDriverElements().ElementAt(i);

        public IEnumerator<IElement> GetEnumerator() => GetAndWaitWebDriverElements().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void ForEach(Action<IElement> action)
        {
            foreach (var element in this)
            {
                action(element);
            }
        }

        private IEnumerable<IElement> GetAndWaitWebDriverElements()
        {
            var driverAdapter = new DriverAdapter(_driver);
            List<IWebElement> nativeElements = _elementFinder.FindAll(_by).ToList();

            for (int i = 0; i < nativeElements.Count; i++)
            {
                IWebElement nativeElement = nativeElements[i];
                if (_parent == null)
                {
                    yield return driverAdapter.CreateElement(nativeElement);
                }
                else
                {
                    yield return _parent.CreateElement(_by);
                }
            }
        }
    }
}
