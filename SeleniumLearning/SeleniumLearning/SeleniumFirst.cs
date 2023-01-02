using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class SeleniumFirst
    {

        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //driver = new FirefoxDriver();   //geckodriver like chromedriver.exe

            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            //driver = new EdgeDriver();  //edgedriver.exe
            
        }

        [Test]
        public void Test1()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);
            //2 windows opened by automation
            driver.Close();  // 1 window which got opened initially by the driver
            // driver.Quit();  // 2 windows
            //driver.PageSource  //html code of the page
        }
    }
}
