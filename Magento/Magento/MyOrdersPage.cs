using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magento.Magento
{
    internal class MyOrdersPage:BasePage
    {
        #region Locators
        By view_orders = By.XPath("//a[@class='action view' and @href='https://magento.softwaretestingboard.com/sales/order/history/']");

        // ViewOrderById
        By myOrderLink = By.LinkText("My Orders");
        By ViewOrderLink = By.LinkText("View Order");

        #endregion

        #region Methods

        LoginPage login = new LoginPage();
        MyAccountPage account = new MyAccountPage();    
        public void ViewOrderById()
        {
            Click(myOrderLink);
            Click(ViewOrderLink);
        }

        public void ViewAllOrders()
        {
            Click(view_orders);
        }

        // MyOrdersPage

        [TestMethod]
        public void TestViewOrderById()
        {

            string orderId = "000028951";
            login.Login(baseUrl, emailToUse, "Na1matKhan");
            account.GotoAccountPage();
            ViewOrderById();
            string orderNumberOnPage = BasePage.GetText(By.CssSelector("span.base"));
            string extractedOrderNumber = orderNumberOnPage.Replace("Order # ", "").Trim();
            Assert.AreEqual(orderId, extractedOrderNumber, $"The order ID on the details page should be {orderId}.");
        }
        #endregion

    }
}
