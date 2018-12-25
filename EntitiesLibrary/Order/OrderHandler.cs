using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntitiesLibrary.Order;

namespace EntitiesLibrary
{
    public class OrderHandler
    {
        public void AddOrder(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(order.user).State = System.Data.Entity.EntityState.Unchanged;
                foreach(var p in order.items)
                {
                    context.Entry(p.Products).State = System.Data.Entity.EntityState.Unchanged;
                }
                context.orders.Add(order);
                context.SaveChanges();
            }
        }
        public Orders GetOrder(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                Orders ordr =  (from o in context.orders
                        .Include(o => o.items)
                        .Include(o => o.user)
                        where o.Id == order.Id
                        select o).FirstOrDefault();
                return ordr;
            }
        }
        public void UpdatePaymentType(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                Orders ordr = (from o in context.orders
                       .Include(o => o.items)
                       .Include(o => o.user)
                            where o.Id == order.Id
                            select o).FirstOrDefault();
                ordr.paymentType = order.paymentType;
                context.SaveChanges();
            }
        }
        public void RemoveOrder(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                Orders ordr = (from o in context.orders
                        .Include(o => o.items)
                        .Include(o => o.user)
                               where o.Id == order.Id
                               select o).FirstOrDefault();
                context.orders.Remove(ordr);
                context.SaveChanges();
            }
        }
        public List<Orders> GetOrdersOfAUser(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                List<Orders> orders =  (from o in context.orders
                        .Include(o=> o.items)
                        where o.user.Id == id select o).ToList();

                foreach(var o in orders)
                {
                    List<CartItem> items = new List<CartItem>();
                    for (int i = 0; i<o.items.ToArray().Length; i++)
                    {
                        int Id = o.items.ToArray()[i].Id;
                        items.Add((from itm in context.cartItem.Include(a=>a.Products.Images) where itm.Id == Id select itm).FirstOrDefault());
                    }
                    o.items = items;
                }
                return orders;
            }
        }
        public List<Orders> GetOrdersForManagement()
        {
            using (ContextClass context = new ContextClass())
            {
                List<Orders> orders =  (from o in context.orders
                        .Include(o=> o.items) select o).ToList();

                foreach (var o in orders)
                {
                    List<CartItem> items = new List<CartItem>();
                    for (int i = 0; i < o.items.ToArray().Length; i++)
                    {
                        int Id = o.items.ToArray()[i].Id;
                        items.Add((from itm in context.cartItem.Include(a => a.Products.Images) where itm.Id == Id select itm).FirstOrDefault());
                    }
                    o.items = items;
                }
                return orders;
            }  
        }
        public void SendOrder(int id)
        {
            using(ContextClass context = new ContextClass())
            {
                Orders order = context.orders.Find(id);
                order.orderstate = "Sent";
                context.SaveChanges();
            }
        }
        public void CancelOrder(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Orders order = context.orders.Find(id);
                order.orderstate = "Cancelled";
                context.SaveChanges();
            }
        }
    }
}
