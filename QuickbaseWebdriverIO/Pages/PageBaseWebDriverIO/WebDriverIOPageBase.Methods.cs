namespace QuickbaseWebdriverIO.Pages
{
    public partial class WebDriverIOPageBase
    {
        public void Search(string searchCriteria)
        {
            SearchButton.Click();
            SearchSection.SearchAndSelectExactMatch(searchCriteria);
        }

        public void ExpandLeftNavigationMenuItem(string leftMenuItem) 
        {
            LeftNavigationMenuButton(leftMenuItem).Click();
        }
    }
}
