using FoodOrderApp.Model;
using FoodOrderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedPlaceView : ContentPage
    {
        SelectedPageViewModel cvm;
        public SelectedPlaceView(Category category)
        {
            cvm = new SelectedPageViewModel(category);
            InitializeComponent();
            this.BindingContext = cvm;
        }

        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            
            var selectedPlace = e.CurrentSelection.FirstOrDefault() as Itemplace;
            if (selectedPlace == null)
                return;
            await Navigation.PushModalAsync(new PlaceViewPage(selectedPlace));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}