using Common.Web.Entities;
using FoodOrderApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
   public  interface IItemplaceConverter
    {
        Itemplace ToItemplace(ItemplaceViewModel model, string imageFile);

        ItemplaceViewModel ToItemplaceViewModel(Itemplace category);
    }
}
