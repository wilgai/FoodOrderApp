using Common.Web.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FoodOrderApp.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public class ItemplaceConverter : IItemplaceConverter
    {
        private readonly ICategoryCombos _categoryCombos;
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "WGrhhQphcYKSODUzqJaK8Dor4HeMclTDMDBqDVyR",
            BasePath = "https://delevery-cb7c7.firebaseio.com/"
        };
        IFirebaseClient client;

        public IEnumerable<SelectListItem> Categories { get; private set; }

        public ItemplaceConverter(ICategoryCombos categoryCombos)
        {
            _categoryCombos = categoryCombos;
        }

   

        public Itemplace ToItemplace(ItemplaceViewModel model, string imageFile)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Categories/" + model.CategoryId);
            Category category = JsonConvert.DeserializeObject<Category>(response.Body);
            return new Itemplace
            {
                Category = category,
                Id = model.Id,
                Name=model.Name,
                Address= model.Address,
                ImageFullPath= imageFile
            };

        
    }

        

        public ItemplaceViewModel ToItemplaceViewModel(Itemplace itemplace)
        {
           

                return new ItemplaceViewModel
            { 
                Categories = _categoryCombos.GetComboCategories(),
                Id = itemplace.Id,
                Category =itemplace.Category ,
                CategoryId = itemplace.Category.CategoryID,
                Name = itemplace.Name,
                Address = itemplace.Address,
                ImageFullPath = itemplace.ImageFullPath
            };
        }
    }
}
