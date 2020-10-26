using Common.Web.Entities;
using FoodOrderApp.Model;
using FoodOrderApp.Services;
using System.Collections.ObjectModel;

namespace FoodOrderApp.ViewModels
{
    public class SelectedPageViewModel : BaseViewModel
    {

        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged();
            }

            get
            {
                return _SelectedCategory;
            }
        }

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

        public ObservableCollection<Itemplace> ItemsByPlaceByFoodCategory { get; set; }
        public ObservableCollection<Itemplace> LatestItems { get; set; }
        public SelectedPageViewModel(Category category)
        {
            SelectedCategory = category;
            ItemsByPlaceByFoodCategory = new ObservableCollection<Itemplace>();
            GetPlaceItemByFoodCategory(category.CategoryID);
            LatestItems = new ObservableCollection<Itemplace>();
            GetLatestItems();
        }

        private async void GetPlaceItemByFoodCategory(int iD)
        {
            var data = await new PlaceItemService().GetFoodItemsByCategoryAsync(iD);
            ItemsByPlaceByFoodCategory.Clear();
            foreach (var item in data)
            {
                ItemsByPlaceByFoodCategory.Add(item);
            }
            TotalFoodItems = ItemsByPlaceByFoodCategory.Count;
        }

        private async void GetLatestItems()
        {
            var data = await new PlaceItemService().GetLatestPlaceItemsAsync();
            LatestItems.Clear();
            foreach (var item in data)
            {
                LatestItems.Add(item);
            }
        }
    }
}
