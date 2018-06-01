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
        protected uint waitMin = 30;
        protected uint waitMax = 60;
        protected uint waitSuper = 120;
        protected Actions actions;
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePageLoadable(IWebDriver driver, int pageLodTimeOut = 60, uint elemtTimeOut = 20)
        {
            this.driver = driver;
            this.actions = new Actions(driver);
            PageFactory.InitElements(this.driver, this);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(elemtTimeOut);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLodTimeOut);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(elemtTimeOut));
            wait.IgnoreExceptionTypes(typeof(Exception));
        }

        public void Wait<TResult>(Func<IWebDriver, TResult> condition, uint timeout = 30)
        {
            Thread.Sleep(100);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }

        public bool WaitAndGetResult<TResult>(Func<IWebDriver, TResult> condition, uint timeout = 30)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.PollingInterval = TimeSpan.FromMilliseconds(500);
                wait.IgnoreExceptionTypes(typeof(Exception));
                wait.Until(condition);
                return true;
            }
            catch { return false; }            
        }

        public void PageLoad()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(waitMax)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

    }
}
