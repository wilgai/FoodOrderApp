using Common.Web.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FoodOrderApp.Web.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Services
{
    public class CategoryDataService
    {
        private readonly Datacontext _context;
        public CategoryDataService(Datacontext context)
        {
            _context = context;
        }
        
        IFirebaseClient client;
        public async Task<List<Category>> GetItemPlacesAsync()
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Categories");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Category>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Category>(((JProperty)item).Value.ToString()));
            }
            return list;
        }


        public async Task<ObservableCollection<Category>> CheckName(string name)
        {
            var nameFound = new ObservableCollection<Category>();
            var items = (await GetItemPlacesAsync()).Where(C => C.CategoryName == name).ToList();
            foreach (var item in items)
            {
                nameFound.Add(item);
            }
            return nameFound;
        }
    }
}
