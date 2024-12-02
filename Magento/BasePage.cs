using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;

namespace Magento
{
    [TestClass]
    public class BasePage
    {
        #region Variables
        public static IWebDriver driver;
        public static string baseUrl = "https://magento.softwaretestingboard.com";
        public static string emailToUse = "remain-dear@x8xnajkk.mailosaur.net";
        public static ExtentReports extentReports;
        public static ExtentTest Test;
        public static ExtentTest Step;
        #endregion


        #region Methods
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

        public void Write(By by, string data)
        {
            try
            {
                driver.FindElement(by).SendKeys(data);
                TakeScreenshots(Status.Pass, data + "Write Successfully");
            }
            catch (Exception ex)
            {
                TakeScreenshots(Status.Fail, "Failed to Write data" +ex);
            }
            
        }
        public void Click(By by)
        {
            try
            {
                driver.FindElement(by).Click();
                TakeScreenshots(Status.Pass,"Clicked Successfully");
            }
            catch (Exception ex)
            {
                TakeScreenshots(Status.Fail, "Failed to Click" + ex);
            }
            
        }

        public void Url(string url)
        {

            driver.Url = url;
        }

        public static string GetText(By by)
        {
            return driver.FindElement(by).Text;
        }

        public static void CreateReport(string path)
        {
            extentReports = new ExtentReports();
            var sparkReport = new ExtentSparkReporter(path);
            extentReports.AttachReporter(sparkReport);
        }

        public static void TakeScreenshots(Status status,string stepDetails)
        {
            string path = @"E:\University\sem7\Software Testing\project\Magneto\ExtentReports\images\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            System.IO.File.WriteAllBytes(path, screenshot.AsByteArray);
            Step.Log(status,stepDetails,MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build());

        }
        #endregion
    }
}
