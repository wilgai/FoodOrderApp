using Firebase.Database;
using FoodOrderApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{


    public class PlaceItemService
    {
        FirebaseClient client;
        public PlaceItemService()
        {
            client = new FirebaseClient("https://delevery-cb7c7.firebaseio.com/");
        }

        public async Task<List<Itemplace>> GetPlaceItemsAsync()
        {
            var places = (await client.Child("Itemplaces")
                 .OnceAsync<Itemplace>())
                 .Select(f => new Itemplace
                 {
                     
                     Name = f.Object.Name,
                     Address=f.Object.Address,
                     ImageFullPath=f.Object.ImageFullPath,
                     //Qualifications=f.Object.Qualifications
                    

                 }).ToList();
            return places;
        }

        public async Task<ObservableCollection<Itemplace>> GetLatestPlaceItemsAsync()
        {
            var latestPlaceItems = new ObservableCollection<Itemplace>();
            var items = (await GetPlaceItemsAsync()).OrderByDescending(f => f.Id).Take(5);
            foreach (var item in items)
            {
                latestPlaceItems.Add(item);
            }
            return latestPlaceItems;
        }
    }
}
