using System.Collections.Generic;
using NUnit.Framework;
using QuickbaseWebdriverIO.Extensions;
using QuickbaseWebdriverIO.Pages;

namespace QuickbaseWebdriverIO.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class SearchTests : TestBase
    {
        private WebDriverIOPageBase _basePage;
        private ApiPage _apiPage;

        public static IEnumerable<TestCaseData> NavigationItems
        {
            get
            {
                yield return new TestCaseData("Docs", "docs/gettingstarted");
                yield return new TestCaseData("API", "docs/api");
                yield return new TestCaseData("Blog", "blog");
                yield return new TestCaseData("Contribute", "docs/contribute");
                yield return new TestCaseData("Community", "community/support");
            }
        }

        [OneTimeSetUp]
        public void ClassPreSetup()
        {
            _basePage = new WebDriverIOPageBase();
            _apiPage = new ApiPage();
            _basePage.Open();
        }

        [Test]
        [TestCaseSource(nameof(NavigationItems))]
        public void NavBarNavigatingToPage_When_ClickButton(string navBarItem, string expectedUrl)
        {
            _basePage.NavigationBarButton(navBarItem).Click();
            //Driver.WaitForLoadedPage();

            _basePage.AssertLandedOnPage(expectedUrl);

        }

        [Test]
        public void UserNavigatedToClickPagDocumentation_When_SearchForClickInSearchBar()
        {
            _basePage.NavigationBarButton("API").Click();
            var searchCriteria = "click";

            _apiPage.Search(searchCriteria);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(searchCriteria, _basePage.PageHeaders.Text);
                _basePage.AssertLandedOnPage("docs/api/element/click/");
            });
        }
    }
}
