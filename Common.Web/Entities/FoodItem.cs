using System;
namespace FoodOrderApp.Model
{
    public class FoodItem
    {
        public int ProductID { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string HomeSelected { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Group { get; set; }
        public int PlaceID { get; set; }

    }
}
