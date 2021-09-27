using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using QuickbaseWebdriverIO.Interfaces;

namespace QuickbaseWebdriverIO.Pages.PageBaseWebDriverIO.Sections
{
    public class SearchSection : PageBase
    {
        public IElement SearchInput => Driver.CreateElement(By.ClassName("DocSearch-Input"));

        public IElement SearchClearButton => Driver.CreateElement(By.XPath("//button[@type='reset']"));

        public IElementsList SearchResults => Driver.CreateElements(By.XPath("//ul[@role='listbox']/li"));

        public void Search(string searchCriteria) 
        {
            SearchInput.TypeText(searchCriteria);
        }

        public void SearchAndSelectExactMatch(string searchCriteria)
        {
            Search(searchCriteria);
            
            SearchResults.Where(e => e.InnerText.Equals(searchCriteria)).FirstOrDefault().Click();
        }
    }
}
