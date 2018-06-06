using System;
using TechTalk.SpecFlow;
using TMX.Selenium.PageObjects.Home;

namespace TMX.TestAutomation.FeatureSteps.Login
{
    [Binding]
    public class LoginSteps : BaseSteps
    {
        public LoginSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        HomePage homePage;

        [Given(@"I Go to dashboard")]
        public void GivenIGoToDashboard()
        {
            homePage = loginPage.Login();
        }

        [Given(@"I Select Matter")]
        public void GivenISelectMatter()
        {
            homePage.SelectMatter();
        }

        [Given(@"I Goto Matter Setting")]
        [When(@"I Goto Matter Setting")]
        public void GivenIGotoMatterSetting()
        {
            homePage.ClickMatterSetting();
        }

        [When(@"I Click (.*) Tab")]
        public void WhenIClick(string tabName)
        {
            homePage.ClickOnTab(tabName);
        }

        [When(@"I click on Bundles")]
        public void WhenIClickOnBundles()
        {
            homePage.GotoBundles();
        }        

        [When(@"I Logout From TMX")]
        [Then(@"I Logout From TMX")]
        public void WhenILogoutTMX()
        {
            homePage.LogOut();
        }

        [Then(@"login page should display")]
        public void ThenLoginPageShouldDisplay()
        {
           
        }

    }
}
