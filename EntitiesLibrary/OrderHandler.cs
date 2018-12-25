using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLibrary
{
    public class OrderHandler
    {
        public void AddOrder(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(order.user).State = System.Data.Entity.EntityState.Unchanged;
                
                context.orders.Add(order);
                context.SaveChanges();
            }
        }
        public Orders GetOrder(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from o in context.orders
                        where o.OrderDate == order.OrderDate && o.OrderID == order.OrderID && o.user == order.user
                        select o).FirstOrDefault();
            }
        }
        public void UpdatePaymentType(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                Orders o =  context.orders.Find(order);
                o.paymentType = order.paymentType;
                context.SaveChanges();
            }
        }
        public void RemoveOrder(Orders order)
        {
            using (ContextClass context = new ContextClass())
            {
                context.orders.Remove(order);
                context.SaveChanges();
            }
        }
    }
}
