using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitiesLibrary.Garments;
using GarmentsOnlineShop.Models;
using GarmentsOnlineShop.Models.DepartmentAndCategories;
using EntitiesLibrary;

namespace GarmentsOnlineShop.Controllers
{
    public class DepartmentsAndCategoriesController : Controller
    {
        // GET: DepartmentsAndCategories
        [HttpGet]
        public ActionResult ManageDepartments()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            //DepartmentsAndCategoriesHelper.ToDepModelList(new GarmentsHandler().GetDepList());
            List<DepartmentsModel> depList = new GarmentsHandler().GetDepList().ToDepModelList();
            return View(depList);
        }
        [HttpGet]
        public ActionResult AddDepartment()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            return PartialView("~/Views/DepartmentsAndCategories/AddDepartment.cshtml");
        }
        [HttpPost]
        public ActionResult AddDepartment(DepartmentsModel depModel, FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                Departments dep = depModel.ToDepEntity();
                HttpPostedFileBase pic = Request.Files["pic"];
                if (pic != null && pic.ContentLength > 0)
                {
                    long uno = DateTime.Now.Ticks;
                    string url = $"~/images/Departments/{uno}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    pic.SaveAs(path);
                    dep.ImageUrl = url;
                }
                new GarmentsHandler().AddDep(dep);
                TempData.Add("alert", new AlertModel("Department added successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to add department", AlertType.Error));
            }

            return RedirectToAction("ManageDepartments");
        }
        [HttpGet]
        public ActionResult UpdateDep(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            DepartmentsModel depModel = new GarmentsHandler().GetDep(id).ToDepModel();


            return PartialView("~/Views/DepartmentsAndCategories/UpdateDep.cshtml", depModel);

        }
        [HttpPost]
        public ActionResult UpdateDep(DepartmentsModel depModel, FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                Departments dep = depModel.ToDepEntity();
                HttpPostedFileBase pic = Request.Files["pic"];
                if (pic != null && pic.ContentLength > 0)
                {
                    long uno = DateTime.Now.Ticks;
                    string url = $"~/images/Departments/{uno}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    pic.SaveAs(path);
                    dep.ImageUrl = url;
                }
                new GarmentsHandler().UpdateDep(dep, dep.Id);
                TempData.Add("alert", new AlertModel("Department updated successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to update department", AlertType.Error));
            }
            return RedirectToAction("ManageDepartments");
        }
        [HttpPost]
        public ActionResult DelDep(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                new GarmentsHandler().DelDep(id);
                TempData.Add("alert", new AlertModel("Department deleted successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alert", new AlertModel("Failed to delete department", AlertType.Error));
            }
            return Json(Url.Action("ManageDepartments"));
        }
        [HttpGet]
        public ActionResult ManageCategories(int Id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            List<CatergoryModel> model = new GarmentsHandler().GetCategories(Id).ToListOfCategoryModels();
            Departments dep = new GarmentsHandler().GetDep(Id);
            ViewBag.depName = dep.Name;
            TempData["depID"] = Id;
            return View(model);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            ViewBag.depList = new GarmentsHandler().GetDepList().SelectItemList();
            return PartialView("~/Views/DepartmentsAndCategories/AddCategory.cshtml");
        }
        [HttpPost]
        public ActionResult AddCategory(CatergoryModel model, FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                Category c = new Category();
                c.Name = model.name;
                c.department = new Departments { Id = Convert.ToInt32(data["departments"]) };
                HttpPostedFileBase pic = Request.Files["pic"];
                if (pic != null && pic.ContentLength > 0)
                {
                    long uno = DateTime.Now.Ticks;
                    string url = $"~/images/Categories/{uno}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    pic.SaveAs(path);
                    c.ImageUrl = url;
                }
                new GarmentsHandler().AddCategory(c);
                TempData.Add("alertCategory", new AlertModel("Category added successfully", AlertType.Success));
                return RedirectToAction($"ManageCategories/{data["departments"]}");
            }
            catch
            {
                TempData.Add("alertCategory", new AlertModel("Failed to add category", AlertType.Error));
                return RedirectToAction($"ManageCategories/{TempData["depID"]}");
            }
            
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            CatergoryModel model = new GarmentsHandler().GetCategory(id).CategoryEntityToModel();
            return PartialView("~/Views/DepartmentsAndCategories/UpdateCategory.cshtml", model);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CatergoryModel model, FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                Category entity = model.CategoryModelToEntity();
                HttpPostedFileBase pic = Request.Files["pic"];
                if (pic != null && pic.ContentLength > 0)
                {
                    long uno = DateTime.Now.Ticks;
                    string url = $"~/images/Categories/{uno}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    pic.SaveAs(path);
                    entity.ImageUrl = url;
                }
                new GarmentsHandler().UpdateCategory(entity, entity.Id);
                Category ctForID = new GarmentsHandler().GetCategory(entity.Id);
                TempData["ctForID"] = ctForID.department.Id;
                TempData.Add("alertCategory", new AlertModel("Category updated successfully", AlertType.Success));
            }
            catch
            {
                TempData.Add("alertCategory", new AlertModel("Failed to update category", AlertType.Error));
            }
            return RedirectToAction($"ManageCategories/{TempData["ctForID"]}");
        }
        [HttpPost]
        public ActionResult DelCategory(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                Category ForDepId = new GarmentsHandler().GetCategory(id);
                TempData["ForDepId"] = ForDepId.department.Id;
                new GarmentsHandler().DelCategory(id);

                TempData.Add("alertCategory", new AlertModel("Category deleted successfully", AlertType.Success));
            }
            catch
            {

                TempData.Add("alertCategory", new AlertModel("Failed to delete category", AlertType.Error));
            }
            return Json(Url.Action($"ManageCategories/{TempData["ForDepId"]}"));
        }

        [HttpGet]
        public ActionResult ManageSubCategories(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            List<SubCatModel> models = new GarmentsHandler().GetSubCategoriesList(id).ToList_SubCatModel();
            Category c = new GarmentsHandler().GetCategoryAndDpt(id);
            ViewBag.DName = c.department.Name;
            ViewBag.CName = c.Name;
            ViewBag.Cid = c.department.Id;
            TempData["DepIdForCategoryList"] = c.department.Id;
            TempData["id"] = id;
            return View(models);
         }
        [HttpGet]
        public ActionResult AddSubCategory()
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            ViewBag.CategoryList = new GarmentsHandler().GetCategories(Convert.ToInt32(TempData["DepIdForCategoryList"])).SelectItemList();

            return PartialView("~/Views/DepartmentsAndCategories/AddSubCategory.cshtml");
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCatModel model, FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                SubCategory entity = model.ToSubCatEntity();
                entity.Category = new Category { Id = Convert.ToInt32(data["categories"]) };
                HttpPostedFileBase pic = Request.Files["pic"];
                if (pic != null && pic.ContentLength > 0)
                {
                    long uno = DateTime.Now.Ticks;
                    string url = $"~/images/SubCategories/{uno}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    pic.SaveAs(path);
                    entity.ImageUrl = url;
                }
                new GarmentsHandler().AddSubCat(entity);

                TempData.Add("alertSubCategory", new AlertModel("Failed to Add Sub category", AlertType.Success));
                return RedirectToAction($"ManageSubCategories/{data["categories"]}");
            }
            catch
            {

                TempData.Add("alertSubCategory", new AlertModel("Failed to add sub category", AlertType.Error));
                return RedirectToAction($"ManageSubCategories/{TempData["id"]}");
            }
            
        }

        [HttpGet]
        public ActionResult EditSubCat(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            SubCatModel model = new GarmentsHandler().GetSubCat(id).ToSubCatModel();
            return PartialView("~/Views/DepartmentsAndCategories/EditSubCat.cshtml", model);
        }
        [HttpPost]
        public ActionResult EditSubCat(SubCatModel model, FormCollection data)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                SubCategory entity = model.ToSubCatEntity();
                HttpPostedFileBase pic = Request.Files["pic"];
                if (pic != null && pic.ContentLength > 0)
                {
                    long uno = DateTime.Now.Ticks;
                    string url = $"~/images/Categories/{uno}{pic.FileName.Substring(pic.FileName.LastIndexOf('.'))}";
                    string path = Server.MapPath(url);
                    pic.SaveAs(path);
                    entity.ImageUrl = url;
                }
                new GarmentsHandler().updateSubCat(entity, entity.Id);

                TempData.Add("alertSubCategory", new AlertModel("Sub category updated successfully", AlertType.Success));
            }
            catch
            {

                TempData.Add("alertSubCategory", new AlertModel("Failed to update Sub category", AlertType.Error));
            }
            return RedirectToAction($"ManageSubCategories/{TempData["id"]}");
        }
        [HttpPost]
        public ActionResult DelSubCategory(int id)
        {
            User currentUser = Session[WebUtil.CURRENT_USER] as User;
            if (!(currentUser != null && (currentUser.IsInRole(WebUtil.ADMIN_ROLE) || currentUser.IsInRole(WebUtil.Super_ADMIN_ROLE))))
                return RedirectToAction("Login_Register", "user", new { returnUrl = "DepartmentsAndCategories/ManageDepartments" });
            try
            {
                new GarmentsHandler().DelSubCat(id);

                TempData.Add("alertSubCategory", new AlertModel("Category deleted successfully", AlertType.Success));
            }
            catch
            {

                TempData.Add("alertSubCategory", new AlertModel("Failed to delete sub category", AlertType.Error));
            }
            return Json(Url.Action($"ManageSubCategories/{TempData["id"]}"));
        }
    }
}
