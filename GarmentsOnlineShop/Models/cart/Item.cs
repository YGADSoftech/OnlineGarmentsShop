using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GarmentsOnlineShop.Models.cart
{
    public class Item
    {
        public Item()
        {

        }
        public Item(EntitiesLibrary.Garments.Products product, int quantity,string size, string color)
        {
            this.product = product;
            this.quantity = quantity;
            this.size = size;
            this.color = color;

        }
        private EntitiesLibrary.Garments.Products product = new EntitiesLibrary.Garments.Products();
        private int quantity;
        private string size;
        private string color;
        public EntitiesLibrary.Garments.Products Products
        {
            get { return product; }
            set { product = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public string Size
        {
            get { return size; }
            set { size = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        
    }
}