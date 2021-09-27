using NUnit.Framework;
using OpenQA.Selenium;
using QuickbaseWebdriverIO.Enums;
using QuickbaseWebdriverIO.Pages;

namespace QuickbaseWebdriverIO.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture(BrowserType.Chrome)]
    public class SearchFunctionalityTests : TestBase
    {
        private WebDriverIOPageBase _basePage;
        public SearchFunctionalityTests(BrowserType browser)
           : base(browser) { }

        [OneTimeSetUp]
        public void ClassPreSetup()
        {
            _basePage = new WebDriverIOPageBase();
        }

        [SetUp]
        public void TestSetup()
        {
            _basePage = new WebDriverIOPageBase();
            _basePage.Open();
        }

        [Test]
        public void SearchAddedAsFavorite_When_OpenSearchBar()
        {
            var searchCriteria = "doubleClick";

            _basePage.Search(searchCriteria);
            _basePage.SearchButton.Click();
            _basePage.SearchSection.SaveSearch(searchCriteria);

            _basePage.SearchSection.AssertSearchAddedAsFavorite(searchCriteria);
        }

        [Test]
        public void SearchRemovedAsFavorite_When_OpenSearchBar_And_ClickRemoveButton()
        {
            var searchCriteria = "isClickable";

            _basePage.Search(searchCriteria);
            _basePage.SearchButton.Click();
            _basePage.SearchSection.SaveSearch(searchCriteria);
            _basePage.SearchSection.RemoveSearchFromHistory(searchCriteria);
            _basePage.SearchSection.SearchInput.TypeText(Keys.Escape);
            _basePage.SearchButton.Click();

            _basePage.SearchSection.AssertItemRemovedFromFavorites(searchCriteria);
        }
    }
}
