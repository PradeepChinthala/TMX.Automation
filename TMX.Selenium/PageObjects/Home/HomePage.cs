using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Configurations;
using TMX.Selenium.PageObjects.Dashboard;
using TMX.Selenium.PageObjects.Originals;
using TMX.Selenium.PageObjects.Bundles;
using System.Linq;
using OpenQA.Selenium.Support.UI;

namespace TMX.Selenium.PageObjects.Home
{
    public class HomePage : BasePageLoadable<HomePage>
    {
        #region Constructor
        public HomePage(IWebDriver driver) : base(driver) { }

        #endregion


        #region Page Objects

        [FindsBy(How = How.CssSelector, Using = "span.icon-profile")]
        private IWebElement userSetting = null;

        [FindsBy(How = How.CssSelector, Using = ".mat-menu-content button:nth-child(2)")]
        private IWebElement logOutButton = null;

        [FindsBy(How = How.XPath, Using = "//span[span[text()='Dashboard']]/md-icon")]
        private IWebElement dashboard = null;

        [FindsBy(How = How.XPath, Using = "//span[span[text()='Originals']]")]
        private IWebElement originals = null;

        [FindsBy(How = How.XPath, Using = "//span[span[text()='Bundles']]")]
        private IWebElement bundles = null;

        [FindsBy(How = How.CssSelector, Using = "span.icon-cog")]
        private IWebElement caseSettings = null;

        [FindsBy(How = How.CssSelector, Using = "span.search-bar-settings")]
        private IWebElement searchFilter = null;

        [FindsBy(How = How.Id, Using = "md-tab-label-0-0")]
        private IWebElement basicFilterTab = null;

        [FindsBy(How = How.Id, Using = "md-tab-label-0-1")]
        private IWebElement textFilterTab = null;

        //[FindsBy(How = How.XPath, Using = "//app-root[contains(text(),'Loading')]")]
        //private IWebElement loading = null;


        #endregion


        #region Methods

        public void LogOut()
        {
            userSetting.ClickWrapper();
            logOutButton.ClickWrapper();
        }

        public void SelecteFilter(string filterSelection)
        {
            var baseFilter = filterSelection.Split('@');
            var childFilters = baseFilter[1].Split(',').ToList();
            var activeFilter = (basicFilterTab.GetText().Contains(baseFilter[0])) ? basicFilterTab : textFilterTab;
            searchFilter.ClickWrapper();
            activeFilter.ClickWrapper();
            foreach(string f in childFilters)
            {
                //driver.FindElement(By.CssSelector($"div.search-bar-settings-container ")).Click();
            }
            searchFilter.ClickWrapper();

        }
        
        public DashboardPage GotoDashBoard()
        {
            dashboard.ClickWrapper();
            return new DashboardPage(driver);
        }

        public OriginalsPage GotoOriginals()
        {
            originals.ClickWrapper();
            return new OriginalsPage(driver);
        }

        public BundlePage GotoBundles()
        {
            Wait(ExpectedConditions.ElementExists(bundles.GetLocator()));
            bundles.ClickWrapper();
            return new BundlePage(driver);
        }

        public void ClickCaseSettings()
        {
            caseSettings.ClickWrapper();
        }

        protected override void ExecuteLoad()
        {
            driver.Navigate().Refresh();
        }

        protected override bool EvaluateLoadedStatus()
        {
            return WaitAndGetResult(ExpectedConditions.UrlContains("/dashboard"), waitLong);
        }
        #endregion
    }
 
}
