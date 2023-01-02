using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    internal class Locators
    {

        //Xpath, Css, id, classname, name, tagname.

        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            //5 seconds can be declared globally
            //Implicit wait : it will wait for time declared or until the element is loaded (every where)
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void LocatorsIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.FindElement(By.Name("password")).SendKeys("123456");

            //checkbox
            driver.FindElement(By.XPath("//div[@class = 'form-group'][5]/label/span/input")).Click();
            //".text-info span:nth-child(1)"
            //driver.FindElement(By.CssSelector(".text-info span input")).Click();

            //css selectors & xpath :

            //css selector : tagname[attribute = 'value']
            //driver.FindElement(By.CssSelector("input[value = 'Sign In']")).Click();

            //xpath : //tagName[@attribute = 'value']
            driver.FindElement(By.XPath("//input[@value = 'Sign In']")).Click();  //WebElement.click();

            //Thread.Sleep(3000);
            //suppose this element is taking more time to load than the global implicit wait
            //here we will use explicit wait so for only this element we have to wait more

            //explict wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.FindElement(By.XPath("//input[@id = 'signInBtn']")),"Sign In"));
            string errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));

            //validate url of link text
            Assert.AreEqual("https://rahulshettyacademy.com/documents-request",link.GetAttribute("href"));
        }

    }
}
