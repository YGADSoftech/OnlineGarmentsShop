using EntitiesLibrary;
using EntitiesLibrary.AddressFolder;
using GarmentsOnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarmentsOnlineShop.Controllers
{
    public class AddLocationController : Controller
    {
        // GET: AddLocation
        [HttpGet]
        public ActionResult Countries()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE)|| currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            List<Country> c = new LocationHandler().GetCountryList();
            return View(c);
        }


        [HttpPost]
        public ActionResult Countries(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });

            try
            {
                Country c = new Country();
                c.Name = data["name"];
                c.Code = Convert.ToInt16(data["code"]);
                new LocationHandler().AddCountry(c);
                TempData.Add("alert", new AlertModel("Country is Added successfully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to Add the Country", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpGet]
        public ActionResult Provinces(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            List<Province> p = new LocationHandler().GetProvincesListBaseOnId(id);
            return PartialView("~/Views/AddLocation/Provinces.cshtml",p);
        }

        [HttpPost]
        public ActionResult AddProvinces(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                int id = Convert.ToInt16(data["id"]);

                Province p = new Province();
                p.Name = data["name"];

                p.Country = new Country { Id = id };

                new LocationHandler().AddProvince(p);
                TempData.Add("alert", new AlertModel("State is Added Successfully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to add the State", AlertType.Error));
            }
                return RedirectToAction("Countries");
            
        }


        [HttpGet]
        public ActionResult Form()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            return PartialView("~/Views/AddLocation/Form.cshtml");
        }

        [HttpGet]
        public ActionResult Form1()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            return PartialView("~/Views/AddLocation/Form1.cshtml");
        }

        [HttpGet]
        public ActionResult Form2()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            return PartialView("~/Views/AddLocation/Form2.cshtml");
        }

        [HttpGet]
        public ActionResult Form3()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            return PartialView("~/Views/AddLocation/Form3.cshtml");
        }

        [HttpGet]
        public ActionResult Cities(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            List<City> c = new LocationHandler().GetCitiesList(id);
            return PartialView("~/Views/AddLocation/Cities.cshtml", c);
        }

        [HttpPost]
        public ActionResult AddCities(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                int id = Convert.ToInt16(data["id"]);
                City c = new City();
                c.Name = data["name"];
                c.Province = new Province { Id = id };
                new LocationHandler().AddCity(c);
                TempData.Add("alert", new AlertModel("City Added SuccessFully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to Add city", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpPost]
        public ActionResult EditCountries(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                int idtoSearch = Convert.ToInt16(data["id"]);
                Country c = new Country { Code = Convert.ToInt16(data["code"]), Name = data["name"] };
                new LocationHandler().UpdateCountry(c, idtoSearch);
                TempData.Add("alert", new AlertModel("Country Updated Successfully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("failed to update country", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpGet]
        public ActionResult DelCountry(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                new LocationHandler().DelCountry(id);
                TempData.Add("alert", new AlertModel("Successfully deleted country", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("failed to delete country", AlertType.Error));
            }
                return RedirectToAction("Countries");
        }

        [HttpPost]
        public ActionResult EditProvinces(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                int idToSearch = Convert.ToInt16(data["id"]);
                Province p = new Province { Name = data["name"] };
                new LocationHandler().UpdateProvince(p, idToSearch);
                TempData.Add("alert", new AlertModel("State updated successfully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to update state", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpGet]
        public ActionResult DelProvince(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                new LocationHandler().DelProvince(id);
                TempData.Add("alert", new AlertModel("State deleted successfully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to delete state", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }

        [HttpPost]
        public ActionResult EditCity(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                int idToSearch = Convert.ToInt16(data["id"]);
                City c = new City { Name = data["name"] };
                new LocationHandler().UpdateCity(c, idToSearch);
                TempData.Add("alert", new AlertModel("City updated Succcessfully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to update city", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }


        [HttpGet]
        public ActionResult DelCity(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "AddLocation/Countries" });
            try
            {
                new LocationHandler().DelCity(id);
                TempData.Add("alert", new AlertModel("City deleted SuccessFully", AlertType.Success));
            }
            catch(Exception ex)
            {
                TempData.Add("alert", new AlertModel("Failed to delete City", AlertType.Error));
            }
            return RedirectToAction("Countries");
        }
    }
}
