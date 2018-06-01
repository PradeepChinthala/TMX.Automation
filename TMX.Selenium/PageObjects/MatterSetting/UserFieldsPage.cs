using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using Configurations;

namespace TMX.Selenium.PageObjects.MatterSetting
{
    public class UserFieldsPage : BasePage
    {
        Dictionary<string, IWebElement> dict;
        #region Constructor
        public UserFieldsPage(IWebDriver driver) : base(driver){}
        #endregion

        #region Page Objects

        [FindsBy(How = How.CssSelector, Using = ".side-nav-container a:nth-child(6) span")]
        private IWebElement userFieldTab = null;

        [FindsBy(How = How.CssSelector, Using = "app-fields md-card button")]
        private IWebElement addField = null;

        [FindsBy(How = How.CssSelector, Using = "md-dialog-container input[formcontrolname='shortName']")]
        private IWebElement shortName = null;

        [FindsBy(How = How.CssSelector, Using = "md-dialog-container input[formcontrolname='fullName']")]
        private IWebElement fullName = null;

        [FindsBy(How = How.CssSelector, Using = "md-dialog-container select[formcontrolname='dataType']")]
        private IWebElement dataType = null;

        [FindsBy(How = How.CssSelector, Using = "md-dialog-container select[formcontrolname='contentType']")]
        private IWebElement contentType = null;

        [FindsBy(How = How.CssSelector, Using = "md-dialog-container div button:nth-child(2)")]
        private IWebElement save = null;

        [FindsBy(How = How.CssSelector, Using = "md-dialog-container div button:nth-child(1)")]
        private IWebElement cancel = null;
        #endregion

        #region Methods
        
        public void ClickAddField()
        {
            Wait(ExpectedConditions.ElementToBeClickable(addField.GetLocator()));
            addField.ScrollToElementPage();
            addField.ClickWrapper();
        }

        public void InsertTextValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                String textValue = row["values"];
                var nameField = row["nameField"].Trim();

                if (nameField.Trim().Equals("ShortName") && !textValue.Contains("null"))
                    shortName.SendKeysWrapper(textValue);

                else if (nameField.Trim().Equals("FullName") && !textValue.Contains("null"))
                {
                    fullName.SendKeysWrapper(textValue);
                    Logger.Informat($"name of the field : {textValue}");
                }                    

                else if (nameField.Trim().Equals("DataType") && !textValue.Contains("null"))
                    dataType.SelectComboboxValue(textValue);
                
            }
            save.ClickWrapper();
        }
        
        public void SelectDataType(string DataType)
        {
           // dataType.S();
        }
        public void ClickSave()
        {
            save.ClickWrapper();
        }
        #endregion
    }
}
