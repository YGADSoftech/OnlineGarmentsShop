using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EntitiesLibrary.Garments
{
    public class Products : Ilistable
    {
        public Products()
        {
            Images = new List<ProductImage>();
            colors = new List<Color>();
            SizesOffered = new List<Size>();
        }
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="varchar")]
        [MaxLength(100)]
        public string Name { get; set; }
        public float Price { get; set; }
        [Column(TypeName ="varchar")]
        [MaxLength(100)]
        public string Description { get; set; }

        public Nullable<DateTime> LaunchDate { get; set; }
        [Required]
        public virtual SubCategory subCategory { get; set; }
        [Required]
        public virtual ICollection<ProductImage> Images { get; set; }
        public virtual ICollection<Color> colors { get; set; }
        public virtual ICollection<Size> SizesOffered { get; set; }
        public virtual Fabrics fabric { get; set; }

    }
}
