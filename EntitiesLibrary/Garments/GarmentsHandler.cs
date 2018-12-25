using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace EntitiesLibrary.Garments
{
    public class GarmentsHandler 
    {
        public Departments GetDep( int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from dep in context.Departments where dep.Id == id select dep).FirstOrDefault();
            }
        }
        public List<Departments> GetDepList()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from dep in context.Departments select dep).ToList();
            }
        }
        public void AddDep(Departments dep)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Departments.Add(dep);
                context.SaveChanges();
            }
        }
        public void UpdateDep(Departments dep, int id)
        {
            using(ContextClass context = new ContextClass())
            {
               Departments dpt = context.Departments.Find(id);
                dpt.Name = dep.Name;
                dpt.ImageUrl = dep.ImageUrl;
                context.SaveChanges();
            }
        }
        public void DelDep(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Departments dep = context.Departments.Find(id);
                context.Departments.Remove(dep);
                context.SaveChanges();
            }
        }
        public List<Category> GetCategories(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return(from c in context.Categories where c.department.Id == id select c).ToList();
            }
        }
        public void AddCategory(Category c)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(c.department).State = EntityState.Unchanged;
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }
        public Category GetCategory(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from c in context.Categories.Include(p => p.department) where c.Id == id select c).FirstOrDefault();
            }

        }
        public void UpdateCategory(Category c, int id)
        {
            using(ContextClass context = new ContextClass())
            {
                Category cat = context.Categories.Find(id);
                cat.Name = c.Name;
                cat.ImageUrl = c.ImageUrl;
                context.SaveChanges();
            }
        }
        public void DelCategory(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Category c = context.Categories.Find(id);
                context.Categories.Remove(c);
                context.SaveChanges();
            }
        }
        public List<SubCategory> GetSubCategoriesList(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from Sb in context.SubCategories where Sb.Category.Id == id select Sb).ToList();
            }
        }
        public Category GetCategoryAndDpt(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from sb in context.Categories.Include(s => s.department) where sb.Id == id select sb).FirstOrDefault();
            }
        }
        public void AddSubCat(SubCategory s)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Entry(s.Category).State = EntityState.Unchanged;
                context.SubCategories.Add(s);
                context.SaveChanges();
            }
        }
        public SubCategory GetSubCat(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from s in context.SubCategories where s.Id == id select s).FirstOrDefault();
            }
        }
        public void updateSubCat(SubCategory sb, int id)
        {
            using (ContextClass context = new ContextClass())
            {
                SubCategory s = context.SubCategories.Find(id);
                s.Name = sb.Name;
                s.ImageUrl = sb.ImageUrl;
                context.SaveChanges();
            }
        }
        public void DelSubCat(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                SubCategory s = context.SubCategories.Find(id);
                context.SubCategories.Remove(s);
                context.SaveChanges();
            }
        }

        public List<Color> getColors()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from c in context.Colors select c).ToList();
            }
        }
        public void AddColor(Color c)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Colors.Add(c);
                context.SaveChanges();
            }
        }
        public List<Fabrics> getFabrics()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from f in context.Fabrics select f).ToList();
            }
        }
        public void AddFabrics(Fabrics f)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Fabrics.Add(f);
                context.SaveChanges();
            }
        }

        public List<Size> getSizes()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from s in context.Sizes select s).ToList();
            }
        }
        public void AddSize(Size s)
        {
            using (ContextClass context = new ContextClass())
            {
                context.Sizes.Add(s);
                context.SaveChanges();
            }
        }

        public List<Products> GetProducts()
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                        .Include(p => p.subCategory.Category.department)
                        .Include(p => p.fabric)
                        .Include(p => p.colors)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.Images)
                        select p).ToList();
            }
        }

        public void AddProduct(Products product)
        {
            using(ContextClass context = new ContextClass())
            {
                context.Entry(product.subCategory).State = EntityState.Unchanged;
                context.Entry(product.fabric).State = EntityState.Unchanged;
                foreach(var color in product.colors)
                {
                    context.Entry(color).State = EntityState.Unchanged;
                }
                foreach(var size in product.SizesOffered)
                {
                    context.Entry(size).State = EntityState.Unchanged;
                }
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void DelProduct(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                Products product = (from p in context.Products
                              .Include(p => p.colors)
                              .Include(p => p.fabric)
                              .Include(p => p.Images)
                              .Include(p => p.SizesOffered)
                              .Include(p => p.subCategory.Category.department)
                                    where p.Id == id
                                    select p).FirstOrDefault();
                
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
        public Products GetProductById(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                        .Include(p => p.Images)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.subCategory.Category.department)
                        .Include(p => p.colors)
                        .Include(p => p.fabric)
                        where p.Id == id
                        select p).FirstOrDefault();
            }
        }
        public List<Products> GetProductByDepartment(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                        .Include(p => p.colors)
                        .Include(p => p.fabric)
                        .Include(p => p.Images)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.subCategory.Category.department)
                        where p.subCategory.Category.department.Id == id
                        select p).ToList();
            }
        }
        public List<Products> GetProductByCategary(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                        .Include(p => p.colors)
                        .Include(p => p.fabric)
                        .Include(p => p.Images)
                        .Include(p => p.SizesOffered)
                        .Include(p => p.subCategory.Category.department)
                        where p.subCategory.Category.Id == id
                        select p).ToList();
            }
        }
        public List<Products> GetProductsBySubCategary(int id)
        {
            using (ContextClass context = new ContextClass())
            {
                return (from p in context.Products
                       .Include(p => p.colors)
                       .Include(p => p.fabric)
                       .Include(p => p.Images)
                       .Include(p => p.SizesOffered)
                       .Include(p => p.subCategory.Category.department)
                        where p.subCategory.Id == id
                        select p).ToList();
            }
        }
    }
}
