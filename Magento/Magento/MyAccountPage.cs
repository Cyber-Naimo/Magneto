using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace Magento.Magento
{
    internal class MyAccountPage : BasePage
    {
        #region Locators

        // GotoAccountPage
        By switchButton = By.CssSelector("button.action.switch[data-action='customer-menu-toggle']");
        By myAccountLink = By.LinkText("My Account");

        // AddAddress
        By addNewAddressButton = By.CssSelector("button.action.primary.add[role='add-address']");
        By manageAddressesLink = By.LinkText("Manage Addresses");
        
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

        // View All reviews
        By view_review = By.XPath("//a[@class='action view' and @href='https://magento.softwaretestingboard.com/review/customer/']");

        #endregion

        #region Objects
        LoginPage login = new LoginPage();
        #endregion

        #region Methods

        public void AddAddress(string phone, string addr, string city, string province, string zip_code, string country)
        {
            Step = Test.CreateNode("AccountPage");
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
            Step = Test.CreateNode("AccountPage");
            Click(switchButton);
            Click(myAccountLink);
        }
        public void ChangePassword(string currentPassword, string newPassword)
        {
            Step = Test.CreateNode("AccountPage");

            Click(changePasswordLink);

            Write(currentPasswordField, currentPassword);

            Write(newPasswordField, newPassword);
            Write(confirmPasswordField, newPassword);

            Click(saveButton);

            string actualMessage = GetText(successMessageLocator);
            Assert.AreEqual("You saved the account information.", actualMessage, "Password change success message did not match.");
        }

        public void ViewAllReviews()
        {
            Step = Test.CreateNode("AccountPage");
            Click(view_review);
        }



        [TestMethod]
        public void AddAdditionalAdress()
        {
            string phone = "12345";
            string add = "House 22";
            string city = "karachi";
            string province = "Alaska";
            string code = "12345";
            string country = "United States";
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            GotoAccountPage();
            AddAddress(phone, add, city, province, code, country);
            string result = BasePage.GetText(By.CssSelector("td[data-th='City']"));
            Assert.AreEqual(city, result);
        }

        [TestMethod]
        public void TestViewAllReviews()
        {
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            GotoAccountPage();
            ViewAllReviews();
            Assert.IsTrue(BasePage.driver.Url.Contains("review/customer"), "Failed to navigate to the reviews page.");
        }








        #endregion
    }
}
