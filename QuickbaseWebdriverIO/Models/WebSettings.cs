namespace QuickbaseWebdriverIO.Models
{
    public class WebSettings
    {
        /// <summary>
        /// Gets or sets base url for the product
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Boolean setting if WebDriver should scroll to the element before click
        /// </summary>
        public bool ScrollToElements { get; set; } = true;
    }
}
