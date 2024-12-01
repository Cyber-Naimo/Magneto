using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magento.Magento
{
    public class LoginPage : BasePage
    {
        #region Locators
        By header_Sign_in_link = By.LinkText("Sign In");
        By email_tag = By.Id("email");
        By pass_tag = By.Id("pass");
        By signin_btn = By.Name("send");
        By forget_pass_btn = By.CssSelector(".action.remind");
        #endregion

        #region Methods
        public void Login(string url, string email, string password)
        {
            Step = Test.CreateNode("LoginPage");
            Url(url);
            Click(header_Sign_in_link);
            Write(email_tag, email);
            Write(pass_tag, password);
            Click(signin_btn);
        }

        public void ClickForgotPassword()
        {
            Click(forget_pass_btn);
        }

        public void ForgotPass(string url)
        {
            Step = Test.CreateNode("LoginPage");
            Url(url);
            Click(header_Sign_in_link);
            Click(forget_pass_btn);

        }

        // Login Page
        [TestMethod]
        public void InValid_Login_Test_Case()
        {
            Login(baseUrl,emailToUse, "Na1matKhan123");
            Thread.Sleep(1000);
            string result = BasePage.driver.FindElement(By.ClassName("logged-in")).Text;
            Assert.AreEqual(result, "Welcome, naimat naimat!");
        }
        #endregion

    }
}
