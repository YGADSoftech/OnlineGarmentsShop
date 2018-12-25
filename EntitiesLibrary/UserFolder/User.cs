using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EntitiesLibrary.AddressFolder;
using EntitiesLibrary.UserFolder;

namespace EntitiesLibrary
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string ContactNumber { get; set; }

        public virtual Address Address { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string ImageUrl { get; set; }

        public Nullable<DateTime> BirthDate { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string SecurityQuestion { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string SecurityAnswer { get; set; }
        
        [Required]
        public virtual Role Role { get; set; }

        public bool IsInRole(int id)
        {
            return Role != null && Role.Id == id;
        }
    }
}
