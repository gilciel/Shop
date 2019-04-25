﻿namespace Shop.UIForms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using Shop.Common.Helpers;
    using Shop.Common.Models;
    using Shop.Common.Services;
    using Shop.UIForms.Helpers;
    using Shop.UIForms.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        private bool isRunning;
        private bool isEnabled;
        private readonly ApiService apiService;

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.Email = "gilcielphp@gmail.com";
            this.Password = "123456";
            this.IsRemember = true;
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailMessage,
                    Languages.Accept);
                //await Application.Current.MainPage.DisplayAlert(
                //    "Error",
                //    "You must enter an email.",
                //    "Accept"
                //    );
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an password.",
                    "Accept"
                    );
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.IsRunning = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                return;
            }
            var token = (TokenResponse)response.Result;
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Products = new ProductsViewModel();

            Settings.IsRemember = this.IsRemember;
            Settings.UserEmail = this.Email;
            Settings.UserPassword = this.Password;
            Settings.Token = JsonConvert.SerializeObject(token);

            mainViewModel.UserEmail = this.Email;
            mainViewModel.UserPassword = this.Password;

            Application.Current.MainPage = new MasterPage();
            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
    }
}
