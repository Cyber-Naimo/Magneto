using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class CheckoutPage : BasePage
    {
        #region Locators
        By cart_icon = By.XPath("/html/body/div[2]/header/div[2]/div[1]");
        By checkout_btn = By.XPath("//*[@id=\"top-cart-btn-checkout\"]");
        By email_input_text = By.XPath("//*[@id=\"customer-email\"]");
        By fname_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/div[1]/div/input");
        By lname_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/div[2]/div/input");
        By street_address_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/fieldset/div/div[1]/div/input");
        By city_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/div[4]/div/input");
        By postalcode_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/div[7]/div/input");
        By country_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/div[8]/div/select");
        By phone_number_input_text = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[1]/div[2]/form[2]/div/div[9]/div/input");
        By shipping_method_input_btn = By.XPath("/html/body/div[2]/main/div[2]/div/div[2]/div[4]/ol/li[2]/div/div[3]/form/div[1]/table/tbody/tr/td[1]/input");
        By next_btn = By.CssSelector("#shipping-method-buttons-container > div > button");
        By placeOrderBtn = By.CssSelector("span[data-bind=\"i18n: 'Place Order'\"]");

        #endregion

        #region Data
        string email = "tempmail12@gmail.com";
        string fname = "Qasim";
        string lname = "Panhwar";
        string street_address = "Street 110";
        string city = "Karachi";
        string postal_code = "76200";
        string country = "Pakistan";
        string phone_number = "03123456789";
        #endregion

        #region Method
        public void checkout()
        {
            Step = Test.CreateNode("CheckoutPage");
            Thread.Sleep(2000);
            Click(cart_icon);
            Thread.Sleep(2000);
            Click(checkout_btn);
            Thread.Sleep(2000);
            Write(email_input_text, email);
            Thread.Sleep(4000);
            Write(fname_input_text, fname);
            Write(lname_input_text, lname);
            Write(street_address_input_text, street_address);
            Write(city_input_text, city);
            Write(postalcode_input_text, postal_code);
            Write(country_input_text, country);
            Write(phone_number_input_text, phone_number);
            Thread.Sleep(4000);
            Click(shipping_method_input_btn);
            Click(next_btn);
            Thread.Sleep(6000);
            Click(placeOrderBtn);
            Thread.Sleep(5000);
        }

        #endregion

    }
}
