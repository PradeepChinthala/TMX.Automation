using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Threading;

namespace TMX.Selenium
{
    static class WebElementExtension
    {        

        public static void ClickWrapper(this IWebElement element, bool js = false)
        {
            Thread.Sleep(100);

            if (element.ControlEnabled())
            {
                if (!js)
                    js = element.Displayed == false ? true : false;

                if (js)
                    JavaScriptExecutor(JSOperator.Click, element);
                else
                    element.Click();
            }
            else
            {
                throw new Exception(String.Format("Element {0} is not displayed", element));
            }
        }

        public static void MoveByOffSetClick(this IWebElement element,int x,int y, MoveToElementOffsetOrigin org)
        {            
            Actions actions = new Actions(element.WrapsDriver());
            actions.MoveToElement(element,x,y, org).Click().Build().Perform();
        }

        public static void SendKeysWrapper(this IWebElement element, string value, bool js = false)
        {
            int retryCount = 2;
            while (retryCount > 0)
            {
                Thread.Sleep(100);
                if (element.ControlEnabled())
                {
                    if (!js)
                        js = element.Displayed == false ? true : false;

                    if (js)
                    {
                        element.ClearWrapper(js);
                        JavaScriptExecutor(string.Format(JSOperator.SetValue, value), element);
                        break;
                    }
                    else
                    {
                        element.ClearWrapper();
                        element.SendKeys(value); break;
                    }
                }
                try { element.GetText().Should().Contain(value); }
                catch
                {
                    element.ClearWrapper();
                    if (retryCount == 1)
                        throw;
                }
                retryCount--;
            }
        }

        public static void ClearWrapper(this IWebElement element, bool js = false)
        {
            if (element.ControlEnabled())
            {
                if (!js)
                    js = element.Displayed == false ? true : false;

                if (js)
                    JavaScriptExecutor("arguments[0].value = '';", element);
                else
                    element.Clear();
            }
            else
            {
                throw new Exception(String.Format("Element {0} is not displayed", element));
            }
                
        }

        public static void SelectCustomOption(this IList<IWebElement> elements, string value)
        {
            if (elements.FirstOrDefault().ControlEnabled())
            {
                IWebElement returnObject = elements.Where(e => e.Text.ToLower().Contains(value.ToLower())).FirstOrDefault();
                returnObject.ControlExist();
                returnObject.ClickWrapper();
            }
            else
            {
                throw new Exception(String.Format("Element {0} is not displayed", elements));
            }
        }

        public static void SelectComboboxValue(this IWebElement element, string value, bool js = false)
        {
            Thread.Sleep(200);
            int retryCount = 3;

            while (retryCount > 0)
            {
                if (element.ControlEnabled())
                {
                    if (!js)
                        js = element.Displayed == false ? true : false;

                    if (js)
                    {
                        JavaScriptExecutor(string.Format(JSOperator.SelectComboBox, value), element);
                        break;                        
                    }
                    else
                    {
                        SelectElement dropdown = new SelectElement(element);
                        dropdown.SelectByText(value);
                        if (element.GetText().Contains(value))
                            break;
                    }                    
                }                               
                retryCount--;
            }
        }

        public static bool ControlEnabled(this IWebElement element, bool enabled = true, uint timeoutInSeconds = 30)
        {            
            IWebDriver driver = element.WrapsDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.PollingInterval = TimeSpan.FromMilliseconds(500);
            wait.IgnoreExceptionTypes(typeof(Exception));
            return wait.Until(drv =>
            {
                if (!enabled && !element.Enabled)
                    return true;
                else if (enabled && element.Enabled)
                    return true;
                else
                    return false;
            });
        }

        public static bool ControlExist(this IWebElement element, uint timeoutInSeconds = 30)
        {
            try
            {
                return element.ControlEnabled(true,timeoutInSeconds);
            }
            catch { return false; }
        }

        public static void ScrollToElementPage(this IWebElement element)
        {
            JavaScriptExecutor(JSOperator.ScrollToElement, element);
        }

        public static string GetText(this IWebElement element, string result = null)
        {
            try
            {
                if (element.TagName == "input") { result = element.GetAttribute("value").ToString(); }
                else if (element.TagName == "textarea") { result = element.Text; }
                else { result = element.Text; }
            }
            catch { result = element.GetAttribute("innerHTML"); }
            finally
            {
                if (string.IsNullOrWhiteSpace(result))
                    throw new Exception($"[Failed] : Getting the element Text");
            }
            return result;
        }

        public static By GetLocator(this IWebElement element)
        {
            var elementProxy = RemotingServices.GetRealProxy(element);
            var bysFromElement = (IReadOnlyList<object>)elementProxy
                .GetType()
                .GetProperty("Bys", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                .GetValue(elementProxy);

            return (By)bysFromElement[0];
        }

        public static By GetLocator(this IList<IWebElement> elements)
        {
            var elementProxy = RemotingServices.GetRealProxy(elements);
            var bysFromElement = (IReadOnlyList<object>)elementProxy
                .GetType()
                .GetProperty("Bys", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?
                .GetValue(elementProxy);

            return (By)bysFromElement[0];
        }        

        public static IWebDriver WrapsDriver(this IWebElement element)
        {
            try
            {
                Thread.Sleep(200);
                IWebDriver driver = (element as IWrapsDriver ?? (IWrapsDriver)((IWrapsElement)element).WrappedElement).WrappedDriver;
                return driver;
            }
            catch { throw; }            
        }

        public static void RefreshPage(this IWebDriver driver)
        {
            (driver as IJavaScriptExecutor).ExecuteScript("location.reload();");
        }

        #region Java Executor

        private static void JavaScriptExecutor(string pattern, IWebElement element)
        {
            var js = element.WrapsDriver() as IJavaScriptExecutor;
            js.ExecuteScript(pattern, element);
        }

        private static class JSOperator
        {
            public static string Click { get { return "arguments[0].click();"; } }
            public static string Clear { get { return "arguments[0].value = '';"; } }
            public static string SetValue { get { return "arguments[0].value = '{0}';"; } }
            public static string IsDisplayed { get { return "if(parseInt(arguments[0].offsetHeight) > 0 && parseInt(arguments[0].offsetWidth) > 0) return true; return false;"; } }
            public static string ValidateAttribute { get { return "return arguments[0].getAttribute('{0}');"; } }
            public static string ScrollToElement { get { return "arguments[0].scrollIntoView(true);"; } }
            public static string SelectComboBox
            {
                get
                {
                    return String.Format("var length = arguments[0].options.length;  for (var i=0; i<length; i++){{  if (arguments[0].options[i].text == '{0}'){{ arguments[0].selectedIndex = i; break; }} }}");
                }
            }
            public static string GetSelectedComboboxValue
            {
               get { return "return arguments[0].options[arguments[0].selectedIndex].text"; }
            }
        }

        #endregion

    }
}
