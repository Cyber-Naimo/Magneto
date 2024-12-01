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
            string resultFile = @"E:\University\sem7\Software Testing\project\Magneto\ExtentReports\Execution_log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
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

        // RegisterPage
        [TestMethod]
        [TestCategory("FlowTest")]
        // [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "Valid_Register_Test_Case", DataAccessMethod.Sequential)]
        public void ValidRegisterTestCase()
        {
            //string url = TestContext.DataRow["url"].ToString();
            //string firstName = TestContext.DataRow["firstName"].ToString();
            //string lastName = TestContext.DataRow["lastName"].ToString();
            //string email = TestContext.DataRow["email"].ToString();
            //string password = TestContext.DataRow["password"].ToString();
            //string confirmPassword = TestContext.DataRow["confirmPassword"].ToString();
            //register.Signup(BasePage.baseUrl, firstName, lastName, email, password, confirmPassword);

            register.Signup(BasePage.baseUrl, "naimat", "naimat", BasePage.emailToUse, "Na1matKhan", "Na1matKhan");
            string result = BasePage.driver.FindElement(By.ClassName("base")).Text;
            //Assert.AreEqual(result, "My Account");
            Assert.AreEqual(result, "Create New Customer Account");
        }


        // Login Page
        [TestMethod]
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
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "Update_Password_Test_Case", DataAccessMethod.Sequential)]

        public void UpdatePassword()
        {
            //string currentPassword = TestContext.DataRow["currentPassword"].ToString();
            //string newPassword = TestContext.DataRow["newPassword"].ToString();
            //string url = TestContext.DataRow["url"].ToString();
            //string email = TestContext.DataRow["email"].ToString();
            //string password = TestContext.DataRow["password"].ToString();
            //login.Login(BasePage.baseUrl, BasePage.emailToUse, currentPassword);

            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            Thread.Sleep(1000);
            account.GotoAccountPage();
            //account.ChangePassword(currentPassword, newPassword);
            account.ChangePassword("Na1matKhan","Na1matKhan");

            //login.Login(BasePage.baseUrl, BasePage.emailToUse, newPassword);
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            Thread.Sleep(1000);
            string result = BasePage.driver.FindElement(By.ClassName("logged-in")).Text;
            Assert.AreEqual(result, "Welcome, naimat naimat!");
        }



        // MyOrdersPage

        [TestMethod]
        [TestCategory("FlowTest")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "View_All_Orders_Test_Case", DataAccessMethod.Sequential)]
        public void TestViewAllOrders()
        {
            //string url = TestContext.DataRow["url"].ToString();
            //string email = TestContext.DataRow["email"].ToString();
            //string password = TestContext.DataRow["password"].ToString();
            //login.Login(url, email, password);
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            orders.ViewAllOrders();
            Assert.IsTrue(BasePage.driver.Url.Contains("sales/order/history"), "Failed to navigate to the orders history page.");
        }

        // MyWishlistPage
        [TestMethod]
        [TestCategory("FlowTest")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "Add_Item_To_Cart_From_Wishlist_Test_Case", DataAccessMethod.Sequential)]
        public void AddItemtoCartfromWishListPage()
        {
            //string url = TestContext.DataRow["url"].ToString();
            //string email = TestContext.DataRow["email"].ToString();
            //string password = TestContext.DataRow["password"].ToString();
            //login.Login(url, email, password);
            login.Login(BasePage.baseUrl, BasePage.emailToUse, "Na1matKhan");
            Thread.Sleep(1000);
            account.GotoAccountPage();
            wishlists.AddAllItemtoCart();
            Assert.IsTrue(wishlists.IsWishlistEmpty());
        }

        // ProductPage
        [TestMethod]
        [TestCategory("FlowTest")]
        // [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "Valid_Order_Placement_Test_Case", DataAccessMethod.Sequential)]
        public void Valid_Order_Placement()
        {
            //string productUrl = TestContext.DataRow["productUrl"].ToString();
            //string expectedConfirmationText = TestContext.DataRow["expectedConfirmationText"].ToString();
            //product.Add_to_Cart(productUrl);
            product.Add_to_Cart("https://magento.softwaretestingboard.com/");
            checkout.checkout();
            string assertion_txt = BasePage.driver.FindElement(By.CssSelector("#maincontent > div.page-title-wrapper > h1 > span")).Text;
            //Assert.AreEqual(assertion_txt, expectedConfirmationText);
            Assert.AreEqual(assertion_txt, "Thank you for your purchase!");

        }

        [TestMethod]
        [TestCategory("FlowTest")]
        // [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "Valid_Update_Cart_Test_Case", DataAccessMethod.Sequential)]
        public void Valid_Update_Cart()
        {
            By piece_price_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[2]/span/span/span");
            By total_price_txt = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/form/div[1]/table/tbody/tr[1]/td[4]/span/span/span");
            By checkout_btn = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/div[1]/ul/li[1]/button");

            //string productUrl = TestContext.DataRow["productUrl"].ToString();
            //int quantity = int.Parse(TestContext.DataRow["quantity"].ToString());
            //product.Add_to_Cart(productUrl);
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
        [TestCategory("FlowTest")]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "Valid_Compare_Products_Test_Case", DataAccessMethod.Sequential)]
        public void Valid_Compare_products()
        {
            By product1_name_comparepage = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/table/tbody[1]/tr/td[1]/strong/a");
            By product2_name_comparepage = By.XPath("/html/body/div[2]/main/div[3]/div/div[2]/table/tbody[1]/tr/td[2]/strong/a");

            //string ProductName_1 = TestContext.DataRow["ProductName1"].ToString();
            //string ProductName_2 = TestContext.DataRow["ProductName2"].ToString();
            //string url = TestContext.DataRow["url"].ToString();
            //string email = TestContext.DataRow["email"].ToString();
            //string password = TestContext.DataRow["password"].ToString();

            string ProductName_1 = "Proteus Fitness Jackshirt";
            string ProductName_2 = "Argus All-Weather Tank";
            
            //login.Login(url, email, password);
            login.Login(BasePage.baseUrl,BasePage.emailToUse, "Na1matKhan");
            product.Compare_Products(ProductName_1, ProductName_2);

            string retrieved_product1_name = BasePage.driver.FindElement(product1_name_comparepage).Text;
            string retrieved_product2_name = BasePage.driver.FindElement(product2_name_comparepage).Text;
            Assert.AreEqual(ProductName_1, retrieved_product1_name);
            Assert.AreEqual(ProductName_2, retrieved_product2_name);
        }
        #endregion
    }
}
