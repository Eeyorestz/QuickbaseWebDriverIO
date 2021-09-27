using OpenQA.Selenium;
using QuickbaseWebdriverIO.Interfaces;
using QuickbaseWebdriverIO.Pages.PageBaseWebDriverIO.Sections;

namespace QuickbaseWebdriverIO.Pages
{
    public partial class WebDriverIOPageBase : PageBase
    {
        public SearchSection SearchSection => new();

        public IElement SearchButton => Driver.CreateElement(By.XPath("//button[@aria-label='Search']"));

        public IElement PageHeaders => Driver.CreateElement(By.TagName("h1"));
        public IElement PageSubHeaders => Driver.CreateElement(By.TagName("h2"));

        public IElement NavigationBarButton(string navigationMenuItem) => Driver.CreateElement(By.XPath($"//a[contains(@class,'navbar') and normalize-space(text()) = '{navigationMenuItem}']"));

        public IElement LeftNavigationMenuButton(string rearNavigationMenuItem) => Driver.CreateElement(By.XPath($"//a[normalize-space(text()) = '{rearNavigationMenuItem}']"));

        public IElementsList LeftNavigationMenuSubItems(string rearNavigationMenuItem) => Driver.CreateElements(By.XPath($"//a[normalize-space(text()) = '{rearNavigationMenuItem}']/following-sibling::ul/li"));
    }
}
