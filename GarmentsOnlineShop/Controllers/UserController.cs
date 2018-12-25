using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using EntitiesLibrary;
using GarmentsOnlineShop.Models;
using EntitiesLibrary.AddressFolder;
using EntitiesLibrary.UserFolder;
using System.Net.Mail;
using System.Net;
using GarmentsOnlineShop.Models.User;

namespace GarmentsOnlineShop.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Login_Register()
        {
            User u = Session[WebUtil.CURRENT_USER] as User;
            if(u != null)
            {
                if (u.IsInRole(WebUtil.ADMIN_ROLE) || u.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
            }
            string ReturnUrl = Request.QueryString["returnUrl"];
            ViewBag.ReturnUrl = ReturnUrl;
            HttpCookie myCookie = Request.Cookies[WebUtil.MY_COOKIE];
            if (myCookie != null)
            {
                string[] data = myCookie.Value.Split(',');
                User user = new UserHandler().GetUserForLogin(data[0], data[1]);
                if (user != null)
                {
                    myCookie.Expires = DateTime.Today.AddDays(7);
                    Response.SetCookie(myCookie);
                    Session.Add(WebUtil.CURRENT_USER, user);
                    string[] parts = null;
                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        parts = ReturnUrl.Split('/');
                        if (parts.Length == 2) return RedirectToAction(parts[0], parts[1]);
                    }
                    if (user.IsInRole(WebUtil.ADMIN_ROLE) || user.IsInRole(WebUtil.Super_ADMIN_ROLE))
                    {
                        return RedirectToAction("ManageOrders", "OrderManagement");
                    }
                    else
                    {
                        return RedirectToAction("UserViewPage", "UserLayout");
                    }
                }
            }
            //SignUp
            // as you need to select country, province and city from dropdownlist to register.. so if yo are registering for the first time there wont be any city
            // so you wont be able to register. thats why I made this logic so by default there will a city in database
            List<Country> country = new LocationHandler().GetCountryList();
            if (country.Count == 0)
            {
                Country c = new Country();
                c.Name = "Pakistan";
                c.Code = 92;
                new LocationHandler().AddCountry(c);
                Country C = new LocationHandler().GetCountry(c.Name);
                if (C != null)
                {
                    Province p = new Province { Name = "Punjab", Country = new Country { Id = C.Id } };
                    new LocationHandler().AddProvince(p);
                    Province P = new LocationHandler().GetProvince(p.Name, C.Id);
                    if (P != null)
                    {
                        City city = new City { Name = "Lahore", Province = new Province { Id = P.Id } };
                        new LocationHandler().AddCity(city);
                    }
                }
            }      
            
            
            ViewBag.Countries = new LocationHandler().GetCountryList().SelectItemList();
            return View();
        }

        [HttpPost]
        public ActionResult Login_Register(FormCollection data)
        {
            User us = Session[WebUtil.CURRENT_USER] as User;
            if (us != null)
            {
                if (us.IsInRole(WebUtil.ADMIN_ROLE) || us.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
            }

            User u = new UserHandler().GetUser(data["email"]); 
            if (u == null) //for checking whether this email is already registered or not
            {
                try
                {
                    User user = new User();
                    user.FirstName = data["first"];
                    user.LastName = data["last"];
                    Address a = new Address();
                    a.StreetAddress = data["strtadrs"];
                    a.City = new City { Id = Convert.ToInt16(data["CityList"]) };
                    user.Address = a;
                    user.BirthDate = Convert.ToDateTime(data["dob"]);
                    user.ContactNumber = data["Contact"];
                    user.Email = data["email"];
                    user.Password = data["pass"];
                    user.SecurityQuestion = data["que"];
                    user.SecurityAnswer = data["ans"];
                    HttpPostedFileBase DP = Request.Files["DisplayPic"];
                    if (DP != null && DP.ContentLength > 0)
                    {
                        long unique = DateTime.Now.Ticks;
                        string url = "~/images/User/" + unique + DP.FileName.Substring(DP.FileName.LastIndexOf('.'));
                        string path = Server.MapPath(url);
                        DP.SaveAs(path);
                        user.ImageUrl = url;
                    }
                    string emailForSuperUser = (user.Email).ToLower();
                    List<Role> roles = new UserHandler().GetRoles();
                    if (roles.Count == 0)
                    {
                        Role SuperUser = new Role { Name = "Super Admin" };
                        Role Admin = new Role { Name = "Admin" };
                        Role SimpleUser = new Role { Name = "User" };
                        new UserHandler().AddRoll(SuperUser);
                        new UserHandler().AddRoll(Admin);
                        new UserHandler().AddRoll(SimpleUser);
                    }
                    if (emailForSuperUser == "onenaweed@gmail.com")
                    {
                        Role r = new UserHandler().getRole("Super Admin");
                        user.Role = new Role { Id = r.Id };
                    }
                    else
                    {
                        Role r = new UserHandler().getRole("User");
                        user.Role = new Role { Id = r.Id };
                    }
                    //sending email on registration
                    SmtpClient smtpClt = new SmtpClient("smtp.gmail.com", 587);
                    smtpClt.EnableSsl = true;
                    smtpClt.Credentials = new NetworkCredential("naviid904@gmail.com", "ASSALAMOALAIKUM"); // Add emai and password of the id from which you want tp send email
                    smtpClt.Send("naviid904@gmail.com", user.Email, "Welcome Message", $"Dear {user.FirstName} welcome to The Garments online Store");

                    new UserHandler().AddUser(user);
                    TempData.Add("alert", new AlertModel("You are a registered user. Now all you need is to Login.", AlertType.Success));
                }
                catch
                {
                    TempData.Add("alert", new AlertModel("Oops.. Unable to register you. Please try again.", AlertType.Error));
                }
            }
            else
            {
                TempData.Add("alert", new AlertModel("You already have an account.", AlertType.Info));
            }
            return RedirectToAction("Login_Register");
        }
  
        [HttpGet]
        public ActionResult SendProvincesToDropDown(int id)
        {
            DropDownModel ddm = new DropDownModel();
            ddm.Caption = "-Select State-";
            ddm.Name = "ProvincesList";
            ddm.Id = "PList";
            ddm.Values = new LocationHandler().GetProvincesListBaseOnId(id).SelectItemList();
            return PartialView("~/Views/Shared/_DropDown.cshtml", ddm);
        }
        [HttpGet]
        public ActionResult SendCitiesToDropDown(int id)
        {
            DropDownModel ddm = new DropDownModel();
            ddm.Caption = "-Select City-";
            ddm.Id = "CList";
            ddm.Name = "CityList";
            ddm.Values = new LocationHandler().GetCitiesList(id).SelectItemList();
            return PartialView("~/Views/Shared/_DropDown.cshtml",ddm);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            User us = Session[WebUtil.CURRENT_USER] as User;
            if (us != null)
            {
                if (us.IsInRole(WebUtil.ADMIN_ROLE) || us.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
            }
            User user = new UserHandler().GetUserForLogin(model.LoginId, model.Password);
            if(user!=null)
            {
                Session.Add(WebUtil.CURRENT_USER, user);
                if (model.RememberMe)
                {
                    HttpCookie myCookie = new HttpCookie(WebUtil.MY_COOKIE);
                    myCookie.Expires = DateTime.Today.AddDays(7);
                    myCookie.Value = $"{model.LoginId},{model.Password}";
                    Response.SetCookie(myCookie);
                }
                string returnURL = Request.QueryString["rurl"];
                string[] parts = null;
                if (!string.IsNullOrWhiteSpace(returnURL))
                {
                    parts = returnURL.Split('/');
                    if (parts.Length == 2) return RedirectToAction(parts[1], parts[0]);
                }
                if (user.IsInRole(WebUtil.ADMIN_ROLE) || user.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
            }
            else
            {
                TempData.Add("alertLoginFailed", new AlertModel("Incorrect LoginId or Password... Please try again...", AlertType.Error));
                return RedirectToAction("Login_Register");
            }
        }


        [HttpGet]
        public ActionResult logout()
        {
            User user = Session[WebUtil.CURRENT_USER] as User;
            if(user== null)
            {
                return RedirectToAction("Login_Register");
            }
            HttpCookie cookie = Request.Cookies[WebUtil.MY_COOKIE];
            if(cookie!= null)
            {
                cookie.Expires = DateTime.Now;
                Response.SetCookie(cookie);
            }
            Session.Remove(WebUtil.CURRENT_USER);
            return RedirectToAction("Login_Register");
        }


        [HttpGet]
        public ActionResult AccountSetting()
        {
            User user = Session[WebUtil.CURRENT_USER] as User;
            if(user== null)
            {
                return RedirectToAction("Login_Register");
            }
            AccountSettingModel model = new AccountSettingModel();
            ViewBag.Countries = new LocationHandler().GetCountryList();
            ViewBag.Provinces = new LocationHandler().GetProvincesListBaseOnId(user.Address.City.Province.Country.Id);
            ViewBag.Cities = new LocationHandler().GetCitiesList(user.Address.City.Province.Id);
            return View();
        }
        [HttpPost]
        public ActionResult AccountSetting(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if(currentUser== null)
            {
                return RedirectToAction("Login_Register");
            }
            try
            {
                
                User user = new User();
                user.FirstName = data["FirstName"];
                user.LastName = data["LastName"];
                user.ContactNumber = data["ContactNumber"];
                Address a = new Address();
                a.StreetAddress = data["StreetAddress"];
                a.City = new City { Id = Convert.ToInt16(data["CityList"]) };
                user.Address = a;
                new UserHandler().UpdateUser(user, currentUser.Id);
                User temp = new UserHandler().GetUserForRenewSession(currentUser.Id);
                Session.Remove(WebUtil.CURRENT_USER);
                Session.Add(WebUtil.CURRENT_USER, temp);
                if (temp.IsInRole(WebUtil.ADMIN_ROLE) || temp.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
                
            }
            catch
            {
                TempData.Add("failed", new AlertModel("Failed to update your info..! Please try again.", AlertType.Error));

                return RedirectToAction("AccountSetting");
            }

        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (currentUser == null)
            {
                return RedirectToAction("Login_Register");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (currentUser == null)
            {
                return RedirectToAction("Login_Register");
            }
            try
            {
                if (data["CurrentPassword"] != currentUser.Password)
                {
                    TempData.Add("alert", new AlertModel("You entered wrong password..! Please try again...", AlertType.Error));
                    return RedirectToAction("ChangePassword");
                }
                if (data["NewPassword"] != data["Confirm"])
                {
                    TempData.Add("alert", new AlertModel("Some error occurred..! Please try again...", AlertType.Error));
                    return RedirectToAction("ChangePassword");
                }
                new UserHandler().ChangePassword(data["NewPassword"], currentUser.Id);
                User temp = new UserHandler().GetUserForRenewSession(currentUser.Id);
                Session.Remove(WebUtil.CURRENT_USER);
                Session.Add(WebUtil.CURRENT_USER, temp);
                if (temp.IsInRole(WebUtil.ADMIN_ROLE) || temp.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to change your password..! Please try again...", AlertType.Error));
                return RedirectToAction("ChangePassword");
            }

        }
        [HttpGet]
        public ActionResult ForgotPassword(LoginModel model)
        {

            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (currentUser != null)
            {
                if (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))
                {
                    return RedirectToAction("ManageOrders", "OrderManagement");
                }
                else
                {
                    return RedirectToAction("UserViewPage", "UserLayout");
                }
            }
            try
            {
                User user = new UserHandler().ForgotPass_getUser(model.LoginId);
                SmtpClient smtpclt = new SmtpClient("smtp.gmail.com", 587);
                smtpclt.EnableSsl = true;
                smtpclt.Credentials = new NetworkCredential("naviid904@gmail.com", "ASSALAMOALAIKUM");
                smtpclt.Send("naviid904@gmail.com", user.Email, "Password", $"Dear {user.FirstName} {user.LastName}. Your password is, {user.Password}");
            }
            catch
            {
                return RedirectToAction("Login_Register");
            }

            return RedirectToAction("Login_Register");
        }

    }
    
}