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
        Product product = new Product();

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

        [TestMethod]
        public void Valid_Order_Placement()
        {
            product.Add_to_Cart("https://magento.softwaretestingboard.com/");
            product.Checkout();
            string assertion_txt = BasePage.driver.FindElement(By.CssSelector("#maincontent > div.page-title-wrapper > h1 > span")).Text;
            Assert.AreEqual(assertion_txt, "Thank you for your purchase!");

        }

        [TestMethod]
        public void Valid_Update_Cart()
        {
            By piece_price_txt = By.XPath("/html/body/div[2]/header/div[2]/div[1]/div/div/div/div[2]/div[4]/ol/li/div/div/div[2]/div[1]/span/span/span/span");
            By total_price_txt = By.XPath("/html/body/div[2]/header/div[2]/div[1]/div/div/div/div[2]/div[2]/div/span/span");
            int quantity = 3;
            product.Add_to_Cart("https://magento.softwaretestingboard.com/");
            product.UpdateCart(quantity);
            string initial_price = BasePage.driver.FindElement(piece_price_txt).Text;
            initial_price = initial_price.Substring(1, initial_price.Length - 4);
            string updated_price = BasePage.driver.FindElement(total_price_txt).Text;
            updated_price = updated_price.Substring(1, updated_price.Length - 4);
            Assert.AreEqual(updated_price, (Int32.Parse(initial_price)*quantity).ToString());

        }
        [TestMethod]
        public void Valid_Compare_products()
        {
            By product1_name_comparepage = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/table/tbody[1]/tr/td[1]/strong/a");
            By product2_name_comparepage = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/table/tbody[1]/tr/td[2]/strong/a");

            string ProductName_1 = "Proteus Fitness Jackshirt";
            string ProductName_2 = "Argus All-Weather Tank";
            login.Login("https://magento.softwaretestingboard.com/customer/account/login/", "fegom47501@kindomd.com", "Na1matKhan");
            product.Compare_Products(ProductName_1, ProductName_2);

            string retrieved_product1_name = BasePage.driver.FindElement(product1_name_comparepage).Text;
            string retrieved_product2_name = BasePage.driver.FindElement(product2_name_comparepage).Text;
            Assert.AreEqual(ProductName_1, retrieved_product1_name);
            Assert.AreEqual(ProductName_2, retrieved_product2_name);
        }
        [TestMethod]
        public void Valid_Add_to_Wishlist()
        {
            login.Login("https://magento.softwaretestingboard.com/customer/account/login/", "fegom47501@kindomd.com", "Na1matKhan");
            string Product_Name = "Proteus Fitness Jackshirt";
            By wishlist_product_name = By.XPath("/html/body/div[2]/main/div[2]/div[1]/form/div[1]/ol/li/div/strong/a");
            product.Add_to_Wishlist(Product_Name);
            string retrieve_product_name = BasePage.driver.FindElement(wishlist_product_name).Text;
            Assert.AreEqual(retrieve_product_name, Product_Name);
        
        }

    }
}
