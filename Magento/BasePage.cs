using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magento
{
    [TestClass]
    public class BasePage
    {
        public static IWebDriver driver;

        public static void SeleniumInit(string browser)
        {
            if (browser == "Chrome")
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--start-maximized");
                chromeOptions.AddArguments("--imcognito");
                driver = new ChromeDriver(chromeOptions);
            }
            else if (browser == "FireFox")
            {
                FirefoxOptions firefoxOptions = new FirefoxOptions();
                firefoxOptions.AddArguments("");
                driver = new FirefoxDriver(firefoxOptions);
            }
            else if (browser == "Microsoft Edge")
            {
                EdgeOptions options = new EdgeOptions();
                options.AddArguments("");
                var service = EdgeDriverService.CreateDefaultService(@"C:\Users\Hashmat\source\repos\Practice-Testing\Practice-Testing\bin\Debug", @"C:\Program Files (x86)\Microsoft\Edge\Application");
                service.Start();
                driver = new EdgeDriver(options);
            }
        }

        public static void Write(By by, string data)
        {
            driver.FindElement(by).SendKeys(data);
        }
        public static void Click(By by)
        {
            driver.FindElement(by).Click();
        }

        public static void Url(string url)
        {
            driver.Url = url;
        }

        public static string GetText(By by)
        {
            return driver.FindElement(by).Text;
        }

    }
}
