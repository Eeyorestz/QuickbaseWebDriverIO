using NUnit.Framework;
using QuickbaseWebdriverIO.Services;

namespace QuickbaseWebdriverIO
{
    [SetUpFixture]
    public class AssemblySetUp
    {
        [OneTimeSetUp]
        public void BeforeAllTestInit()
        {
            BrowserDisposalService.KillDrivers();
        }

        [OneTimeTearDown]
        public void AfterTestsCleanUp()
        {
            BrowserDisposalService.KillDrivers();
        }
    }
}
