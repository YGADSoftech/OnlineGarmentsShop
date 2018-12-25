using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarmentsOnlineShop.Models.DepartmentAndCategories
{
    public class CatergoryModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string imageUrl { get; set; }
        
        public int dep_Id { get; set; }
    }
}