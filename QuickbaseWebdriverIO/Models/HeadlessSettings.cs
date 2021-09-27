using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbaseWebdriverIO.Models
{
    public class HeadlessSettings
    {
        public bool IsEnabled { get; set; }

        public string ScreenshotResolution { get; set; }

        public bool FullPageScreenshot { get; set; }
    }
}
