using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    public class E2ETest
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
        public void EndToEndFlow()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new String[2];
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.FindElement(By.Name("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//div[@class = 'form-group'][5]/label/span/input")).Click();
            driver.FindElement(By.XPath("//input[@value = 'Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            //check for the partial text
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList < IWebElement > products = driver.FindElements(By.TagName("app-card"));
            foreach (IWebElement product in products)
            {
                string p = product.FindElement(By.CssSelector(".card-title a")).Text; //in the scope of current product
                if (expectedProducts.Contains(p))
                {
                    //click on cart
                    product.FindElement(By.CssSelector(".card-footer button")).Click(); //in whole scope there will be four but in scope for current product
                }
                TestContext.Progress.WriteLine(p);
            }

            driver.FindElement(By.PartialLinkText("Checkout")).Click();

            IList < IWebElement > checkoutCards = driver.FindElements(By.CssSelector("h4 a"));
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            Assert.AreEqual(expectedProducts, actualProducts);

            driver.FindElement(By.CssSelector(".btn-success")).Click();

            driver.FindElement(By.Id("country")).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();
            driver.FindElement(By.CssSelector("label[for*='checkbox2']")).Click();
            driver.FindElement(By.CssSelector("[value='Purchase']")).Click();
            string textMessage = driver.FindElement(By.CssSelector(".alert-success")).Text;
            StringAssert.Contains("Success", textMessage);
        }
    }
}
