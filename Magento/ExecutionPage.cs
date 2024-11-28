using Magento.Magento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magento
{
    [TestClass]
    public class ExecutionPage
    {
        #region Setup and Cleanups
        public TestContext instance;

        public TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            BasePage.SeleniumInit(ConfigurationManager.AppSettings["Browser"].ToString());
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            BasePage.driver.Close();
        }
        #endregion

        LoginPage login = new LoginPage();
        RegisterPage register = new RegisterPage();

        [TestMethod]
        public void ValidRegisterTestCase()
        {
            register.Signup("https://magento.softwaretestingboard.com/customer/account/create/","naimat","naimat", "circle-wrong@rvudwee7.mailosaur.net", "Na1matKhan","Na1matKhan");
            Thread.Sleep(5000);
            string result = BasePage.driver.FindElement(By.ClassName("base")).Text;
            Assert.AreEqual(result, "My Account");
        }   

        [TestMethod]
        public void Valid_Login_Test_Case()
        {
            login.Login("https://magento.softwaretestingboard.com/customer/account/login/","fegom47501@kindomd.com","Na1matKhan");
            string result = BasePage.driver.FindElement(By.ClassName("base")).Text;
            Assert.AreEqual(result, "My Account");
        }
    }
}
