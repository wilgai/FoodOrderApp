using Common.Web.Entities;
using FoodOrderApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public class CategoryConverterHelper : ICategoryConverterHelper
    {
        public Category ToCategory(CategoryViewModel model, string imageUrl)
        {
            Guid Id = Guid.NewGuid();
            return new Category
            {
                CategoryID = Id.ToString(),
                ImageUrl = imageUrl,
                CategoryName = model.CategoryName,
                CategoryPoster=model.CategoryPoster
            };
        }

        public CategoryViewModel ToCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                CategoryID = category.CategoryID,
                ImageUrl = category.ImageUrl,
                CategoryName = category.CategoryName,
                CategoryPoster=category.CategoryPoster
            };
        }
    }
}
