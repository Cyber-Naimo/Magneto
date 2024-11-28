using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class ForgetPasswordPage:BasePage
    {
        #region Locators
        By email_input = By.Name("email"); 
        By submit_button = By.CssSelector("submit");
        #endregion

        #region Methods
        public void SubmitForgotPassword(string email)
        {
            Write(email_input, email);  
            Click(submit_button);     
        }
        #endregion
    }
}
