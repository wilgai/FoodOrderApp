using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using FoodOrderApp.Model;
using System.Linq;
using System.Collections.ObjectModel;

namespace FoodOrderApp.Services
{
    public class FoodItemService
    {
        FirebaseClient client;
        public FoodItemService()
        {
            client = new FirebaseClient(" https://delevery-cb7c7.firebaseio.com/");
        }

        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            var products = (await client.Child("FoodItems")
                 .OnceAsync<FoodItem>())
                 .Select(f => new FoodItem
                 {
                     CategoryID = f.Object.CategoryID,
                     PlaceID=f.Object.PlaceID,
                     Description = f.Object.Description,
                     HomeSelected = f.Object.HomeSelected,
                     ImageUrl = f.Object.ImageUrl,
                     Name = f.Object.Name,
                     Price=f.Object.Price,
                     Group=f.Object.Group,
                     ProductID = f.Object.ProductID,
                     
                 }).ToList();
            return products;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryAsync(int categoryID)
        {
            var foodItemsByCategory = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.CategoryID == categoryID).ToList();
            foreach (var item in items)
            {
                foodItemsByCategory.Add(item);
            }
            return foodItemsByCategory;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByPlaceAsync(int placeID)
        {
            var foodItemsByPlace = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.PlaceID == placeID).ToList();
            foreach (var item in items)
            {
                foodItemsByPlace.Add(item);
            }
            return foodItemsByPlace;
        }
        public async Task<ObservableCollection<FoodItem>> GetLatestFoodItemsAsync()
        {
            var latestFoodItems = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).OrderByDescending(f => f.ProductID).Take(10);
            foreach (var item in items)
            {
                latestFoodItems.Add(item);
            }
            return latestFoodItems;
        }

        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByQueryAsync(string searchText)
        {
            var foodItemsByQuery = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.Name.Contains(searchText)).ToList();
            foreach (var item in items)
            {
                foodItemsByQuery.Add(item);
            }
            return foodItemsByQuery;
        }

       
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByGroupComboAsync(int Id,string group)
        {
            var foodItemsByGroup = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.Group == group && p.PlaceID == Id).ToList();
            foreach (var item in items)
            {
                foodItemsByGroup.Add(item);
            }
            return foodItemsByGroup;
        }
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByGroupKidsAsync(int Id, string group)
        {
            var foodItemsByGroup = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.Group == group && p.PlaceID == Id).ToList();
            foreach (var item in items)
            {
                foodItemsByGroup.Add(item);
            }
            return foodItemsByGroup;
        }
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByGroupDrinksAsync(int Id, string group)
        {
            var foodItemsByGroup = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.Group == group && p.PlaceID == Id).ToList();
            foreach (var item in items)
            {
                foodItemsByGroup.Add(item);
            }
            return foodItemsByGroup;
        }
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByGroupSpecialAsync(int Id, string group)
        {
            var foodItemsByGroup = new ObservableCollection<FoodItem>();
            var items = (await GetFoodItemsAsync()).Where(p => p.Group == group && p.PlaceID == Id    ).ToList();
            foreach (var item in items)
            {
                foodItemsByGroup.Add(item);
            }
            return foodItemsByGroup;
        }

    }
}
