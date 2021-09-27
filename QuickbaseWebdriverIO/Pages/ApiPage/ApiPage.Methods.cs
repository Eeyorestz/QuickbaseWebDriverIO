using System.Collections.Generic;
using System.Linq;

namespace QuickbaseWebdriverIO.Pages
{
    public partial class ApiPage : WebDriverIOPageBase
    {
        public void ExpandLeftNavigationMenuItem(string leftMenuItem)
        {
            LeftNavigationMenuButton(leftMenuItem).Click();
        }

        public List<string> LeftNavigationMenuItemsFactory(string menuItem)
        {
            Dictionary<string, List<string>> subMenuItems = new Dictionary<string, List<string>>();
            subMenuItems.Add(
                "Protocols",
                new List<string>
                {
                    "WebDriver Protocol",
                    "Appium",
                    "Mobile JSON Wire Protocol",
                    "Chromium",
                    "Firefox",
                    "Sauce Labs",
                    "Selenium Standalone",
                    "JSON Wire Protocol"
                });
            subMenuItems.Add(
                "browser",
                new List<string>
                {

                });

            return subMenuItems.FirstOrDefault(k => k.Key == menuItem).Value;
        }

        public List<string> ExtractSubMenuItemsText(string mainItem) 
        {
            var itemsText = new List<string>();
            LeftNavigationMenuSubItems(mainItem).ForEach(x=> itemsText.Add(x.InnerText));

            return itemsText;
        }
    }
}
