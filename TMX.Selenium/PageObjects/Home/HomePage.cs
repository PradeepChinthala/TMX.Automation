using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Configurations;
using TMX.Selenium.PageObjects.Dashboard;
using TMX.Selenium.PageObjects.Originals;
using TMX.Selenium.PageObjects.Bundles;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Configurations;

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

        [FindsBy(How = How.CssSelector, Using = "span.icon-out")]
        private IWebElement logOutButton = null;

        [FindsBy(How = How.XPath, Using = "//span[span[text()='Dashboard']]/md-icon")]
        private IWebElement dashboard = null;

        [FindsBy(How = How.XPath, Using = "//span[span[text()='Originals']]")]
        private IWebElement originals = null;

        [FindsBy(How = How.XPath, Using = "//span[span[text()='Bundles']]")]
        private IWebElement bundles = null;

        [FindsBy(How = How.CssSelector, Using = "span.icon-cog")]
        private IWebElement matterSetting = null;

        [FindsBy(How = How.CssSelector, Using = "span.search-bar-settings")]
        private IWebElement searchFilter = null;

        [FindsBy(How = How.Id, Using = "md-tab-label-0-0")]
        private IWebElement basicFilterTab = null;

        [FindsBy(How = How.Id, Using = "md-tab-label-0-1")]
        private IWebElement textFilterTab = null;

        [FindsBy(How = How.CssSelector, Using = "div.mat-select-trigger")]
        private IWebElement matterDropDown = null;

        [FindsBy(How = How.CssSelector, Using = "div.mat-select-content md-option")]
        private IList<IWebElement> matterSelection = null;

        private string tabName = "//div[@class='side-nav-container']/a[span[contains(text(),'{0}')]]";
        #endregion


        #region Methods

        public void LogOut()
        {
            logOutButton.ClickWrapper();
        }
        public void SelectMatter()
        {
            matterDropDown.ClickWrapper();
            wait.Until(d => matterSelection.Count > 1);
            matterSelection.SelectCustomOption(Config.MatterName);
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

        public void ClickMatterSetting()
        {
            Wait(ExpectedConditions.ElementToBeClickable(matterSetting.GetLocator()));
            matterSetting.ClickWrapper();
        }
        
        public void ClickOnTab(string tabName)
        {
            var tabElement = driver.FindElement(By.XPath(string.Format(this.tabName,tabName)));
            tabElement.ClickWrapper();
        }
        public void ClickUserSetting()
        {
            userSetting.Click();
        }

        protected override void ExecuteLoad()
        {           
            if(!driver.Url.Contains("/dashboard"))
            {
                int i = 0;
                while(i < Config.Retries)
                {
                    driver.RefreshPage();
                    var result = WaitAndGetResult(ExpectedConditions.UrlContains("/dashboard"), waitMin);
                    if (result)
                        break;
                    i++;
                }                
            }          

        }

        protected override bool EvaluateLoadedStatus()
        {
            return WaitAndGetResult(ExpectedConditions.UrlContains("/dashboard"), 2);
        }
        #endregion
    }
 
}
