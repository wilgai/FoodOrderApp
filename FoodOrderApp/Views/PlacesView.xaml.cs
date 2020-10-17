using FoodOrderApp.Model;
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
    public partial class PlacesView : ContentPage
    {
        public PlacesView()
        {
            InitializeComponent();
        }
        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var itemplace = e.CurrentSelection.FirstOrDefault() as Itemplace;
            if (itemplace == null)
                return;

            await Navigation.PushModalAsync(new PlaceViewPage(itemplace));

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}