using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLibrary.Garments;

namespace GarmentsOnlineShop.Models.Products
{
    public static class ProductsHelper
    {
        public static List<ProductsModel> ToProductSummary(this List<EntitiesLibrary.Garments.Products> products)
        {
            List<ProductsModel> model = new List<ProductsModel>();
            foreach (var p in products)
            {
                ProductsModel pm = new ProductsModel();
                pm.id = p.Id;
                pm.name = p.Name;
                pm.price = p.Price;
                if(p.Images.Count>0)
                {
                    pm.imageUrl = p.Images.ToList()[0].Url;
                    pm.alternateImage = (p.Images.Count > 1) ? p.Images.ToList()[1].Url : "/images/nophoto.png";
                }
                else
                {
                    pm.imageUrl = "/images/nophoto.png";
                }
                model.Add(pm);
            }
            model.TrimExcess();
            return model;
        }
    }
}