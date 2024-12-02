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
    internal class MyWishlistPage:BasePage
    {
        #region Locators

        // My Wishlist
        By myWishListLink = By.LinkText("My Wish List");
        By wishlistEmptyMessage = By.CssSelector(".block-wishlist.empty");

        // AddAllItemtoCart
        By addtocart_tag = By.XPath("//button[@title='Add All to Cart']");

        // Shared Wished List
        By shareWishlistButton = By.CssSelector("button[title='Share Wish List']");
        By email_send_tag = By.Name("emails");
        By msg_send_tag = By.Name("message");
        By Submitbtn = By.CssSelector(".action.submit.primary");

        //Add Review to Product
        By specificReviewLink = By.XPath("//a[@href='https://magento.softwaretestingboard.com/savvy-shoulder-tote.html#reviews']");

        By summary_tag = By.Id("summary_field");
        By description = By.Name("detail");
        By submitReviewButton = By.CssSelector("button.action.submit.primary");
        #endregion

        #region Objects
        LoginPage login = new LoginPage();
        MyAccountPage account = new MyAccountPage();
        ProductPage product = new ProductPage();
        #endregion

        #region Methods
        public void AddAllItemtoCart()
        {
            Step = Test.CreateNode("MyWishlistPage");
            Click(myWishListLink);
            Thread.Sleep(1000);
            Click(addtocart_tag);

        }
        public bool IsWishlistEmpty()
        {
            Step = Test.CreateNode("MyWishlistPage");
            return driver.FindElement(wishlistEmptyMessage).Displayed;
        }

        public void SendWishlist(string emails, string message)
        {
            Step = Test.CreateNode("MyWishlistPage");
            Click(myWishListLink);
            Thread.Sleep(5000);
            Click(shareWishlistButton);
            Write(email_send_tag, emails);
            Write(msg_send_tag, message);
            Click(Submitbtn);
        }

        public void ClickRatingStars()
        {
            Step = Test.CreateNode("MyWishlistPage");
            try
            {
                // Find the rating star element (for example, the 5-star rating)
                IWebElement ratingStar = BasePage.driver.FindElement(By.XPath("//label[@for='Rating_5'][@title='5 stars']"));

                // Scroll into view to make sure the element is visible
                ((IJavaScriptExecutor)BasePage.driver).ExecuteScript("arguments[0].scrollIntoView(true);", ratingStar);

                Thread.Sleep(5000);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", ratingStar);
            }
            catch (ElementClickInterceptedException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Retry logic or additional handling if necessary
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void AddReviewtoProduct(string summary, string detail)
        {
            Step = Test.CreateNode("MyWishlistPage");
            Click(myWishListLink);
            Click(specificReviewLink);

            Thread.Sleep(9000);

            ClickRatingStars();
            Thread.Sleep(9000);
            Write(summary_tag, summary);
            Write(description, detail);
            Click(submitReviewButton);
        }

        [TestMethod]
        public void ShareWishList()
        {
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            SendWishlist(emailToUse, "Hello");
            string result = BasePage.GetText(By.XPath("//div[text()='Your wish list has been shared.']"));
            Assert.AreEqual(result, "Your wish list has been shared.");
        }

        [TestMethod]
        public void AddReviewTestCase()
        {
            string summary = "Good";
            string description = "Nice Product";

            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            AddReviewtoProduct(summary, description);
            string result = BasePage.GetText(By.XPath("//div[contains(text(),'You submitted your review for moderation.')]"));
            Assert.AreEqual(result, "You submitted your review for moderation.");

        }

        [TestMethod]
        public void Valid_Add_to_Wishlist()
        {
            login.Login(baseUrl,emailToUse, "Na1matKhan");
            string Product_Name = "Proteus Fitness Jackshirt";
            By wishlist_product_name = By.XPath("/html/body/div[2]/main/div[2]/div[1]/form/div[1]/ol/li/div/strong/a");
            product.Add_to_Wishlist(Product_Name);
            string retrieve_product_name = BasePage.driver.FindElement(wishlist_product_name).Text;
            Assert.AreEqual(retrieve_product_name, Product_Name);
        }
        #endregion
    }
}
