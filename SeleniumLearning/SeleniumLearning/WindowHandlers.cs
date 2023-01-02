using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class WindowHandlers
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void WindowHandle()
        {
            string email = "mentor@rahulshettyacademy.com";
            string parentWindowId = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();
            //WindowHandles : retuns collection of strings i.e, id of tabs
            Assert.AreEqual(2, driver.WindowHandles.Count);

            string childWindowName = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);
            string text = driver.FindElement(By.CssSelector(".red")).Text;
            string[] splittedText = text.Split(" ");
            TestContext.Progress.WriteLine(splittedText[4]);
            Assert.AreEqual(email, splittedText[4]);

            driver.SwitchTo().Window(parentWindowId);
            driver.FindElement(By.Id("username")).SendKeys(splittedText[4]);
        }
    }
}
