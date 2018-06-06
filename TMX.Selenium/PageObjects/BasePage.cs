﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace TMX.Selenium.PageObjects
{
    public abstract class BasePage
    {
        protected uint waitMin = 30;
        protected uint waitMax = 60;
        protected uint waitSuper = 120;
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected Actions actions;

        public BasePage(IWebDriver driver,int pageLodTimeOut = 50, int elemtTimeOut = 20)
        {
            this.driver = driver;
            this.actions = new Actions(driver);
            PageFactory.InitElements(this.driver, this);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(elemtTimeOut);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLodTimeOut);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(elemtTimeOut));
            wait.IgnoreExceptionTypes(typeof(Exception));
        }
        public void Wait<TResult>(Func<IWebDriver, TResult> condition, int timeout = 5)
        {  
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            wait.Until(condition);
        }
    }
}
