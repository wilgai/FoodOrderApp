using Common.Web.Entities;
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
                     Id=f.Object.Id,
                     Name = f.Object.Name,
                     Address=f.Object.Address,
                     ImageFullPath=f.Object.ImageFullPath,
                     CategoryID=f.Object.CategoryID
                     //Qualifications=f.Object.Qualifications
                    

                 }).ToList();
            return places;
        }

        public async Task<ObservableCollection<Itemplace>> GetLatestPlaceItemsAsync()
        {
            var latestPlaceItems = new ObservableCollection<Itemplace>();
            var items = (await GetPlaceItemsAsync()).OrderByDescending(f => f.Id).Take(10);
            foreach (var item in items)
            {
                latestPlaceItems.Add(item);
            }
            return latestPlaceItems;
        }


        public async Task<ObservableCollection<Itemplace>> GetFoodItemsByCategoryAsync(string categoryID)
        {
            var foodItemsByPlace = new ObservableCollection<Itemplace>();
            var items = (await GetPlaceItemsAsync()).Where(p => p.CategoryID.ToString() == categoryID).ToList();
            foreach (var item in items)
            {
                foodItemsByPlace.Add(item);
            }
            return foodItemsByPlace;
        }

        public async Task<ObservableCollection<Itemplace>> GetPlaceItemsByQueryAsync(string searchText)
        {
            var placeItemsByQuery = new ObservableCollection<Itemplace>();
            var items = (await GetPlaceItemsAsync()).Where(p => p.Name.ToLower().Contains(searchText.ToLower())).ToList();
            foreach (var item in items)
            {
                placeItemsByQuery.Add(item);
            }
            return placeItemsByQuery;
        }
    }
}
