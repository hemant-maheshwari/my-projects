using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
using PocketCloset.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FollowingPage : ContentPage
    {
        
        private FollowerController followingController;
        private User user;
        public FollowingPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init() // initialize screen components
        {
            BackgroundColor = Constants.backgroundColor;
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            boxViewFollowing.Color = Constants.logoColor;
            followingController = new FollowerController();
        }
        public void goToHomePage(object sender, EventArgs e) // navigattion to home page
        {
            App.Current.MainPage = new NavPage(user);
        }
        public void followingListItemTapped(object sender, ItemTappedEventArgs e)
        {
           // Application.Current.MainPage = new AccountPage(user);
        } 
       protected async override void OnAppearing() //shows what appear on screen and poupltaes posts
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            List<FollowViewModel> following = await followingController.getAllFollowing(user.userId);
            followingListView.ItemsSource = following;
        }
        public void isActivitySpinnerShowing(bool status) //determines the visibility activity spinner
        {
            if (status.Equals(true))
            {
                followingLayout.IsVisible = false;
                followingLayout.IsEnabled = false;
                activitySpinnerFollowingLayout.IsVisible = true;
                followingLoader.IsVisible = true;
                followingLoader.IsRunning = true;
                followingLoader.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerFollowingLayout.IsVisible = false;
                followingLayout.IsVisible = true;
                followingLayout.IsEnabled = true;
                followingLoader.IsVisible = false;
                followingLoader.IsRunning = false;
                followingLoader.IsEnabled = false;

            }

        }
  
    }

}
// List<FollowViewModel> followers = await followerController.getAllFollowers(user.userId);
