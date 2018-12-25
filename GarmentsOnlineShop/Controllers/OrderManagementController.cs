using EntitiesLibrary;
using GarmentsOnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarmentsOnlineShop.Controllers
{
    public class OrderManagementController : Controller
    {
        [HttpGet]
        public ActionResult MyOrders()
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if (!(u != null && (u.IsInRole(WebUtil.USER_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "/OrderManagement/MyOrders" });
            List<Orders> orders = new OrderHandler().GetOrdersOfAUser(u.Id);
            return View(orders);
        }
        [HttpGet]
        public ActionResult ManageOrders()
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if (!(u != null && (u.IsInRole(WebUtil.Super_ADMIN_ROLE)) || (u.IsInRole(WebUtil.ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "/OrderManagement/ManageOrders" });

            List<Orders> orders = new OrderHandler().GetOrdersForManagement();
            return View(orders);
        }
        [HttpGet]
        public ActionResult SendOrder(int id)
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if (!(u != null && (u.IsInRole(WebUtil.Super_ADMIN_ROLE)) || (u.IsInRole(WebUtil.ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "/OrderManagement/ManageOrders" });
            try
            {
                new OrderHandler().SendOrder(id);
                TempData.Add("alert", new AlertModel("Order state successfully changed into 'Sent'..!", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to changed the order state..!", AlertType.Error));
            }
            return RedirectToAction("ManageOrders");
        }
        [HttpGet]
        public ActionResult CancelOrder(int id)
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if (!(u != null && (u.IsInRole(WebUtil.Super_ADMIN_ROLE)) || (u.IsInRole(WebUtil.ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "/OrderManagement/ManageOrders" });
            try
            {
                new OrderHandler().CancelOrder(id);
                TempData.Add("alert", new AlertModel("Order state successfully changed from 'pending' to 'cancelled'..!", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to changed the order state..!", AlertType.Error));
            }
            return RedirectToAction("ManageOrders");
        }
    }
    
}