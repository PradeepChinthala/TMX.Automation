using Configurations;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using TechTalk.SpecFlow;
using TMX.Selenium.PageObjects.Home;
using TMX.Selenium.WebDriverHelpers;

namespace TMX.TestAutomation.FeatureSteps
{
    [Binding]
    public sealed class Hooks
    {
        private readonly ScenarioContext scenarioContext;
        private static string browser = Config.Browser;

        public Hooks(ScenarioContext scenarioContext)
        {          
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetUp(FeatureContext featureContext)
        {
            IWebDriver driver;
            if (featureContext.Keys.Count > 0)
            {
                driver = featureContext.Get<IWebDriver>();
            }
            else
            {
                driver = new WebDriverFactory().GetWebDriver(browser);
            }
            scenarioContext.Set<IWebDriver>(driver);

            var loginPage = new LoginPage(driver);
            scenarioContext.Set<LoginPage>(loginPage);

        }

        [AfterScenario]
        public void AfterScenario(FeatureContext featureContext)
        {
            var driver = scenarioContext.Get<IWebDriver>();
            driver.Quit();
        }

        [AfterStep]
        public void LogStepResult()
        {
            //This method is here to fix the bug in SpecFlow
            //the bug is when using parallel execution the test output log is not written to the tests
            //see https://github.com/techtalk/SpecFlow/issues/737

            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(LevelOfParallelismAttribute), false);
            LevelOfParallelismAttribute attribute = null;

            if (attributes.Length > 0)
            {
                attribute = attributes[0] as LevelOfParallelismAttribute;
                var levelOfParallelism = (int)attribute.Properties.Get(attribute.Properties.Keys.First());

                if (levelOfParallelism > 1)
                {
                    string stepText = scenarioContext.StepContext.StepInfo.StepDefinitionType + " " + scenarioContext.StepContext.StepInfo.Text;
                    Console.WriteLine(stepText);
                    var stepTable = scenarioContext.StepContext.StepInfo.Table;
                    if (stepTable != null && stepTable.ToString() != "") Console.WriteLine(stepTable);
                    var error = scenarioContext.TestError;
                    Console.WriteLine(error != null ? "-> error: " + error.Message : "-> done.");
                }
            }
        }
    }
}
