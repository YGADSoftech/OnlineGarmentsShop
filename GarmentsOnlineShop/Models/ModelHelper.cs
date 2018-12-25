using EntitiesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarmentsOnlineShop.Models
{
    public static class ModelHelper
    {
        public static List<SelectListItem> SelectItemList(this IEnumerable<Ilistable> values)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var v in values)
            {
                list.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id) });
            }
            return list;
        }
    }
}