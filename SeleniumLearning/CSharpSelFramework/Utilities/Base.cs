using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using System.Threading;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace CSharpSelFramework.Utilities
{
    public class Base
    {
        public ExtentReports extent;
        public ExtentTest test;

        string browserName;

        //repot file
        [OneTimeSetUp]
        public void Setup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("Username", "Rehan Dilkash");            
        }

        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            //review : dotnet test pathto.csproj --filter TestCategory=Regression --% -- TestRunParameters.Parameter(name=\"browserName\",value=\"Edge\")
            browserName = TestContext.Parameters["browserName"];
            if (browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            InitBrowser(browserName);
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                        driver.Value = new FirefoxDriver();
                        break;
                    }
                case "Chrome":
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                        driver.Value = new ChromeDriver();
                        break;
                    }
                case "Edge":
                    {
                        new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                        driver.Value = new EdgeDriver();
                        break;
                    }
            }
        }

        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public static JsonReader GetDataParser()
        {
            return new JsonReader();
        }

        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            DateTime time = DateTime.Now;
            string fileName = "sCREENSHOT_" + time.ToString("h_mm_ss") + ".png";
            if(status == TestStatus.Failed)
            {
                test.Fail("Test failed",CaptureScreenShot(driver.Value,fileName));
                //test.Fail("Test failed", CaptureScreenShot(driver.Value, fileName));
                test.Log(Status.Fail, "test failed with logtrace " + stackTrace);
            }
            else if(status == TestStatus.Passed)
            {
                test.Pass("Test Passed", CaptureScreenShot(driver.Value, fileName));
                test.Log(Status.Pass, "test passed with logtrace " + stackTrace);
            }

            extent.Flush();
            driver.Value.Quit();
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot,screenShotName).Build();
        }
    }
}
