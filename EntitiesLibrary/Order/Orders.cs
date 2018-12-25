using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLibrary.Garments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLibrary.Order;

namespace EntitiesLibrary
{
    public class Orders
    {
        public Orders()
        {
            items = new List<CartItem>();
        }
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(200)]
        public string address { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string OrderID { get; set; }
        [Required]
        public virtual User user { get; set; }
        [Required]
        public virtual ICollection<CartItem> items { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        public string orderstate { get; set; }
        public Nullable<DateTime> OrderDate { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        public string paymentType { get; set; }
        [Required]
        public float TotalAmount { get; set; }
    }
}
