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
    public class ProductPage : BasePage
    {
        #region Locators
        By category_men = By.XPath("//*[@id=\"ui-id-5\"]");
        By type_men_jackets = By.XPath("/html/body/div[2]/main/div[4]/div[2]/div[2]/div/ul[1]/li[2]/a");
        By style_men = By.XPath("//*[@id=\"narrow-by-list\"]/div[1]/div[1]");
        By filter_jackets_insulated = By.XPath("//*[@id=\"narrow-by-list\"]/div[1]/div[2]/ol/li[1]/a");
        By filter_price_input_txt = By.XPath("/html/body/div[2]/main/div[3]/div[1]/div[2]/div[3]/select");
        By asc_dsc_btn = By.XPath("/html/body/div[2]/main/div[3]/div[1]/div[2]/div[3]/a");
        By size_xs_out = By.XPath("//*[@id=\"option-label-size-143-item-166\"]");
        By color_black_out = By.XPath("//*[@id=\"option-label-color-93-item-49\"]");
        By add_to_cart_out = By.XPath("//*[@id=\"maincontent\"]/div[3]/div[1]/div[3]/ol/li[1]/div/div/div[3]/div/div[1]/form/button");
        By search_product_input_field = By.XPath("/html/body/div[2]/header/div[2]/div[2]/div[2]/form/div[1]/div/input");
        By searb_product_btn = By.XPath("/html/body/div[2]/header/div[2]/div[2]/div[2]/form/div[2]/button");
        By search_product_name = By.XPath("/html/body/div[2]/main/div[3]/div[1]/div[2]/div[2]/ol/li[1]/div/div/strong/a");
        By search_product_widget = By.XPath("/html/body/div[2]/main/div[3]/div[1]/div[2]/div[2]/ol/li[1]/div/a/span/span/img");
        By widget_product_compare_btn = By.CssSelector("#maincontent > div.columns > div > div.product-info-main > div.product-social-links > div > a.action.tocompare");
        By navbar_compare_products_btn = By.XPath("/html/body/div[2]/header/div[2]/ul/li/a");
        By widget_product_wishlist_btn = By.XPath("/html/body/div[2]/main/div[2]/div/div[1]/div[5]/div/a[1]");
        #endregion

        #region Methods
        public void Add_to_Cart(string url)
        {
            Step = Test.CreateNode("ProductPage");
            Url(url);
            Thread.Sleep(2000);
            Click(category_men);
            Thread.Sleep(2000);
            Click(type_men_jackets);
            Thread.Sleep(2000);
            Click(style_men);
            Thread.Sleep(2000);
            Click(filter_jackets_insulated);
            Thread.Sleep(2000);
            Write(filter_price_input_txt, "Price");
            Thread.Sleep(2000);
            Click(asc_dsc_btn);
            Thread.Sleep(2000);
            Click(size_xs_out);
            Thread.Sleep(2000);
            Click(color_black_out);
            Thread.Sleep(2000);
            Click(add_to_cart_out);
            Thread.Sleep(2000);

        }

        

        

        public void Add_to_Wishlist(string Product_Name)
        {
            Step = Test.CreateNode("ProductPage");
            if (Search_Product(Product_Name))
            {
                Click(search_product_widget);
                Thread.Sleep(2000);
                Click(widget_product_wishlist_btn);
                Thread.Sleep(2000);
            }
        }

        public bool Search_Product(string Product_Name)
        {
            Step = Test.CreateNode("ProductPage");
            Write(search_product_input_field, Product_Name);
            Click(searb_product_btn);
            Thread.Sleep(3000);
            string retrieved_product = BasePage.driver.FindElement(search_product_name).Text;
            if (retrieved_product != Product_Name)
            {
                return false;
            }
            return true;
        }

        public void Compare_Products(string ProductName_1, string ProductName_2)
        {
            Step = Test.CreateNode("ProductPage");
            if (Search_Product(ProductName_1))
            {
                Click(search_product_widget);
                Thread.Sleep(2000);
                Click(widget_product_compare_btn);
                Thread.Sleep(2000);
            }

            if (Search_Product(ProductName_2))
            {
                Click(search_product_widget);
                Thread.Sleep(2000);
                Click(widget_product_compare_btn);
                Thread.Sleep(2000);
            }

            Click(navbar_compare_products_btn);
            Thread.Sleep(2000);

        }
        #endregion
    }
}
