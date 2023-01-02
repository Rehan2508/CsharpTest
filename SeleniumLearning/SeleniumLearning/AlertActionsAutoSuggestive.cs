using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    internal class AlertActionsAutoSuggestive
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }

        [Test]
        public void frames()
        {
            //scroll : selenium cannot scroll you have to use javascript executer(by firing javascript events)
            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            //id, name, index
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();
            Thread.Sleep(4000);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        }

        [Test]
        public void TestAlert()
        {
            string name = "Rehan";
            driver.FindElement(By.CssSelector("#name")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[onclick*='displayConfirm']")).Click();
            string alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();   //Accept() : posit value
            //driver.SwitchTo().Alert().Dismiss();   //Dismiss() : negetive value
            //driver.SwitchTo().Alert().SendKeys("hello");

            StringAssert.Contains(name,alertText);
        }

        [Test]
        public void TestAutoSuggestiveDropDowns()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            Thread.Sleep(3000);
            IList < IWebElement > options = driver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach (IWebElement option in options)
            {
                //TestContext.WriteLine(option.Text);
                if(option.Text.Equals("India"))
                    option.Click();
            }

            //will not get the text India, it will only get static texts
            //TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).Text);
            //will be able to get dynamic values shown on the page
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }

        [Test]
        public void TestActions()
        {
            driver.Url = "https://rahulshettyacademy.com";
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            //driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")));
            a.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"))).Click().Perform();
        }

        [Test]
        public void TestACtionsDragAndDrop()
        {
            driver.Url = "https://demoqa.com/droppable/";
            Actions action = new Actions(driver);
            action.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("draggable"))).Perform();
        }
    }
}
