using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using CSharpSelFramework.Utilities;

namespace SeleniumLearning
{
    [Parallelizable(ParallelScope.Self)]
    public class WindowHandlers : Base
    {
        [Test]
        public void WindowHandle()
        {
            string email = "mentor@rahulshettyacademy.com";
            string parentWindowId = driver.Value.CurrentWindowHandle;
            driver.Value.FindElement(By.ClassName("blinkingText")).Click();
            //WindowHandles : retuns collection of strings i.e, id of tabs
            Assert.AreEqual(2, driver.Value.WindowHandles.Count);

            string childWindowName = driver.Value.WindowHandles[1];
            driver.Value.SwitchTo().Window(childWindowName);
            TestContext.Progress.WriteLine(driver.Value.FindElement(By.CssSelector(".red")).Text);
            string text = driver.Value.FindElement(By.CssSelector(".red")).Text;
            string[] splittedText = text.Split(" ");
            TestContext.Progress.WriteLine(splittedText[4]);
            Assert.AreEqual(email, splittedText[4]);

            driver.Value.SwitchTo().Window(parentWindowId);
            driver.Value.FindElement(By.Id("username")).SendKeys(splittedText[4]);
        }
    }
}
