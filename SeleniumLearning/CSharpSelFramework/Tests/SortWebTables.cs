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
using CSharpSelFramework.Utilities;

namespace SeleniumLearning
{
    [Parallelizable(ParallelScope.Self)]
    public class SortWebTables : Base
    {
        [Test]
        public void SortTable()
        {
            driver.Value.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
            ArrayList a = new ArrayList();
            ArrayList b = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.Value.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            //step 1 - get all veggie name in arrayList A

            //step 2 - sort this arrayList - expected arrayList A

            //step 3 - go and click column

            //step 4 - get all veggie names into arrayList B

            //arrayList A to B = equal

            IList < IWebElement > veggies = driver.Value.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in veggies)
            {
                a.Add(veggie.Text);
            }

            a.Sort();
            /*foreach (string veggie in a)
            {
                TestContext.WriteLine(veggie);
            }*/

            driver.Value.FindElement(By.CssSelector("th[aria-label*='fruit name']")).Click();  //regular expression
            //xpath regular expression : th[cintains(@aria-label,'fruit name')]

            veggies = driver.Value.FindElements(By.XPath("//tr/td[1]"));
            foreach (IWebElement veggie in veggies)
            {
                b.Add(veggie.Text);
            }

            Assert.AreEqual(a, b);
        }
    }
}
