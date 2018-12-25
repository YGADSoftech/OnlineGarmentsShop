using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarmentsOnlineShop.Models.Products
{
    public class ProductsModel
    {
        public int id { get; set; }
        public string name  { get; set; }
        public float price { get; set; }
        public string imageUrl { get; set; }
        public string alternateImage { get; set; }
    }
}