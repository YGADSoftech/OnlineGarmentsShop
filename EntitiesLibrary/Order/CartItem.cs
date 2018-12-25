using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLibrary.Order
{
    public class CartItem
    {
        public int Id { get; set; }
        [Required]
        public virtual EntitiesLibrary.Garments.Products Products { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(200)]
        public string size_Name { get; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(200)]
        public string color_name { get; set; }
    }
}
