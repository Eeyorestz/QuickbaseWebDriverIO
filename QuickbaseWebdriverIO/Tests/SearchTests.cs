using NUnit.Framework;
using QuickbaseWebdriverIO.Pages;

namespace QuickbaseWebdriverIO.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class SearchTests : TestBase
    {
        private WebDriverIOPageBase _basePage;
        private ApiPage _apiPage;

        [OneTimeSetUp]
        public void ClassPreSetup()
        {
            _basePage = new WebDriverIOPageBase();
            _apiPage = new ApiPage();
            _basePage.Open();
            _basePage.NavigationBarButton("API").Click();
        }

        [Test]
        public void UserNavigatedToClickPagDocumentation_When_SearchForClickInSearchBar()
        {
            var searchCriteria = "click";

            _apiPage.Search(searchCriteria);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(searchCriteria, _basePage.PageHeaders.Text);
                _basePage.AssertLandedOnPage("docs/api/element/click/");
            });
        }

        [Test]
        public void LeftMenuExpanded_When_ClickOnMenu()
        {
            var menuItem = "Protocols";

            _apiPage.ExpandLeftNavigationMenuItem(menuItem);

            Assert.AreEqual(_apiPage.LeftNavigationMenuItemsFactory(menuItem), _apiPage.ExtractSubMenuItemsText(menuItem));
        }
    }
}
