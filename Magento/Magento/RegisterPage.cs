﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class RegisterPage : BasePage
    {
        #region Locators
        By header_Sign_up_link = By.LinkText("Create an Account");
        By first_name_tag = By.Id("firstname");
        By last_name_tag = By.Id("lastname");
        By email_tag = By.Name("email");
        By pass_tag = By.Id("password");
        By confirm_pass_tag = By.Name("password_confirmation");
        By signup_btn = By.XPath("//button[@title='Create an Account']");

        #endregion

        #region Methods
        public void Signup(string url, string first_name, string last_name, string email, string password, string conf_password)
        {
            Step = Test.CreateNode("RegisterPage");
            Url(url);
            Click(header_Sign_up_link);
            Write(first_name_tag, first_name);
            Write(last_name_tag, last_name);
            Write(email_tag, email);
            Write(pass_tag, password);
            Write(confirm_pass_tag, conf_password);
            Click(signup_btn);
        }


        [TestMethod]
        public void InValidRegisterTestCase()
        {
            Signup(baseUrl, "naimat", "naimat", emailToUse, "Na1matKhan", "Na1matKhan");
            IWebElement element = BasePage.driver.FindElement(By.XPath("//div[contains(text(), 'There is already an account with this email address.')]"));
            Assert.IsTrue(element.Text.Contains("There is already an account with this email address."), "Error message not displayed as expected.");
        }
        #endregion
    }
}
