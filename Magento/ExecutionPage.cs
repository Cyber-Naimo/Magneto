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
using Mailosaur;
using Mailosaur.Models;
using OpenQA.Selenium.Support.UI;

namespace Magento
{
    [TestClass]
    public class ExecutionPage
    {
        #region Setup and Cleanups
        public TestContext instance;
        public  TestContext TestContext
        {
            set { instance = value; }
            get { return instance; }
        }

        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext context)
        {
            string resultFile = @"C:\Users\Hashmat\source\repos\Magento\ExtentReports\Execution_log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
            BasePage.CreateReport(resultFile);
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            BasePage.extentReports.Flush();
        }
        [TestInitialize()]
        public void TestInitialize()
        {
            BasePage.SeleniumInit(ConfigurationManager.AppSettings["Browser"].ToString());
            BasePage.Test = BasePage.extentReports.CreateTest(TestContext.TestName);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            BasePage.driver.Close();
        }
        #endregion

        #region Objects

        LoginPage login = new LoginPage();
        RegisterPage register = new RegisterPage();
        ForgetPasswordPage forgetPassword = new ForgetPasswordPage();
        ProductPage product = new ProductPage();
        CartPage cart = new CartPage();
        CheckoutPage checkout = new CheckoutPage();
        MyAccountPage account = new MyAccountPage();
        MyOrdersPage orders = new MyOrdersPage();
        MyWishlistPage wishlists = new MyWishlistPage();
        

        #endregion

        #region Methods

        
        
        [TestMethod]
        [TestCategory("FlowTest")]
        public void ValidRegisterTestCase()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Valid_Register_Test_Case");

            foreach (var item in testData)
            {
                string url = item["BaseUrl"];
                string firstName = item["FirstName"];
                string lastName = item["LastName"];
                string email = item["Email"];
                string password = item["Password"];
                string confirmPassword = item["ConfirmPassword"];
                string expectedText = item["ExpectedText"];

                // Perform the registration
                register.Signup(url, firstName, lastName, email, password, confirmPassword);

                // Wait for the page to load and verify the result
                WebDriverWait wait = new WebDriverWait(BasePage.driver, TimeSpan.FromSeconds(10));
                IWebElement confirmationMessage = wait.Until(driver => driver.FindElement(By.ClassName("base")));
                string result = confirmationMessage.Text;

                // Assert the expected result
                Assert.AreEqual(result, expectedText);
            }
        }


        // Login Page
        [TestMethod]
        [TestCategory("FlowTest")]
        public void Valid_Login_Test_Case()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Valid_Login_Test_Case");
            
            foreach (var item in testData)
            {
                string url = item["BaseUrl"];
                string email = item["Email"];
                string password = item["Password"];
                login.Login(url, email, password);
                WebDriverWait wait = new WebDriverWait(BasePage.driver, TimeSpan.FromSeconds(10));
                IWebElement passwordReset = wait.Until(driver => driver.FindElement(By.ClassName("logged-in")));
                string result = passwordReset.Text;
                Assert.AreEqual(result, item["ExpectedText"]);
            }
        }

        [TestMethod]
        [TestCategory("FlowTest")]
        public void Forget_Password_Test_Case()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Forget_Password_Test_Case");

            foreach (var item in testData)
            {
                string url = item["BaseUrl"];
                string mail = item["Email"];
                string password = item["Password"];
                string apiKey = item["ApiKey"];
                string serverId = item["ServerId"];

                login.ForgotPass(url);
                forgetPassword.SubmitForgotPassword(mail);

                var mailosaur = new MailosaurClient(apiKey);

                // Wait for an email to arrive, matching this search criteria
                var criteria = new SearchCriteria()
                {
                    SentTo = mail
                };
                var email = mailosaur.Messages.Get(serverId, criteria);
                var resetLink = email.Html.Links[1].Href;

                forgetPassword.Reset(resetLink, password, password);

                WebDriverWait wait = new WebDriverWait(BasePage.driver, TimeSpan.FromSeconds(10));
                IWebElement passwordReset = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"maincontent\"]/div[2]/div[2]/div/div/div")));
                string result = passwordReset.Text;

                Assert.AreEqual(result, item["ExpectedMessage"]);
            }
        }


        // MyAccountPage

        [TestMethod]
        [TestCategory("FlowTest")]
        public void UpdatePassword()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Update_Password_Test_Case");

            foreach (var item in testData)
            {
                string url = item["BaseUrl"];
                string email = item["Email"];
                string currentPassword = item["CurrentPassword"];
                string newPassword = item["NewPassword"];
                string expectedText = item["ExpectedText"];

                login.Login(url, email, currentPassword);

                WebDriverWait wait = new WebDriverWait(BasePage.driver, TimeSpan.FromSeconds(10));
                IWebElement loggedInElement = wait.Until(driver => driver.FindElement(By.ClassName("logged-in")));

                account.GotoAccountPage();
                account.ChangePassword(currentPassword, newPassword);

                login.Login(url, email, newPassword);

                IWebElement loggedInWithNewPassword = wait.Until(driver => driver.FindElement(By.ClassName("logged-in")));

                string result = loggedInWithNewPassword.Text;
                Assert.AreEqual(result, expectedText);
            }
        }




        // MyOrdersPage

        [TestMethod]
        [TestCategory("FlowTest")]
        public void TestViewAllOrders()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "View_All_Orders_Test_Case");

            foreach (var item in testData)
            {
                string url = item["BaseUrl"];
                string email = item["Email"];
                string password = item["Password"];

                // Login with the provided credentials
                login.Login(url, email, password);

                // Navigate to the account page and view all orders
                account.GotoAccountPage();
                orders.ViewAllOrders();

                // Verify if the URL contains the expected orders history page
                Assert.IsTrue(BasePage.driver.Url.Contains("sales/order/history"), "Failed to navigate to the orders history page.");
            }
        }




        // MyWishlistPage

        [TestMethod]
        [TestCategory("FlowTest")]
        public void AddItemtoCartfromWishListPage()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Add_Item_To_Cart_From_Wishlist_Test_Case");

            foreach (var item in testData)
            {
                string url = item["BaseUrl"];
                string email = item["Email"];
                string password = item["Password"];

                // Login with the provided credentials
                login.Login(url, email, password);

                // Navigate to the account page
                account.GotoAccountPage();

                // Add items from the wishlist to the cart
                wishlists.AddAllItemtoCart();

                // Assert that the wishlist is empty after adding the items to the cart
                Assert.IsTrue(wishlists.IsWishlistEmpty());
            }
        }




        // ProductPage
        [TestMethod]
        [TestCategory("FlowTest")]
        public void Valid_Order_Placement()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Valid_Order_Placement_Test_Case");

            foreach (var item in testData)
            {
                string productUrl = item["ProductUrl"];
                string expectedConfirmationText = item["ExpectedConfirmationText"];

                // Add product to cart
                product.Add_to_Cart(productUrl);

                // Proceed to checkout
                checkout.checkout();

                // Assert confirmation message
                string assertion_txt = BasePage.driver.FindElement(By.CssSelector("#maincontent > div.page-title-wrapper > h1 > span")).Text;
                Assert.AreEqual(assertion_txt, expectedConfirmationText);
            }
        }


        [TestMethod]
        [TestCategory("FlowTest")]
        public void Valid_Update_Cart()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Valid_Update_Cart_Test_Case");

            foreach (var item in testData)
            {
                string productUrl = item["ProductUrl"];
                int quantity = int.Parse(item["Quantity"]);

                By piece_price_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[2]/span/span/span");
                By total_price_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[4]/span/span/span");

                // Add product to cart
                product.Add_to_Cart(productUrl);

                // Update cart with the specified quantity
                cart.UpdateCart(quantity);

                // Verify the price
                string initial_price = BasePage.driver.FindElement(piece_price_txt).Text;
                initial_price = initial_price.Substring(1, initial_price.Length - 4);
                string updated_price = BasePage.driver.FindElement(total_price_txt).Text;
                updated_price = updated_price.Substring(1, updated_price.Length - 4);
                Assert.AreEqual(updated_price, (Int32.Parse(initial_price) * quantity).ToString());
            }
        }

        [TestMethod]
        [TestCategory("FlowTest")]
        public void Valid_Compare_Products()
        {
            var testData = XmlHelper.ReadXmlData("TestData.xml", "Valid_Compare_Products_Test_Case");

            foreach (var item in testData)
            {
                string productName1 = item["ProductName1"];
                string productName2 = item["ProductName2"];
                string email = item["Email"];
                string password = item["Password"];

                By product1_name_comparepage = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/table/tbody[1]/tr/td[1]/strong/a");
                By product2_name_comparepage = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/table/tbody[1]/tr/td[2]/strong/a");

                // Login with the provided credentials
                login.Login(BasePage.baseUrl, email, password);

                // Compare the two products
                product.Compare_Products(productName1, productName2);

                // Verify that the product names match
                string retrieved_product1_name = BasePage.driver.FindElement(product1_name_comparepage).Text;
                string retrieved_product2_name = BasePage.driver.FindElement(product2_name_comparepage).Text;
                Assert.AreEqual(productName1, retrieved_product1_name);
                Assert.AreEqual(productName2, retrieved_product2_name);
            }
        }


        #endregion
    }
}
