using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using System.Collections;

namespace SeleniumLearning
{
    public class SortWebTables
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]
        public void SortTable()
        {
            ArrayList a = new ArrayList();
            ArrayList b = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            //step 1 - get all veggie name in arrayList A

            //step 2 - sort this arrayList - expected arrayList A

            //step 3 - go and click column

            //step 4 - get all veggie names into arrayList B

            //arrayList A to B = equal

            IList < IWebElement > veggies = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            a.Sort();
            /*foreach (string veggie in a)
            {
                TestContext.WriteLine(veggie);
            }*/

            driver.FindElement(By.CssSelector("th[aria-label*='fruit name']")).Click();  //regular expression
            //xpath regular expression : th[cintains(@aria-label,'fruit name')]

            veggies = driver.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in veggies)
            {
                b.Add(veggie.Text);
            }

            Assert.AreEqual(a, b);
        }
    }
}
