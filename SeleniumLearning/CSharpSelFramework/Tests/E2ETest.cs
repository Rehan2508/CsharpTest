using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using CSharpSelFramework.Utilities;
using CSharpSelFramework.PageObject;
using System.Runtime.CompilerServices;

namespace CSharpSelFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class E2ETest : Base
    {

        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("rahulshetty", "learning")]

        //run all data sets of Test method in parallel - Done
        //run all test test methods in one class parallel - Done [Parallelizable(ParallelScope.Children)] at class level
        //run all test files in project parallel - Done

        // dotnet test pathto.csproj (All tests)  : pathto.csproj is the xml project file
        // dotnet test pathto.csproj --filter TestCategory=Regression
        //review : dotnet test pathto.csproj --filter TestCategory=Regression --% -- TestRunParameters.Parameter(name=\"browserName\",value=\"Edge\")

        [Test, TestCaseSource("AddTestDataConfig"), Category("Regression")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(string username, string password, string[] expectedProducts)
        {
            //string[] expectedProducts = { "iphone X", "Blackberry" };
            string[] actualProducts = new string[2];

            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productPage = loginPage.validLogin(username, password);
            productPage.WaitForPageDisplay();
            
            IList<IWebElement> products = productPage.GetCards();
            foreach (IWebElement product in products)
            {
                string p = product.FindElement(productPage.GetCardTitle()).Text; //in the scope of current product
                if (expectedProducts.Contains(p))
                {
                    //click on cart
                    product.FindElement(productPage.GetAddToCartButton()).Click(); //in whole scope there will be four but in scope for current product
                }
                TestContext.Progress.WriteLine(p);
            }

            CheckoutPage checkoutPage = productPage.Checkout();

            IList<IWebElement> checkoutCards = checkoutPage.GetCards();
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }

            Assert.AreEqual(expectedProducts, actualProducts);

            ConfirmationPage confirmationPage = checkoutPage.Checkout();
            confirmationPage.GetCountry().SendKeys("ind");
            string textMessage = confirmationPage.Confirmation();

            StringAssert.Contains("Success", textMessage);
        }

        [Test, Category("Smoke")]
        public void LocatorsIdentification()
        {
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.Value.FindElement(By.Id("username")).Clear();
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshetty");
            driver.Value.FindElement(By.Name("password")).SendKeys("123456");

            //checkbox
            driver.Value.FindElement(By.XPath("//div[@class = 'form-group'][5]/label/span/input")).Click();

            driver.Value.FindElement(By.XPath("//input[@value = 'Sign In']")).Click();  //WebElement.click();
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(driver.Value.FindElement(By.XPath("//input[@id = 'signInBtn']")), "Sign In"));
            string errorMessage = driver.Value.FindElement(By.ClassName("alert-danger")).Text;
        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(GetDataParser().ExtractData("username"), GetDataParser().ExtractData("password"), GetDataParser().ExtractDataArray("products"));
            yield return new TestCaseData(GetDataParser().ExtractData("username"), GetDataParser().ExtractData("password"), GetDataParser().ExtractDataArray("products"));
            yield return new TestCaseData(GetDataParser().ExtractData("usernameWrong"), GetDataParser().ExtractData("passwordWrong"), GetDataParser().ExtractDataArray("products"));
        }
    }
}
