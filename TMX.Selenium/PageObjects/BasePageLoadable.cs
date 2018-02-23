using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TMX.Selenium.PageObjects
{
    public abstract class BasePageLoadable<T> : LoadableComponent<T> where T : LoadableComponent<T>
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected IJavaScriptExecutor js;
        protected Actions actions;

        public BasePageLoadable(IWebDriver driver, int pageLodTimeOut = 60, int elemtTimeOut = 20)
        {
            this.driver = driver;
            this.actions = new Actions(driver);
            PageFactory.InitElements(this.driver, this);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(elemtTimeOut);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLodTimeOut);
        }

        public void Wait<TResult>(int seconds, Func<IWebDriver, TResult> condition)
        {
            Thread.Sleep(100);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }

    }
}
