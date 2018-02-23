using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TMX.Selenium.PageObjects.Dashboard
{


    public class DashboardPage : BasePage
    {
        #region Constructor
        public DashboardPage(IWebDriver driver) : base(driver) { }
        #endregion

        #region Page Objects

        [FindsBy(How = How.XPath, Using = "//md-card[span[text()='Originals']]")]
        private IWebElement originalsCard = null;
        
        #endregion

        #region Methods



        #endregion
    }
}
