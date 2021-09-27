using OpenQA.Selenium;
using QuickbaseWebdriverIO.Interfaces;

namespace QuickbaseWebdriverIO.Pages
{
    public partial class ApiPage
    {
        public override string PageUrl => "docs/api";


        public IElement LeftNavigationMenuButton(string rearNavigationMenuItem) => Driver.CreateElement(By.XPath($"//a[normalize-space(text()) = '{rearNavigationMenuItem}']"));

        public IElementsList LeftNavigationMenuSubItems(string rearNavigationMenuItem) => Driver.CreateElements(By.XPath($"//a[normalize-space(text()) = '{rearNavigationMenuItem}']/following-sibling::ul/li"));
    }
}
