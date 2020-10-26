using Common.Web.Entities;
using FoodOrderApp.Model;
using FoodOrderApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceViewPage : ContentPage
    {

        PlaceViewPageViewModel cvm;
        int selectionIndex = 0;
        List<Label> tabHeaders = new List<Label>();
        List<VisualElement> tabContents = new List<VisualElement>();
        private string _group;
        public PlaceViewPage(Itemplace itemplace)
        {
            cvm = new PlaceViewPageViewModel(itemplace);
            InitializeComponent();
            this.BindingContext = cvm;

            tabHeaders.Add(Tab1);
            tabHeaders.Add(Tab2);
            tabHeaders.Add(Tab3);
            tabHeaders.Add(Tab4);
            tabHeaders.Add(Tab5);

            tabContents.Add(All);
            tabContents.Add(Combo);
            tabContents.Add(Drinks);
            tabContents.Add(Kids);
            tabContents.Add(Special);
        }


        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void CollectionView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var selectedProduct = e.CurrentSelection.FirstOrDefault() as FoodItem;
            if (selectedProduct == null)
                return;
            await Navigation.PushModalAsync(new ProductDetailsView(selectedProduct));
            ((CollectionView)sender).SelectedItem = null;
        }

        private async Task ShowSelection(int newTab)
        {

            //Getting the text of the selected tab
            //_group = tabHeaders[newTab].Text;
            // navigate the selection pill
            var selectdTabLabel = tabHeaders[newTab];
            _ = SelectionUnderline.TranslateTo(selectdTabLabel.Bounds.X, 0, 150, easing: Easing.SinInOut);

            await tabContents[selectionIndex].FadeTo(0);
            tabContents[selectionIndex].IsVisible = false;
            tabContents[newTab].IsVisible = true;
            _ = tabContents[newTab].FadeTo(1);

            selectionIndex = newTab;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var tabIndex = tabHeaders.IndexOf((Label)sender);
            await ShowSelection(tabIndex);
        }
    }
}