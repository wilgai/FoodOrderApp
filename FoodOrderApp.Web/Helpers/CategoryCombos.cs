using Common.Web.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FoodOrderApp.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Helpers
{
    public class CategoryCombos : ICategoryCombos
    {
        private readonly Datacontext _context;
        public CategoryCombos(Datacontext context)
        {
            _context = context;
        }
        
        IFirebaseClient client;
        public IEnumerable<SelectListItem> GetComboCategories()
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Categories");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Category>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Category>(((JProperty)item).Value.ToString()));
            }
            List<SelectListItem> category = list.Select(t => new SelectListItem
            {
                Text = t.CategoryName,
                Value = $"{t.CategoryID}"

            })
            .OrderBy(t => t.Text)
            .ToList();

            category.Insert(0, new SelectListItem
            {
                Text = "[Select a category...]",
                Value = "0"
            });

            return category;

        }
    }
}
