using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMX.Selenium.PageObjects.Originals
{
    public class OriginalsPage : BasePage
    {
        #region Constructor
        public OriginalsPage(IWebDriver driver) : base(driver) { }
        #endregion

        #region Page Objects

        [FindsBy(How = How.XPath, Using = "//md-card[span[text()='Originals']]")]
        private IWebElement originalsCard = null;
       
        #endregion

        #region Methods



        #endregion
    }
}
