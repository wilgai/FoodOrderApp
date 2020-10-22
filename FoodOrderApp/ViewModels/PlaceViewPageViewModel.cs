using FoodOrderApp.Model;
using FoodOrderApp.Services;
using System.Collections.ObjectModel;

namespace FoodOrderApp.ViewModels
{
    public class PlaceViewPageViewModel : BaseViewModel
    {
        private Itemplace _SelectedItemplace;
        public Itemplace SelectedItemplace
        {
            set
            {
                _SelectedItemplace = value;
                OnPropertyChanged();
            }

            get
            {
                return _SelectedItemplace;
            }
        }

        public ObservableCollection<FoodItem> FoodItemsByPlace { get; set; }
        public ObservableCollection<FoodItem> FoodItemByGroupKids { get; set; }
        public ObservableCollection<FoodItem> FoodItemByGroupDrinks { get; set; }
        public ObservableCollection<FoodItem> FoodItemByGroupCombo { get; set; }
        public ObservableCollection<FoodItem> FoodItemByGroupSpecial { get; set; }


        private int _TotalFoodItems;
        public int TotalFoodItems
        {
            set
            {
                _TotalFoodItems = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalFoodItems;
            }
        }

        public PlaceViewPageViewModel(Itemplace itemplace)
        {
            SelectedItemplace = itemplace;
            FoodItemsByPlace = new ObservableCollection<FoodItem>();
            GetFoodItems(itemplace.Id);
            FoodItemByGroupCombo = new ObservableCollection<FoodItem>();
            FoodItemByGroupDrinks = new ObservableCollection<FoodItem>();
            FoodItemByGroupKids = new ObservableCollection<FoodItem>();
            FoodItemByGroupSpecial = new ObservableCollection<FoodItem>();

            GetFoodItemByGroupCombo(itemplace.Id,"Combo");
            GetFoodItemByGroupDrinks(itemplace.Id, "Drinks");
            GetFoodItemByGroupKids(itemplace.Id, "Kids");
            GetFoodItemByGroupSpecial(itemplace.Id, "Special");
        }

        private async void GetFoodItems(int Id)
        {
            var data = await new FoodItemService().GetFoodItemsByPlaceAsync(Id);
            FoodItemsByPlace.Clear();
            foreach (var item in data)
            {
                FoodItemsByPlace.Add(item);
            }
            TotalFoodItems = FoodItemsByPlace.Count;
        }


        private async void GetFoodItemByGroupCombo(int Id,string group)
        {
            var data = await new FoodItemService().GetFoodItemsByGroupComboAsync(Id,group);
            FoodItemByGroupDrinks.Clear();
            foreach (var item in data)
            {
                FoodItemByGroupDrinks.Add(item);
            }

        }
        private async void GetFoodItemByGroupDrinks(int Id,string group)
        {
            var data = await new FoodItemService().GetFoodItemsByGroupDrinksAsync(Id, group);
            FoodItemByGroupKids.Clear();
            foreach (var item in data)
            {
                FoodItemByGroupKids.Add(item);
            }

        }
        private async void GetFoodItemByGroupKids(int Id,string group)
        {
            var data = await new FoodItemService().GetFoodItemsByGroupKidsAsync(Id, group);
            FoodItemByGroupCombo.Clear();
            foreach (var item in data)
            {
                FoodItemByGroupCombo.Add(item);
            }

        }
        private async void GetFoodItemByGroupSpecial(int Id, string group)
        {
            var data = await new FoodItemService().GetFoodItemsByGroupSpecialAsync(Id, group);
            FoodItemByGroupSpecial.Clear();
            foreach (var item in data)
            {
                FoodItemByGroupSpecial.Add(item);
            }

        }

    }
}
