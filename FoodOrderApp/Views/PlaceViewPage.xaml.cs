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
    public partial class PlaceViewPage : ContentPage
    {

        PlaceViewPageViewModel cvm;
        int selectionIndex = 0;
        List<Label> tabHeaders = new List<Label>();
        List<VisualElement> tabContents = new List<VisualElement>();
        public PlaceViewPage(Itemplace itemplace)
        {
            cvm = new PlaceViewPageViewModel(itemplace);
            InitializeComponent();
            this.BindingContext = cvm;
            tabHeaders.Add(All);
            tabHeaders.Add(Combo);
            tabHeaders.Add(Drinks);
            tabHeaders.Add(Kids);
            tabHeaders.Add(Special);

            tabContents.Add(AllContent);
            tabContents.Add(ComboContent);
            tabContents.Add(DrinksContent);
            tabContents.Add(KidsContent);
            tabContents.Add(SpecialContent);


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

        private async Task ShowSelection(int newTab)
        {
            if (newTab == selectionIndex) return;

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