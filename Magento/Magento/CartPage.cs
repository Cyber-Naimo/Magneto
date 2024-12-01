using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class CartPage : BasePage
    {
        #region locators
        By cart_icon = By.XPath("/html/body/div[2]/header/div[2]/div[1]");
        By edit_cart_icon = By.XPath("/html/body/div[2]/header/div[2]/div[1]/div/div/div/div[2]/div[5]/div/a/span");
        By quantity_input_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[3]/div/div/label/input");
        By update_btn = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[2]/button[2]");
        

        #endregion

        public void UpdateCart(int quantity)
        {
            Step = Test.CreateNode("CartPage");
            Thread.Sleep(2000);
            Click(cart_icon);
            Thread.Sleep(2000);
            Click(edit_cart_icon);
            Thread.Sleep(3000);
            string str_quantity = quantity.ToString();
            IJavaScriptExecutor js = (IJavaScriptExecutor)BasePage.driver;
            js.ExecuteScript("arguments[0].value = '';", BasePage.driver.FindElement(quantity_input_txt));
            Thread.Sleep(1000);
            Write(quantity_input_txt, str_quantity);
            Thread.Sleep(1000);
            Click(update_btn);
            Thread.Sleep(2000);


        }
    }
}
