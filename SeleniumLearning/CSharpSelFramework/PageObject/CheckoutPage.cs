using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelFramework.PageObject
{
    public class CheckoutPage
    {
        private IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkoutCards;

        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement checkoutButton;

        public IList<IWebElement> GetCards()
        {
            return checkoutCards;
        }

        public ConfirmationPage Checkout()
        {
            checkoutButton.Click();
            return new ConfirmationPage(driver);
        }
    }
}
