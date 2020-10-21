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

    }
}
