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

        [Given(@"I go to dashboard")]
        public void GivenIGoToDashboard()
        {
            homePage = loginPage.Login();
        }

        [When(@"I click on Bundles")]
        public void WhenIClickOnBundles()
        {
            homePage.GotoBundles();
        }


        [When(@"I click on logout")]
        public void WhenIClickOnLogout()
        {
            homePage.LogOut();
        }

        [Then(@"login page should display")]
        public void ThenLoginPageShouldDisplay()
        {
           
        }

    }
}
