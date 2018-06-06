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
using System.Data;

namespace TMX.Selenium.PageObjects.MatterSetting
{
    public class UserFieldsPage : BasePage
    {
        #region Constructor
        public UserFieldsPage(IWebDriver driver) : base(driver){}
        #endregion

        #region Page Objects

        [FindsBy(How = How.CssSelector, Using = "#fields md-card[class='mat-card'] div[class='field-row table-header'] div h4")]
        private IList<IWebElement> userFieldsHeaderNames = null;

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
                    Parameter.Add("NewField", textValue);
                    Logger.InfoFormat($"{textValue} : field Created ");
                }                    

                else if (nameField.Trim().Equals("DataType") && !textValue.Contains("null"))
                    dataType.SelectComboboxValue(textValue);
                
            }
            Wait(ExpectedConditions.ElementToBeClickable(save.GetLocator()));
            save.ClickWrapper();
        }        
       
        public bool GetNewlyCreatedField()
        {
            return true;
        }
        public void ClickSave()
        {
            save.ClickWrapper();
        }

        public DataTable GetFieldRecords()
        {
            DataTable htmlTable = new DataTable();
            IList<IWebElement> rows = null;
            var columnNames = userFieldsHeaderNames.Select(e => e.Text.Trim()).ToList();

            for(int i = 0; i <= columnNames.Count; i++)
            {
                htmlTable.Columns.Add(columnNames[i]);
                for(int j = 0; j <= columnNames.Count; j++)
                {

                }
            }


            return htmlTable;
        }
        #endregion
    }
}
