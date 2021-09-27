using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace QuickbaseWebdriverIO.Services
{
    public static class BrowserDisposalService
    {
        private static readonly List<string> _browsersToCheck = new()
        {
            "chrome",
            "edge",
            "firefox",
            "internet explorer",
        };

        private static List<string> _driversToCheck = new()
        {
            "chromedriver",
            "msedgedriver",
            "geckodriver",
            "iedriverserver",
        };

        public static void KillDrivers()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _driversToCheck = _driversToCheck.Union(_browsersToCheck).ToList();
            }

            KillAllProcesses(_driversToCheck);
        }

        private static void KillAllProcesses(List<string> processesToKill)
        {
            var processes = Process
                .GetProcesses()
                .Where(p => processesToKill.Any(x => p.ProcessName.ToLower().Contains(x)));

            foreach (var process in processes)
            {
                try
                {
                    process.Kill(true);
                }
                catch (Exception e)
                {
                    throw new Exception("Failed to kill all process", e);
                }
            }
        }
    }
}
