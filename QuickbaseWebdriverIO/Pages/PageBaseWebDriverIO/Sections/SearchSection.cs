using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using QuickbaseWebdriverIO.Interfaces;

namespace QuickbaseWebdriverIO.Pages.PageBaseWebDriverIO.Sections
{
    public class SearchSection : PageBase
    {
        public IElement SearchInput => Driver.CreateElement(By.ClassName("DocSearch-Input"));

        public IElementsList SearchResults => Driver.CreateElements(By.XPath("//ul[@role='listbox']/li"));

        private IElement AddAsFavoriteButton(string searchCriteria) => Driver.CreateElement(CommonRecentSearchXpath(searchCriteria, "Save"));

        private IElement PreSearchIcon(string searchCriteria) => Driver.CreateElement(By.XPath($"//div[normalize-space(text()) = 'Favorites']/following-sibling::ul//span[normalize-space(text()) = '{searchCriteria}']/../preceding-sibling::div[@class='DocSearch-Hit-icon']"));
     
        public void Search(string searchCriteria)
        {
            SearchInput.NativeElement.Clear();
            foreach (var letter in searchCriteria)
            {
                SearchInput.NativeElement.SendKeys(letter.ToString());
            }
        }

        public void SearchAndSelectExactMatch(string searchCriteria)
        {
            Search(searchCriteria);

            SearchResults.Where(e => e.InnerText.Equals(searchCriteria)).FirstOrDefault().Click();
        }

        public void SaveSearch(string searchCriteria)
        {
            AddAsFavoriteButton(searchCriteria).Click();
        }

        public void RemoveSearchFromHistory(string searchCriteria)
        {
            Driver.CreateElement(CommonRecentSearchXpath(searchCriteria, "Remove")).Click();
        }

        public void AssertSearchAddedAsFavorite(string searchCriteria)
        {
            var innerHtml = PreSearchIcon(searchCriteria).InnerHtml;
            Assert.Multiple(() =>
            {
                Assert.IsFalse(innerHtml.Contains("<g stroke="));
                Assert.IsFalse(AddAsFavoriteButton(searchCriteria).IsDisplayed);
            });
        }

        public void AssertItemRemovedFromFavorites(string searchCriteria) 
        {
            Assert.Multiple(() =>
            {
                Assert.IsFalse(PreSearchIcon(searchCriteria).IsDisplayed);
                Assert.IsTrue(AddAsFavoriteButton(searchCriteria).IsDisplayed);
            });
        }

        private By CommonRecentSearchXpath(string searchCriteria, string buttonTitle)
        {
            return By.XPath($"//ul[@role='listbox']//span[normalize-space(text()) = '{searchCriteria}']/../following-sibling::div//button[contains(@title,'{buttonTitle}')]");
        }
    }
}
