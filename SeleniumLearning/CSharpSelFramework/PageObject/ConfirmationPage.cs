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
    public class ConfirmationPage
    {
        private IWebDriver driver;
        public ConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement country;

        [FindsBy(How = How.LinkText, Using = "India")]
        private IWebElement india;

        [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
        private IWebElement checkBox;

        [FindsBy(How = How.CssSelector, Using = "[value='Purchase']")]
        private IWebElement purchaseButton;

        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successMessage;

        public IWebElement GetCountry()
        {
            return country;
        }

        public void WaitForChoiceToLoad()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
        }

        public string Confirmation()
        {
            WaitForChoiceToLoad();
            india.Click();
            checkBox.Click();
            purchaseButton.Click();
            return successMessage.Text;
        }
    }
}
