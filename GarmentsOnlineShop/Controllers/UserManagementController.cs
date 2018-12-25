using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitiesLibrary.UserFolder;
using EntitiesLibrary;

namespace GarmentsOnlineShop.Controllers
{
    public class UserManagementController : Controller
    {
        [HttpGet]
        public ActionResult ManageUsers()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE)))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "UserManagement/ManageUsers" });

            List<User> user = new UserHandler().GetUsers();
            return View(user);
        }
        [HttpGet]
        public ActionResult MakeAdmin(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE)))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "UserManagement/ManageUsers" });

            if (id != 1)
            {
                new UserHandler().GiveAdminAthority(id);
            }
            
            return RedirectToAction("ManageUsers");
        }
        [HttpGet]
        public ActionResult RemoveAdmin(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE)))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "UserManagement/ManageUsers" });

            if (id != 1)
            {
                new UserHandler().RemoveAdminship(id);
            }
            return RedirectToAction("ManageUsers");
            
        }
    }
}