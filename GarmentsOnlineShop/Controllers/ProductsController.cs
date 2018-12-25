using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GarmentsOnlineShop.Models.Products;
using GarmentsOnlineShop.Models;
using EntitiesLibrary.Garments;
using EntitiesLibrary;

namespace GarmentsOnlineShop.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult AddColors()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/AddColors" });
            List<Color> colors = new GarmentsHandler().getColors();
            return View(colors);
        }
        [HttpPost]
        public ActionResult AddColors(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/AddColors" });
            try
            {
                Color color = new Color { Name = data["Name"] };
                new GarmentsHandler().AddColor(color);
                TempData.Add("alert", new AlertModel("Color added successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("failed to add color", AlertType.Error));
            }
                return RedirectToAction("AddColors");
        }
        [HttpGet]
        public ActionResult AddFabrics()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/AddFabrics" });
            List<Fabrics> f = new GarmentsHandler().getFabrics();
            return View(f);
        }
        [HttpPost]
        public ActionResult AddFabrics(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/AddFabrics" });
            try
            {
                Fabrics f = new Fabrics { Name = data["Name"] };
                new GarmentsHandler().AddFabrics(f);

                TempData.Add("alert", new AlertModel("Fabric added successfully", AlertType.Success));
            }
            catch
            {

                TempData.Add("alert", new AlertModel("Failed to add fabric", AlertType.Error));
            }
            return RedirectToAction("AddFabrics");
        }
        [HttpGet]
        public ActionResult AddSizes()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/AddSizes" });
            List<Size> s = new GarmentsHandler().getSizes();
            return View(s);
        }
        [HttpPost]
        public ActionResult AddSizes(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/AddSizes" });
            try
            {
                Size s = new Size { Name = data["Name"], Number = Convert.ToInt32(data["num"]) };
                new GarmentsHandler().AddSize(s);
                TempData.Add("alert", new AlertModel("Size added successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to add size", AlertType.Error));
            }
            return RedirectToAction("AddSizes");
        }

        [HttpGet]
        public ActionResult ManageProducts()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/ManageProducts" });
            List<ProductsModel> products = new GarmentsHandler().GetProducts().ToProductSummary();
            return View(products);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/ManageProducts" });
            ViewBag.Departments = new GarmentsHandler().GetDepList().SelectItemList();
            ViewBag.Sizes = new GarmentsHandler().getSizes().SelectItemList();
            ViewBag.Colors = new GarmentsHandler().getColors().SelectItemList();
            ViewBag.Fabrics = new GarmentsHandler().getFabrics().SelectItemList();
            return PartialView("~/Views/Products/AddProduct.cshtml");
        }

        [HttpGet]
        public ActionResult GetCategoriesListByDepId(int id)
        {
            DropDownModel ddm = new DropDownModel();
            ddm.Id = "CList";
            ddm.Name = "Categories";
            ddm.Caption = "Select Category";
            ddm.Values = new GarmentsHandler().GetCategories(id).SelectItemList();
            return PartialView("~/Views/Shared/_DropDown.cshtml", ddm);
        }

        [HttpGet]
        public ActionResult GetSubCategoriesListByDepId(int id)
        {
            DropDownModel ddm = new DropDownModel();
            ddm.Id = "ScList";
            ddm.Name = "SubCategories";
            ddm.Caption = "Select Sub Category";
            ddm.Values = new GarmentsHandler().GetSubCategoriesList(id).SelectItemList();
            return PartialView("~/Views/Shared/_DropDown.cshtml", ddm);
        }
        [HttpPost]
        public ActionResult AddProduct(FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/ManageProducts" });
            try
            {
                Products product = new Products();
                product.Name = data["Name"];
                product.Price = Convert.ToSingle(data["Price"]);
                product.LaunchDate = Convert.ToDateTime(data["LaunchingDate"]);
                product.subCategory = new SubCategory { Id = Convert.ToInt32(data["SubCategories"]) };
                string[] sizes = data["SizesOffered"].Split(',');
                foreach (var size in sizes)
                {
                    product.SizesOffered.Add(new Size { Id = Convert.ToInt32(size) });
                }
                string[] colors = data["ColorsOffered"].Split(',');
                foreach (var color in colors)
                {
                    product.colors.Add(new Color { Id = Convert.ToInt32(color) });
                }
                product.fabric = new Fabrics { Id = Convert.ToInt32(data["Fabric"]) };
                product.Description = data["Description"];
                long uno = DateTime.Now.Ticks;
                int counter = 0;
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase pic = Request.Files[file];
                    if (pic != null && pic.ContentLength > 0)
                    {
                        string url = $"~/images/Products/{uno}{++counter}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                        string path = Server.MapPath(url);
                        pic.SaveAs(path);
                        product.Images.Add(new ProductImage { Url = url, Priority = counter });
                    }
                }
                new GarmentsHandler().AddProduct(product);
                TempData.Add("alert", new AlertModel("Product added successfully", AlertType.Success));
            }
            catch
            {

                TempData.Add("alert", new AlertModel("Failed to add Product", AlertType.Error));
            }

                return RedirectToAction("ManageProducts");
        }

        [HttpPost]
        public ActionResult DelProduct(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "Products/ManageProducts" });
            try
            {
                new GarmentsHandler().DelProduct(id);
                TempData.Add("alert", new AlertModel("Product deleted successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to delete product", AlertType.Error));
            }
            return Json(Url.Action("ManageProducts"));
        }
    }
}