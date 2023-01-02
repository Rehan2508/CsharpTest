using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.PageObject
{
    public class ProductsPage
    {
        private IWebDriver driver;
        private By cardTitle = By.CssSelector(".card-title a");
        private By addToCart = By.CssSelector(".card-footer button");
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        public void WaitForPageDisplay()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

        }

        public IList<IWebElement> GetCards() 
        {
            return cards;
        }

        public CheckoutPage Checkout()
        {
            checkoutButton.Click();
            return new CheckoutPage(driver);
        }

        public By GetCardTitle()
        {
            return cardTitle;
        }

        public By GetAddToCartButton()
        {
            return addToCart;
        }
    }
}
