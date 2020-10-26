﻿using Common.Web.Entities;
using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    public class AddItemPlaceData
    {
        private readonly Random _random;
        private readonly FirebaseClient _client;

        public List<Itemplace> Itemplaces { get; set; }
        public AddItemPlaceData()
        {
            string remarks = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            _random = new Random();
            _client = new FirebaseClient(" https://delevery-cb7c7.firebaseio.com/");

            Itemplaces = new List<Itemplace>()
            {
                new Itemplace(){
                 Id=1,
                 Name="Burger King",
                 Address="Ave. Venezuela",
                 ImageFullPath="MainBurger.png",
                 CategoryID = 1,
                 Qualifications=GetRandomQualifications(remarks)
                },
                 new Itemplace(){
                 Id=2,
                 Name="Mcdonald's",
                 Address="Auto pista San Isidro",
                 ImageFullPath="MainBurger.png",
                 CategoryID = 3,
                 Qualifications=GetRandomQualifications(remarks)
                },
                 new Itemplace(){
                 Id=3,
                 Name="KFC",
                 Address="Ave. Sabana Larga",
                 ImageFullPath="MainBurger.png",
                 CategoryID = 4,
                 Qualifications=GetRandomQualifications(remarks)
                },
                  new Itemplace(){
                 Id=4,
                 Name="Little Caesars",
                 Address="Ave. Lope de vega",
                 ImageFullPath="MainBurger.png",
                 CategoryID = 2,
                 Qualifications=GetRandomQualifications(remarks)
                },
            };
        }


        public async Task AddItemPlacesAsync()
        {
            try
            {
                foreach (var itemplace in Itemplaces)
                {
                    await _client.Child("Itemplaces").PostAsync(new Itemplace()
                    {
                        Id = itemplace.Id,
                        Name = itemplace.Name,
                        Address = itemplace.Address,
                        ImageFullPath = itemplace.ImageFullPath,
                        CategoryID=itemplace.CategoryID,
                        Qualifications = itemplace.Qualifications
                    });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private ICollection<PlaceQualification> GetRandomQualifications(string remarks)
        {
            List<PlaceQualification> qualifications = new List<PlaceQualification>();
            for (int i = 0; i < 10; i++)
            {
                qualifications.Add(new PlaceQualification
                {
                    Id= _random.Next(1, 5),
                    Date = DateTime.UtcNow,
                    Remarks = remarks,
                    Score = _random.Next(1, 5),

                });
            }
            return qualifications;
        }
    }
}
