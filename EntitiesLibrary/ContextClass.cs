using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntitiesLibrary.Garments;
using EntitiesLibrary.AddressFolder;
using EntitiesLibrary.UserFolder;
using EntitiesLibrary.Order;

namespace EntitiesLibrary
{
    public class ContextClass : DbContext
    {
        public ContextClass() : base("ConString")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Products>()
                .HasMany<Color>(p => p.colors)
                .WithMany();

            modelBuilder.Entity<Products>()
                .HasMany<Size>(p => p.SizesOffered)
                .WithMany();

            modelBuilder.Entity<User>()
                .HasOptional<Address>(u => u.Address)
                .WithRequired();

        }

        public DbSet<Country> counrties { get; set; }
        public DbSet<Province> provinces { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Departments> Departments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<Fabrics> Fabrics { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<CartItem> cartItem {get; set;}
    }
}
