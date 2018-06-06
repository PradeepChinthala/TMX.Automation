﻿using System;
using TechTalk.SpecFlow;
using TMX.Selenium.PageObjects.Home;
using TMX.Selenium.PageObjects.MatterSetting;
using TMX.TestAutomation.FeatureSteps;

namespace TMX.Automation.FeatureSteps.MatterSetting
{
    [Binding]
    [Scope(Feature = "UserFields")]
    public class UserFieldsSteps : BaseSteps
    {
        UserFieldsPage userFieldsPage;
        public UserFieldsSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            userFieldsPage = new UserFieldsPage(driver);
        }

        [When(@"I Click AddField")]
        public void GivenIClickAddField()
        {
            userFieldsPage.ClickAddField();
        }

        [When(@"I Enter Fields")]
        public void GivenIEnterFields(Table table)
        {
            userFieldsPage.InsertTextValues(table);
        }


        [Then(@"Field Should Be Created")]
        public void ThenFieldShouldBeCreated()
        {
            userFieldsPage.GetNewlyCreatedField();
        }

        [When(@"I Click Edit Icon Beside Field")]
        public void WhenIClickEditIconBesideField()
        {
        }

        [Then(@"str Should Be Displayed")]
        public void ThenStrShouldBeDisplayed()
        {

        }

        [When(@"I Click Delete Button")]
        public void WhenIClickDeleteButton()
        {

        }

        [Then(@"(.*) Field Should Be Deleted")]
        public void ThenStrFieldShouldBeDeleted(string fieldName)
        {

        }

    }
}
