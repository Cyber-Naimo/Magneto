using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class ForgetPasswordPage : BasePage
    {
        #region Locators
        By email_input = By.Name("email");
        By submit_button = By.CssSelector(".action.submit.primary");
        By password = By.Name("password");
        By password_confirm = By.Name("password_confirmation");
        #endregion

        #region Methods
        public void SubmitForgotPassword(string email)
        {
            Step = Test.CreateNode("ForgetPasswordPage");
            Write(email_input, email);
            Click(submit_button);
        }

        public void Reset(string url, string new_password, string confirm_password)
        {
            Step = Test.CreateNode("ForgetPasswordPage");
            Url(url);
            Write(password, new_password);
            Write(password_confirm, confirm_password);
            Click(submit_button);

        }

        #endregion
    }
}
