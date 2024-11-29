using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Magento.Magento
{
    internal class MyAccountPage:BasePage
    {
        #region Locators

        // GotoAccountPage
        By switchButton = By.CssSelector("button.action.switch[data-action='customer-menu-toggle']");
        By myAccountLink = By.LinkText("My Account");

        
        // My Wishlist
        By myWishListLink = By.LinkText("My Wish List");

        // AddAddress
        By addNewAddressButton = By.CssSelector("button.action.primary.add[role='add-address']");
        By manageAddressesLink = By.LinkText("Manage Addresses");
        By wishlistEmptyMessage = By.CssSelector(".block-wishlist .empty");
        By phone_tag = By.Id("telephone");
        By address_tag = By.Id("street_1");
        By city_tag = By.Id("city");
        By state_tag = By.Id("region_id");
        By zip_code_tag = By.Id("zip");
        By country_tag = By.Id("country");
        By billing_addr = By.Id("primary_billing");
        By shipping_addr = By.Id("primary_shipping");


        // ChangePassword
        By changePasswordLink = By.CssSelector(".action.change-password");
        By currentPasswordField = By.Id("current-password");
        By newPasswordField = By.Id("password");
        By confirmPasswordField = By.Id("password-confirmation");
        By saveButton = By.CssSelector(".action.save.primary");
        By successMessageLocator = By.XPath("//div[contains(text(), 'You saved the account information.')]");


        // ViewOrderById
        By myOrderLink = By.LinkText("My Orders");
        By ViewOrderLink = By.LinkText("View Order");

        // AddAllItemtoCart
        By addtocart_tag = By.XPath("//button[@title='Add All to Cart']");

        // Shared Wished List
        By shareWishlistButton = By.CssSelector("button[title='Share Wish List']");
        By email_send_tag = By.Name("emails");
        By msg_send_tag = By.Name("message");
        By Submitbtn = By.CssSelector(".action.submit.primary");

        //Add Review to Product
        By specificReviewLink = By.XPath("//a[@href='https://magento.softwaretestingboard.com/savvy-shoulder-tote.html#reviews']");
        By rating5Label = By.XPath("//label[@for='Rating_5'][@title='5 stars']");
        By nickname = By.Name("nickname");
        By summary_tag = By.Id("summary_field");
        By description = By.Name("detail");
        By submitReviewButton = By.CssSelector("button.action.submit.primary");
        By ReviewsuccessMessage = By.XPath("//div[contains(text(),'You submitted your review for moderation.')]");


        #endregion

        #region Methods

        public void AddAddress(string phone,string addr,string city,string province,string zip_code,string country)
        {
            Click(manageAddressesLink);
            Click(addNewAddressButton);
            Write(phone_tag, phone);
            Write(address_tag, addr);
            Write(city_tag, city);
            Write(state_tag, province);
            Write(zip_code_tag, zip_code);
            Write(country_tag, country);
            Click(billing_addr);
            Click(shipping_addr);
            Click(saveButton);

        }

        public void GotoAccountPage()
        {
            Click(switchButton);
            Click(myAccountLink);
        }
        public void ChangePassword(string currentPassword, string newPassword)
        {
            Click(changePasswordLink);

            Write(currentPasswordField, currentPassword);

            Write(newPasswordField, newPassword);
            Write(confirmPasswordField, newPassword);
     
            Click(saveButton);

            string actualMessage = GetText(successMessageLocator);
            Assert.AreEqual("You saved the account information.", actualMessage, "Password change success message did not match.");
        }

        public void ViewOrderById(string orderId)
        {
            Click(myOrderLink);
            Click(ViewOrderLink);
        }
      
        public void AddAllItemtoCart()
        {
            Click(myWishListLink);
            Click(addtocart_tag);

        }
        public bool IsWishlistEmpty()
        {
            return driver.FindElement(wishlistEmptyMessage).Displayed;
        }

        public void SendWishlist(string emails,string message)
        {
            Click(myWishListLink);
            Thread.Sleep(5000);
            Click(shareWishlistButton);
            Write(email_send_tag, emails);
            Write(msg_send_tag, message);
            Click(Submitbtn);
        }

        public void ClickRatingStars()
        {
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
            Click(myWishListLink);
            Click(specificReviewLink);

            Thread.Sleep(9000);

            ClickRatingStars();
            Thread.Sleep(9000);
            Write(summary_tag, summary);
            Write(description, detail);
            Click(submitReviewButton);
        }



        #endregion
    }
}
