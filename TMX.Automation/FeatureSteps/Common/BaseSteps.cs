using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TMX.Selenium.PageObjects.Home;

namespace TMX.TestAutomation.FeatureSteps
{
    [Binding]
    public class BaseSteps
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected readonly ScenarioContext scenarioContext;

        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
            scenarioContext.TryGetValue<IWebDriver>(out driver);
            scenarioContext.TryGetValue<LoginPage>(out loginPage);
        }

    }
}
