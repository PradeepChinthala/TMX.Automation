using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMX.Selenium.PageObjects.Bundles
{
    public class BundlePage : BasePage
    {
        #region Constructor
        public BundlePage(IWebDriver driver) : base(driver) { }
        #endregion

        #region Page Objects

        [FindsBy(How = How.CssSelector, Using = "div.index-mode-container h5:nth-child(1)")]
        private IWebElement indexView = null;

        [FindsBy(How = How.CssSelector, Using = "div.index-mode-container h5:nth-child(2)")]
        private IWebElement myView = null;

        #endregion

        #region Methods

        public void ClickIndexView()
        {
            indexView.ClickWrapper();
        }

        public void ClickMyView()
        {
            myView.ClickWrapper();
        }
        #endregion
    }
}
