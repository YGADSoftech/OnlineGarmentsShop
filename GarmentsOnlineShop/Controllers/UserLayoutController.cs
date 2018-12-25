 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GarmentsOnlineShop.Models.DepartmentAndCategories;
using GarmentsOnlineShop.Models.Products;
using EntitiesLibrary.Garments;
using GarmentsOnlineShop.Models;
using GarmentsOnlineShop.Models.PayPal;
using EntitiesLibrary.AddressFolder;
using EntitiesLibrary;
using System.Configuration;
using EntitiesLibrary.Order;

namespace GarmentsOnlineShop.Controllers
{
    public class UserLayoutController : Controller
    {
        [HttpGet]
        public ActionResult GetDepartments()
        {
            List<DepartmentsModel> d = new GarmentsHandler().GetDepList().ToDepModelList();
            return PartialView("~/Views/D_C_S/DepList.cshtml",d);
        }
        [HttpGet]
        public ActionResult GetCategories(int id)
        {
            List<CatergoryModel> c = new GarmentsHandler().GetCategories(id).ToListOfCategoryModels();
            return PartialView("~/Views/D_C_S/CatList.cshtml",c);
        }

        [HttpGet]
        public ActionResult GetSubCategories(int id)
        {
            List<SubCatModel> s = new GarmentsHandler().GetSubCategoriesList(id).ToList_SubCatModel();
            return PartialView("~/Views/D_C_S/SubCatList.cshtml",s);
        }
        [HttpGet]
        public ActionResult UserViewPage()
        {
            List<ProductsModel> products = new GarmentsHandler().GetProducts().ToProductSummary();
            return View(products);
        }
        [HttpGet]
        public ActionResult getProductById(int id)
        {
            Products p = new GarmentsHandler().GetProductById(id);
            return PartialView("~/Views/UserLayout/getProductById.cshtml", p);
        }

        [HttpGet]
        public ActionResult getProductByDepartment(int id)
        {
            List<ProductsModel> products = new GarmentsHandler().GetProductByDepartment(id).ToProductSummary();
            return View(products);
        }
        [HttpGet]
        public ActionResult GetProductByCategary(int id)
        {
            List<ProductsModel> products = new GarmentsHandler().GetProductByCategary(id).ToProductSummary();
            return View(products);
        }
        [HttpGet]
        public ActionResult GetProductsBySubCategary(int id)
        {
            List<ProductsModel> products = new GarmentsHandler().GetProductsBySubCategary(id).ToProductSummary();
            return View(products);
        }
        [HttpGet]
        public ActionResult shoppingCartPartialView(int id)
        {
            Products p = new GarmentsHandler().GetProductById(id);
            string s = p.SizesOffered.ToList()[0].Name;
            string c = p.colors.ToList()[0].Name;
            if(Session["cart"] == null)
            {
                List<Models.cart.Item> cart = new List<Models.cart.Item>();
                cart.Add(new Models.cart.Item(p, 1,s,c));
                Session["cart"] = cart;
            }
            else
            {
                List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
                int index = IsExisting(id);
                if(index == -1)
                {
                    cart.Add(new Models.cart.Item(p, 1,s,c));
                }
                else
                {
                    cart[index].Quantity++;
                }
                Session["cart"] = cart;
            }
            return PartialView("~/Views/UserLayout/shoppingCartPartialView.cshtml");
        }
        [HttpGet]
        public ActionResult MyCart()
        {
            return PartialView("~/Views/UserLayout/shoppingCartPartialView.cshtml");
        }
        [HttpGet]
        public ActionResult DeleteItemFromCart(int id)
        {
            int index = IsExisting(id);
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return PartialView("~/Views/UserLayout/shoppingCartPartialView.cshtml");
        }
        [HttpGet]
        public ActionResult UpdateQuantity(updateCart uc)
        {
            int quantity = Convert.ToInt32(uc.quantity);
            int index = Convert.ToInt32(uc.index);
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            cart[index].Quantity = quantity;
            Session["cart"] = cart;
            return PartialView("~/Views/UserLayout/shoppingCartPartialView.cshtml");
        }
        [HttpGet]
        public ActionResult UpdateSize(updateCartSize uc)
        {
            int index = Convert.ToInt32(uc.index);
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            cart[index].Size = uc.size;
            Session["cart"] = cart;
            return PartialView("~/Views/UserLayout/shoppingCartPartialView.cshtml");
        }
        [HttpGet]
        public ActionResult UpdateColor(updateCartColor uc)
        {
            int index = Convert.ToInt32(uc.index);
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            cart[index].Color = uc.Color;
            Session["cart"] = cart;
            return PartialView("~/Views/UserLayout/shoppingCartPartialView.cshtml");
        }
        public int IsExisting(int id)
        {
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            int x = -1;
            for(int i = 0; i<cart.Count; i++)
            {
                if(cart[i].Products.Id== id)
                {
                    x = i;
                }
            }
            return x;
        }
        
        [HttpGet]
        public ActionResult VerifyDetail()
        {
            ViewBag.Countries = new LocationHandler().GetCountryList().SelectItemList();
            return View();
        }
        public ActionResult PostToPayPal()
        {
            paypalModel paypal = new paypalModel();
            paypal.cmd = "_xclick";
            paypal.business = ConfigurationManager.AppSettings["BusinessAccountKey"];

            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandbox"]);
            if(useSandbox)
            {
                ViewBag.actionURl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            }
            else
            {
                ViewBag.actionURl = "https://www.paypal.com/cgi-bin/webscr";
            }
            paypal.cancel_return = System.Configuration.ConfigurationManager.AppSettings["CancelURL"];
            paypal.@return = ConfigurationManager.AppSettings["ReturnURL"];
            paypal.notify_url = ConfigurationManager.AppSettings["NotifyURL"];

            paypal.currency_code = ConfigurationManager.AppSettings["CurrencyCode"];
            paypal.item_name = "user name's order";
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            paypal.amount = cart.Sum(x => x.Quantity * x.Products.Price).ToString();
            return View(paypal);
        }

        [HttpPost]
        public ActionResult VerifyDetail(FormCollection data)
        {
            User user = Session[WebUtil.CURRENT_USER] as User;
            string orderAddress = data["streetAddress"];
            List<Models.cart.Item> cart = (List<Models.cart.Item>)Session["cart"];
            string paymentMethod = data["PaymentMethod"];
            
            Orders order = new Orders();
            if (!String.IsNullOrEmpty(orderAddress))
            {
                order.address = orderAddress;
            }
            else
            {
                order.address = $"{user.Address.StreetAddress},{user.Address.City.Name},{user.Address.City.Province.Name},{user.Address.City.Province.Country.Name}";
            }
            order.OrderDate = DateTime.Now;
            order.OrderID = (DateTime.Now.Ticks + new Random().Next(100000)).ToString();
            order.user = new User { Id = user.Id };
            foreach(var c in cart)
            {
                order.items.Add(new CartItem
                {
                    Products = new Products { Id = c.Products.Id },
                    Quantity = c.Quantity,
                    color_name = c.Color,
                    size_Name = c.Size,
                });
            }
            order.orderstate = "pending";
            order.TotalAmount = cart.Sum(x => x.Quantity * x.Products.Price);
            if (paymentMethod == "Paypal")
            {
                new OrderHandler().AddOrder(order);
                Orders o = new OrderHandler().GetOrder(order);
                TempData["order"] = o;
                return RedirectToAction("PostToPayPal");
            }
            else
            {
                order.paymentType = "On Delivery";
                new OrderHandler().AddOrder(order);
            }
            Session.Remove("cart");
            return RedirectToAction("UserViewPage");
        }
        [HttpGet]
        public ActionResult RedirectFromPayPal()
        {
            Orders order = TempData["order"] as Orders;
            order.paymentType = "Paid via Paypal";
            new OrderHandler().UpdatePaymentType(order);
            Session.Remove("cart");
            return RedirectToAction("UserViewPage");
        }
        [HttpGet]
        public ActionResult CancelFromPaypal()
        {
            Orders order = TempData["order"] as Orders;
            new OrderHandler().RemoveOrder(order);
            Session.Remove("cart");
            return RedirectToAction("UserViewPage");
        }
        [HttpGet]
        public ActionResult Departments()
        {
            //ViewBag.dep = new GarmentsHandler().GetDepList();
            List<Departments> depList = new GarmentsHandler().GetDepList();
            return PartialView(depList);
        }
    }
    public class updateCart
    {
        public string quantity { get; set; }
        public string index { get; set; }
    }
    public class updateCartSize
    {
        public string size { get; set; }
        public string index { get; set; }
    }
    public class updateCartColor
    {
        public string Color { get; set; }
        public string index { get; set; }
    }
}