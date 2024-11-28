using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class LoginPage:BasePage
    {
        #region Locators
        By email_tag = By.Id("email");
        By pass_tag = By.Id("pass");
        By signin_btn = By.Name("send");
        By forget_pass_btn = By.CssSelector("remind");
        #endregion

        #region Methods
        public void Login(string url,string email, string password)
        {
            Url(url);
            Write(email_tag, email);
            Write(pass_tag, password);
            Click(signin_btn);
        }

        public void ClickForgotPassword()
        {
            Click(forget_pass_btn);
        }

        #endregion


    }
}
