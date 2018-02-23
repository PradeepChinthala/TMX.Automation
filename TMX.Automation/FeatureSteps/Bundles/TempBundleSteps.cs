using System;
using TechTalk.SpecFlow;
using TMX.Selenium.PageObjects.Bundles;

namespace TMX.TestAutomation.FeatureSteps.Bundles
{
    [Scope(Feature = "Temp Bundle")]
    public class TempBundleSteps : BaseSteps
    {
        BundlePage bundlePage;
        public TempBundleSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            bundlePage = new BundlePage(driver);
        }

        [When(@"I click on index view")]
        public void WhenIClickOnIndexView()
        {
            bundlePage.ClickIndexView();
        }
        
        [When(@"I click on my view")]
        public void WhenIClickOnMyView()
        {
            bundlePage.ClickMyView();
        }
    }
}
