using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarmentsOnlineShop.Models.DepartmentAndCategories
{
    public class SubCatModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }

        public int cat_Id { get; set; }
    }
}