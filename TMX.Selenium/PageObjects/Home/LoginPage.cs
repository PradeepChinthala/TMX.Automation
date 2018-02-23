using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Configurations;
using OpenQA.Selenium.Support.UI;

namespace TMX.Selenium.PageObjects.Home
{
    public class LoginPage : BasePage
    {
        #region Construtor
        public LoginPage(IWebDriver driver) : base(driver) { }

        #endregion

        #region Page Objects

        [FindsBy]
        private IWebElement email = null, password=null;

        [FindsBy(How = How.CssSelector, Using = ".logo-and-content .btn-submit")]
        private IWebElement logInButton = null;

        #endregion


        #region Methods

        public HomePage Login(string email = null, string password = null)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                email = Config.Email;
                password = Config.Password;
            }

            Wait(ExpectedConditions.ElementToBeClickable(this.email.GetLocator()), 10);

            this.email.SendKeysWrapper(email);
            this.password.SendKeysWrapper(password);
            logInButton.ClickWrapper();
            return new HomePage(driver);
        }
        #endregion

    }
}
