using Common.Web.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FoodOrderApp.Web.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Web.Services
{

    public class ItemPlaceDataService
    {

        private readonly Datacontext _context;
        public ItemPlaceDataService(Datacontext context)
        {
            _context = context;
        }
        
        IFirebaseClient client;


        public async Task<List<Itemplace>> GetItemPlacesAsync()
        {
            client = new FireSharp.FirebaseClient(_context.GetFirebaseConnection());
            FirebaseResponse response = client.Get("Itemplaces");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Itemplace>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Itemplace>(((JProperty)item).Value.ToString()));
            }
            return list;
        }


        public  async  Task<ObservableCollection<Itemplace>> CheckName(string name)
        {
            var nameFound = new ObservableCollection<Itemplace>();
            var items = (await GetItemPlacesAsync()).Where(I => I.Name == name).ToList();
            foreach (var item in items)
            {
                nameFound.Add(item);
            }
            return nameFound;
        }
    }
}
