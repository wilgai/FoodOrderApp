﻿using System;
using System.Threading.Tasks;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class LogoutViewModel : BaseViewModel
    {
        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            set
            {
                _UserCartItemsCount = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserCartItemsCount;
            }
        }

        private bool _IsCartExists;
        public bool IsCartExists
        {
            set
            {
                _IsCartExists = value;
                OnPropertyChanged();
            }

            get
            {
                return _IsCartExists;
            }
        }

        public Command LogoutCommand { get; set; }
        public Command GotoCartCommand { get; set; }

        public LogoutViewModel()
        {
            

            IsCartExists = (UserCartItemsCount > 0) ? true : false;

            LogoutCommand = new Command(async () => await LogoutUserAsync());
            GotoCartCommand = new Command(async () => await GotoCartAsync());
        }

        private async Task GotoCartAsync()
        {
            
        }

        private async Task LogoutUserAsync()
        {
         
        
            Preferences.Remove("Username");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginView());

        }
    }
}
