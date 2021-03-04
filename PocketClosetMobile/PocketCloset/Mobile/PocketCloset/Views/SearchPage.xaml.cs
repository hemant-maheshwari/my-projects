using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private UserController userController;
        private User user;
        private FollowerController followerController;

        public SearchPage()
        {
            InitializeComponent();
            Init();
        }
        public void Init() //initialize screen components
        {
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
            followerController = new FollowerController();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
        }
        public void searchButtonPressed(object sender, EventArgs e) //function when search button is pressed
        {
            var keyword = followUserBar.Text;
               
            if (keyword.Length < 3 || keyword.Length > 20)
            {
                isActivitySpinnerShowing(false);
                DisplayAlert("Invalid Search", "Enter a valid UserName", "Okay");
                followUserBar.Focus();
            }
            else 
            {
                isActivitySpinnerShowing(true);
                searchUser();
            }
        }

        public async void followUser(object sender, EventArgs e)  //add friend info sent to controller
        {
            string followeruserName = foundFriend.Text;
            User friendUser = await userController.getUserFromUsername(followeruserName);
            Follower follower = new Follower(user.userId, friendUser.userId);
            bool flag = await followerController.createModel(follower);
            if (flag)
            {
                isActivitySpinnerShowing(true);
                await DisplayAlert("Message", "You now follow "+friendUser.firstName+ " "+friendUser.lastName, "Okay");
                App.Current.MainPage = new NavPage(user);

            }
            else
            {
                await DisplayAlert("Message", "Error Occured", "Okay");
                App.Current.MainPage = new NavPage(user);
            }

        }
        private async void searchUser() // searches database for matching user
        {
            isActivitySpinnerShowing(true);
            try
            {
                User foundUser = await getUserFromUsername(followUserBar.Text);
                if (foundUser != null)
                {
                    isActivitySpinnerShowing(false);
                    foundFriend.Text = foundUser.username;
                    foundFriend.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Message", "User not Found", "Okay");
                    isActivitySpinnerShowing(false);
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                await DisplayAlert("Error", "Error occurred.", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<User> getUserFromUsername(string username) // calls for user from rest api
        {
            return await userController.getUserFromUsername(username);
        }
        

        public void isActivitySpinnerShowing(bool status) //determines the visibility activity spinner
        {
            if (status.Equals(true))
            {
                followUserLayout.IsVisible = false;
                followUserLayout.IsEnabled = false;
                activitySpinnerSearchLayout.IsVisible = true;
                searchLoader.IsVisible = true;
                searchLoader.IsRunning = true;
                searchLoader.IsEnabled = true;
                
               
            }
            if (status.Equals(false))
            {
                activitySpinnerSearchLayout.IsVisible = false;
                followUserLayout.IsVisible = true;
                followUserLayout.IsEnabled = true;
                searchLoader.IsVisible = false;
                searchLoader.IsRunning = false;
                searchLoader.IsEnabled = false;
                
            }
        }
    }
}