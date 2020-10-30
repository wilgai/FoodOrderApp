using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public interface ICategoryCombos
    {
        IEnumerable<SelectListItem> GetComboCategories();
    }
}
