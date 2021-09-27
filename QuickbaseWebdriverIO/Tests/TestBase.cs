using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using QuickbaseWebdriverIO.Browser;
using QuickbaseWebdriverIO.Enums;

namespace QuickbaseWebdriverIO.Tests
{
    public class TestBase : BrowserFactory
    {
        private BrowserType _browserName;

        public TestBase(BrowserType browserName)
        {
            _browserName = browserName;
        }

        public TestBase()
        {

        }

        [OneTimeSetUp]
        public void Init()
        {
            InitDriver(_browserName);
            ClassInit();
        }

        public virtual void ClassInit()
        {
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            Driver.Close();
        }

        [SetUp]
        public void TestPreSetup()
        {
           
        }

        [TearDown]
        public void TearDown()
        {
            var testName = TestContext.CurrentContext.Test.MethodName;
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = Path.Combine(InitilizeAndCreateDirectory("Screenshots"), $"{testName}-{DateTime.Now.ToShortDateString().Replace("/", "_")}");
                Directory.CreateDirectory(path);
                
                Driver.TakeScreenshot($"{path}.png");
                TestContext.AddTestAttachment($"{path}.png");
                Directory.Delete(path,true);
            }
        }

        private string InitilizeAndCreateDirectory(string path)
        {
            var location = new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var projectDirectory = new FileInfo(location.AbsolutePath).Directory.FullName;
            var fullPath = Path.Combine(projectDirectory, path);
            Directory.CreateDirectory(fullPath);

            return fullPath;
        }
    }
}
