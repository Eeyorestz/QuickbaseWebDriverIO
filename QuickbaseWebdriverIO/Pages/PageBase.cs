using System;
using NUnit.Framework;
using QuickbaseWebdriverIO.Browser;
using QuickbaseWebdriverIO.Extensions;
using QuickbaseWebdriverIO.Models;
using QuickbaseWebdriverIO.Services;

namespace QuickbaseWebdriverIO.Pages
{
    public class PageBase : BrowserFactory
    {

        private readonly string _domain;

        public PageBase()
        {
        }

        public PageBase(string domain)
        {
            _domain = domain;
        }

        public virtual string PageUrl { get; set; }
        public virtual string Domain => _domain ?? ConfigurationService.GetSection<WebSettings>().BaseUrl;
       

        public string GetPageUrl => Driver.Url.ToString();

        public void RefreshPage()
        {
            Driver.Refresh();
            Driver.WaitForLoadedPage();
        }

        public virtual void Open()
        {
            Driver.GoToUrl(new Uri(new Uri(Domain), PageUrl));
            Driver.WaitForLoadedPage();
        }

        /// <summary>
        /// Assert that page url contains the override for PageUrl.
        /// </summary>
        public void AssertLandedOnPage()
        {
            Driver.WaitForLoadedPage();

            Assert.IsTrue(GetPageUrl.Contains(PageUrl), $"The page url should contain {PageUrl}, but actually is {GetPageUrl}");
        }

        /// <summary>
        /// Assert that page url contains a string.
        /// </summary>
        /// <param name="url">partial url.</param>
        public void AssertLandedOnPage(string url)
        {
            Driver.WaitForLoadedPage();
            string pageUrl = GetPageUrl;

            Assert.IsTrue(pageUrl.Contains(url), $"The page url should contain {url}, but actually is {pageUrl}");
        }
    }
}
