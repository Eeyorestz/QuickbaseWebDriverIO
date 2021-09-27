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
        public virtual string PageUrl { get; set; }

        public string Domain => ConfigurationService.GetSection<WebSettings>().BaseUrl;

        public string GetPageUrl => Driver.Url.ToString();

        public virtual void Open()
        {
            Driver.GoToUrl(new Uri(new Uri(Domain), PageUrl));
            Driver.WaitForLoadedPage();
        }

        public void AssertLandedOnPage(string url)
        {
            Driver.WaitForLoadedPage();
            string pageUrl = GetPageUrl;

            Assert.IsTrue(pageUrl.Contains(url), $"The page url should contain {url}, but actually is {pageUrl}");
        }
    }
}
