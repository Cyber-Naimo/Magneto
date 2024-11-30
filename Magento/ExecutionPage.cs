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
        ProductPage product = new ProductPage();
        CartPage cart = new CartPage();
        CheckoutPage checkout = new CheckoutPage();
        MyAccountPage account = new MyAccountPage();
        MyOrdersPage orders = new MyOrdersPage();
        MyWishlistPage wishlists = new MyWishlistPage();
            


        // RegisterPage
        [TestMethod]
        public void ValidRegisterTestCase()
        {
            register.Signup(BasePage.baseUrl, "naimat", "naimat", BasePage.emailToUse, "Na1matKhan", "Na1matKhan");
            string result = BasePage.driver.FindElement(By.ClassName("base")).Text;
            Assert.AreEqual(result, "My Account");
        }


        // Login Page
        [TestMethod]
        public void Valid_Login_Test_Case()
        {
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
           Thread.Sleep(1000);
            string result = BasePage.driver.FindElement(By.ClassName("logged-in")).Text;
            Assert.AreEqual(result, "Welcome, naimat naimat!");
        }


        // MyAccountPage
        [TestMethod]
        public void UpdatePassword()
        {
            string currentPassword = "Na1matKhan";
            string newPassword = "Na1matKhan";
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            account.ChangePassword(currentPassword, newPassword);

            login.Login(BasePage.baseUrl, BasePage.emailToUse, newPassword);
            string result = BasePage.driver.FindElement(By.ClassName("logged-in")).Text;
            Assert.AreEqual(result, "Welcome, naimat naimat!");
        }



        // MyOrdersPage

        [TestMethod]
        public void TestViewAllOrders()
        {
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            orders.ViewAllOrders();
            Assert.IsTrue(BasePage.driver.Url.Contains("sales/order/history"), "Failed to navigate to the orders history page.");
        }

        // MyWishlistPage
        [TestMethod]
        public void AddItemtoCartfromWishListPage()
        {
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            wishlists.AddAllItemtoCart();
            Assert.IsTrue(wishlists.IsWishlistEmpty());
        }

        // ProductPage
        [TestMethod]
        public void Valid_Order_Placement()
        {
            product.Add_to_Cart("https://magento.softwaretestingboard.com/");
            checkout.checkout();
            string assertion_txt = BasePage.driver.FindElement(By.CssSelector("#maincontent > div.page-title-wrapper > h1 > span")).Text;
            Assert.AreEqual(assertion_txt, "Thank you for your purchase!");

        }

        [TestMethod]
        public void Valid_Update_Cart()
        {
            By piece_price_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[2]/span/span/span");
            By total_price_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[4]/span/span/span");
            By checkout_btn = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/div[1]/ul/li[1]/button");

            int quantity = 3;
            product.Add_to_Cart("https://magento.softwaretestingboard.com/");
            cart.UpdateCart(quantity);
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
    }
}
