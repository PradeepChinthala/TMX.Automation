using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using Configurations;
using System.Windows.Automation;

namespace TMX.Selenium.WebDriverHelpers
{
    public class WebDriverFactory
    {
        public IWebDriver GetWebDriver(string browser, string Url=null)
        {
            IWebDriver driver;
            Url = Config.SiteUrl;

            //if (!Url.Contains("https://tmx"))
            //     Url = $@"https://{Config.AuthenticateUserName}:{Config.AuthenticatePassword}@{Config.SiteUrl}/site";


            switch (browser.ToLower())
            {
                case "chrome":
                    {
                        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                        ChromeOptions options = new ChromeOptions();
                        options.AddUserProfilePreference("download.prompt_for_download", true);
                        options.AddArgument("--disable-infobars");
                        options.AddArguments("--disable-extensions");
                        options.AddArguments("--disable-popup-blocking");
                        options.AddArguments("--disable-print-preview");
                        driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(120));
                        driver.Manage().Window.Maximize();
                        driver.Navigate().GoToUrl(Url);
                        break;
                    }

                case "ie":
                    {
                        InternetExplorerDriverService service = InternetExplorerDriverService.CreateDefaultService();
                        InternetExplorerOptions options = new InternetExplorerOptions();
                        driver = new InternetExplorerDriver(service, options, TimeSpan.FromSeconds(120));
                        driver.Manage().Window.Maximize();
                        driver.Navigate().Refresh();
                        driver.Navigate().GoToUrl(Url);
                        break;
                    }
                case "firefox":
                    {
                        FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                        FirefoxOptions options = new FirefoxOptions();
                        options.SetPreference("browser.tabs.remote.autostart", false);
                        options.SetPreference("browser.tabs.remote.autostart.1", false);
                        options.SetPreference("browser.tabs.remote.autostart.2", false);
                        driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(120));
                        driver.Manage().Window.Maximize();
                        driver.Navigate().GoToUrl(Url);
                        break;
                    }
                //case "edge":
                //    {
                //        EdgeDriverService service = EdgeDriverService.CreateDefaultService();
                //        EdgeOptions options = new EdgeOptions();
                //        options.AddAdditionalCapability("nativeEvents", true);
                //        options.AddAdditionalCapability("acceptSslCerts", true);
                //        options.AddAdditionalCapability("javascriptEnabled", true);
                //        options.AddAdditionalCapability("INTRODUCE_FLAKINESS_BY_IGNORING_SECURITY_DOMAINS", true);
                //        options.AddAdditionalCapability("takes_screenshot", true);
                //        options.AddAdditionalCapability("cssSelectorsEnabled", true);
                //        driver = new EdgeDriver(service, options, TimeSpan.FromSeconds(120));
                //        break;
                //    }


                default:
                    throw new ArgumentException($"Browser Option {browser} Is Not Valid - Use Chrome, Firefox or IE Instead");                    
            }

            return driver;
        }
        
    }
}
