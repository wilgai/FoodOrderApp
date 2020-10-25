using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    public class AddFoodItemData
    {
        FirebaseClient client;

        public List<FoodItem> FoodItems { get; set; }

        public AddFoodItemData()
        {
            client = new FirebaseClient(" https://delevery-cb7c7.firebaseio.com/");
            FoodItems = new List<FoodItem>()
            {
                new FoodItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    PlaceID=5,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 1",
                    Description = "Burger - Pizza - Breakfast",
                    HomeSelected = "CompleteHeart",
                    Group="Combo",
                    Price = 100
                },
                new FoodItem
                {
                    ProductID = 2,
                    CategoryID = 1,
                    PlaceID= 1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 2",
                    Description = "Burger - Pizza - Breakfast",
                    HomeSelected = "EmptyHeart",
                    Group="Special",
                    Price = 350
                },
                new FoodItem
                {
                    ProductID = 3,
                    CategoryID = 1,
                    PlaceID=1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 3",
                    Description = "Burger - Pizza - Breakfast",
                    HomeSelected = "CompleteHeart",
                    Group="Kids",
                    Price = 450
                },
                new FoodItem
                {
                    ProductID = 4,
                    CategoryID = 1,
                    PlaceID=2,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 4",
                    Description = "Burger - Pizza - Breakfast",
                    
                    HomeSelected = "EmptyHeart",
                    Group="Drinks",
                    Price = 450
                },
                 new FoodItem
                {
                    ProductID = 4,
                    CategoryID = 1,
                    PlaceID=1,
                    ImageUrl = "MainBurger",
                    Name = "Burger and Pizza Hub 4",
                    Description = "Burger - Pizza - Breakfast",
                    HomeSelected = "EmptyHeart",
                    Group="Drinks",
                    Price = 450
                },
                new FoodItem
                {
                    ProductID = 5,
                    CategoryID = 2,
                    PlaceID=3,
                    ImageUrl = "MainPizza",
                    Name = "Pizza",
                    Description = "Pizza - Breakfast",
                    
                    HomeSelected = "CompleteHeart",
                    Group="Combo",
                    Price = 453
                },
                new FoodItem
                {
                    ProductID = 6,
                    CategoryID = 2,
                    PlaceID=5,
                    ImageUrl = "MainPizza",
                    Name = "Pizza Hub 2",
                    Description = "Pizza Hub 2- Breakfast",
                    HomeSelected = "EmptyHeart",
                    Group="Kids",
                    Price = 45
                },
                new FoodItem
                {
                    ProductID = 7,
                    CategoryID = 3,
                    PlaceID=1,
                    ImageUrl = "MainDessert",
                    Name = "Ice Creams",
                    Description = "Icecream - Breakfast",
                    HomeSelected = "CompleteHeart",
                    Group="Drinks",
                    Price = 150
                },
                new FoodItem
                {
                    ProductID = 8,
                    CategoryID = 3,
                    PlaceID=2,
                    ImageUrl = "MainDessert",
                    Name = "Cakes",
                    Description = "Cool Cakes- Breakfast",
                    HomeSelected = "EmptyHeart",
                    Group="Special",
                    Price = 850
                }
             };
        }

        public async Task AddFoodItemAsync()
        {
            try
            {
                foreach (var item in FoodItems)
                {
                    await client.Child("FoodItems").PostAsync(new FoodItem()
                    {
                        PlaceID = item.PlaceID,
                        CategoryID = item.CategoryID,
                        ProductID = item.ProductID,
                        Description = item.Description,
                        HomeSelected = item.HomeSelected,
                        ImageUrl = item.ImageUrl,
                        Name = item.Name,
                        Price = item.Price,
                        Group= item.Group
                        
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
