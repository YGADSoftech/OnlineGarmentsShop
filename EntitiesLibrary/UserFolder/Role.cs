using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntitiesLibrary.UserFolder
{
    public class Role:Ilistable
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
