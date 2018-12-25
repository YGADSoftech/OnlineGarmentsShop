using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntitiesLibrary.Garments;

namespace GarmentsOnlineShop.Models.DepartmentAndCategories
{
    public static class DepartmentsAndCategoriesHelper
    {
        
        public static DepartmentsModel ToDepModel(this Departments entity)
        {
            DepartmentsModel model = new DepartmentsModel();
            model.id = entity.Id;
            model.name = entity.Name;
            model.imageUrl = entity.ImageUrl;
            return model;
        }
        public static Departments ToDepEntity(this DepartmentsModel model)
        {
            Departments entity = new Departments();
            entity.Id = model.id;
            entity.Name = model.name;
            entity.ImageUrl = model.imageUrl;
            return entity;
        }
        public static List<DepartmentsModel> ToDepModelList(this List<Departments> entities)
        {
            List<DepartmentsModel> models = new List<DepartmentsModel>();
            foreach(var e in entities)
            {
                models.Add(ToDepModel(e));
            }
            models.TrimExcess();
            return models;
        }
        public static CatergoryModel CategoryEntityToModel(this Category entity)
        {
            CatergoryModel model = new CatergoryModel();
            model.id = entity.Id;
            model.name = entity.Name;
            model.imageUrl = entity.ImageUrl;
            return model;
        }
        public static Category CategoryModelToEntity(this CatergoryModel model)
        {
            Category entity = new Category();
            entity.Id = model.id;
            entity.Name = model.name;
            entity.ImageUrl = model.imageUrl;
            return entity;
        }
        public static List<CatergoryModel> ToListOfCategoryModels(this List<Category> entities)
        {
            List<CatergoryModel> models = new List<CatergoryModel>();
            foreach(var e in entities)
            {
                models.Add(CategoryEntityToModel(e));
            }
            models.TrimExcess();
            return models;
        }

        public static SubCatModel ToSubCatModel(this SubCategory entity)
        {
            SubCatModel model = new SubCatModel();
            model.id = entity.Id;
            model.name = entity.Name;
            model.imageUrl = entity.ImageUrl;
            return model;
        }
        public static SubCategory ToSubCatEntity(this SubCatModel model)
        {
            SubCategory entity = new SubCategory();
            entity.Id = model.id;
            entity.Name = model.name;
            entity.ImageUrl = model.imageUrl;
            return entity;
        }
         public static List<SubCatModel> ToList_SubCatModel(this List<SubCategory> entities)
        {
            List<SubCatModel> models = new List<SubCatModel>();
            foreach(var e in entities)
            {
                models.Add(ToSubCatModel(e));
            }
            models.TrimExcess();
            return models;
        }
    }
}