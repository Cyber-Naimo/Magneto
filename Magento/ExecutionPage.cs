using Magento.Magento;
using Mailosaur;
using Mailosaur.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

        private static string baseUrl = "https://magento.softwaretestingboard.com";
        private static string emailToUse = "remain-dear@x8xnajkk.mailosaur.net";


        LoginPage login = new LoginPage();
        RegisterPage register = new RegisterPage();
        MyAccountPage account = new MyAccountPage();

        [TestMethod]
        public void ValidRegisterTestCase()
        {
            register.Signup(baseUrl,"naimat","naimat", emailToUse, "Na1matKhan","Na1matKhan");
            string result = BasePage.driver.FindElement(By.ClassName("base")).Text;
            Assert.AreEqual(result, "My Account");
        }

        [TestMethod]
        public void InValidRegisterTestCase()
        {
            register.Signup(baseUrl, "naimat", "naimat",emailToUse, "Na1matKhan", "Na1matKhan");
            IWebElement element = BasePage.driver.FindElement(By.XPath("//div[contains(text(), 'There is already an account with this email address.')]"));
            Assert.IsTrue(element.Text.Contains("There is already an account with this email address."), "Error message not displayed as expected.");
        }

        

        [TestMethod]
        public void Valid_Login_Test_Case()
        {
            login.Login(baseUrl,emailToUse,"Na1matKhan");
            string result = BasePage.driver.FindElement(By.ClassName("logged-in")).Text;
            Assert.AreEqual(result, "Welcome, naimat naimat!");
        }

        [TestMethod]
        public void UpdatePassword()
        {
            string currentPassword = "Na1matKhan";
            string newPassword = "Na1matKhan";
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.ChangePassword(currentPassword, newPassword);

            login.Login(baseUrl,emailToUse,newPassword);
            string result = BasePage.driver.FindElement(By.ClassName("logged-in")).Text;
            Assert.AreEqual(result, "Welcome, naimat naimat!");
        }

        [TestMethod]
        public void AddAdditionalAdress()
        {
            string phone = "12345";
            string add = "House 22";
            string city = "karachi";
            string province = "Alaska";
            string code = "12345";
            string country = "United States";
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.AddAddress(phone, add, city, province, code,country);
            string result = BasePage.GetText(By.CssSelector("td[data-th='City']"));
            Assert.AreEqual(city, result);
        }
        
        [TestMethod]
        public void TestViewOrderById()
        {

            string orderId = "000028951";
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.ViewOrderById(orderId);
            string orderNumberOnPage = BasePage.GetText(By.CssSelector("span.base"));
            string extractedOrderNumber = orderNumberOnPage.Replace("Order # ", "").Trim();
            Assert.AreEqual(orderId, extractedOrderNumber, $"The order ID on the details page should be {orderId}.");
        }

        [TestMethod]
        public void AddItemtoCartfromWishListPage()
        {
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.AddAllItemtoCart();
            Assert.IsTrue(account.IsWishlistEmpty());
        }

        [TestMethod]
        public void ShareWishList()
        {
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.SendWishlist(emailToUse, "Hello");
            string result = BasePage.GetText(By.XPath("//div[text()='Your wish list has been shared.']"));
            Assert.AreEqual(result, "Your wish list has been shared.");
        }

        [TestMethod]
        public void AddReviewTestCase()
        {
            string name = "Hello";
            string summary = "Good";
            string description = "Nice Product";

            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.AddReviewtoProduct(summary, description);
            string result = BasePage.GetText(By.XPath("//div[contains(text(),'You submitted your review for moderation.')]"));
            Assert.AreEqual(result, "You submitted your review for moderation.");

        }
    }
}
