using FoodOrderApp.Model;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class PlacesViewModel : BaseViewModel
    {
        private string _UserName;

        public string UserName
        {
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserName;
            }
        }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Itemplace> LatestItems { get; set; }

        public Category SelectedCategory { get; set; }






        public Command LogoutCommand { get; set; }
        public Command ViewOrdersHistoryCommand { get; set; }

        public PlacesViewModel()
        {

            var uname = Preferences.Get("Username", String.Empty);
            if (String.IsNullOrEmpty(uname))
                UserName = "Guest";
            else
                UserName = uname;

            LogoutCommand = new Command(async () => await LogoutAsync());
            ViewOrdersHistoryCommand = new Command(async () => await ViewOrderHistoryAsync());
            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<Itemplace>();
            


            GetCategories();
            GetLatestItems();
        }

        private async Task ViewOrderHistoryAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView());
        }



        private async Task LogoutAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
        }

        private async void GetCategories()
        {
            var data = await new CategoryDataService().GetCategoriesAsync();
            Categories.Clear();
            foreach (var item in data)
            {
                Categories.Add(item);
            }
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
