using Common.Web.Entities;
using FoodOrderApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public interface ICategoryConverterHelper
    {
        Category ToCategory(CategoryViewModel model, string imageId);

        CategoryViewModel ToCategoryViewModel(Category category);
        
    }
}
