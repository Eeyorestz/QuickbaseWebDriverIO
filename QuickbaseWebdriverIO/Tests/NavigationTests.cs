using System.Collections.Generic;
using NUnit.Framework;
using QuickbaseWebdriverIO.Pages;

namespace QuickbaseWebdriverIO.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class NavigationTests : TestBase
    {
        private WebDriverIOPageBase _basePage;

        public static IEnumerable<TestCaseData> NavigationItems
        {
            get
            {
                yield return new TestCaseData("Contribute", "docs/contribute");
                yield return new TestCaseData("Docs", "docs/gettingstarted");
                yield return new TestCaseData("API", "docs/api");               
            }
        }

        [OneTimeSetUp]
        public void ClassPreSetup()
        {
            _basePage = new WebDriverIOPageBase();
            _basePage.Open();
        }

        [Test]
        [TestCaseSource(nameof(NavigationItems))]
        public void NavBarNavigatingToPage_When_ClickButton(string navBarItem, string expectedUrl)
        {
            _basePage.NavigationBarButton(navBarItem).Click();

            _basePage.AssertLandedOnPage(expectedUrl);

        }
    }
}
