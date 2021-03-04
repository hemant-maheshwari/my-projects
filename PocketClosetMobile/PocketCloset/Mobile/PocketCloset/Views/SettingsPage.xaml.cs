using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private UserController userController;
        private User user;
         
        public SettingsPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()  //initializes components of page
        {
            BackgroundColor = Constants.backgroundColor;
            boxViewSettings.Color = Constants.logoColor;
            btnLogOut.BackgroundColor = Constants.logoColor;
            btnUpdateAccInfo.BackgroundColor = Constants.logoColor;
            userController = new UserController();
        }

        private void initializeAccountInfo()  //initializes settings form entries
        {
            entryAccFirstName.Text = user.firstName;
            entryAccLastName.Text = user.lastName;
            entryAccEmail.Text = user.email;
        }
        protected override void OnAppearing()  //brings user data to page when clicked on
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            initializeAccountInfo();

        }
        public void logOut(object sender, EventArgs e) //returns user to login page
        {
            App.Current.MainPage = new LoginPage();
        }

        public void updateUserForm(object sender, EventArgs e)  //verifies if form entries were input correctly
        {
            if (entryAccFirstName.Text == " " || entryAccFirstName.Text == null)
            {
                DisplayAlert("Invalid First Name", "Please enter your first name.", "Okay");
                entryAccFirstName.Focus();
            }
            else if (entryAccLastName.Text == " " || entryAccLastName.Text == null)
            {
                DisplayAlert("Invalid Last Name", "Please enter your last name.", "Okay");
                entryAccLastName.Focus();
            }
            else if (entryAccEmail.Text == null || !Regex.IsMatch(entryAccEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                DisplayAlert("Invalid Email", "Please enter a valid email.", "Okay");
                entryAccEmail.Focus();
                entryAccEmail.Text = "";
            }
            else if (entryAccPassword.Text != null && entryAccPassword.Text != "")
            {
                if (entryAccConfirmPassword.Text == null || entryAccConfirmPassword.Text == "")
                {
                    DisplayAlert("Invalid Confirmation", "Please enter your password again.", "Okay");
                    entryAccConfirmPassword.Focus();
                    entryAccConfirmPassword.Text = "";
                }
                else if (!passwordsMatch(entryAccPassword.Text, entryAccConfirmPassword.Text))
                {
                    DisplayAlert("Invalid Password Confirmation", "Passwords do not match. Try again.", "Okay");
                    entryAccConfirmPassword.Focus();
                    entryAccConfirmPassword.Text = "";
                }
                else
                {
                    user.firstName = entryAccFirstName.Text;
                    user.lastName = entryAccLastName.Text;
                    user.email = entryAccEmail.Text;
                    user.password = entryAccPassword.Text;
                    isUpdateAccLayoutShowing(false);
                    isActivitySpinnerShowing(true);
                    updateUserAccount();
                }
            }
            else
            {
                user.firstName = entryAccFirstName.Text;
                user.lastName = entryAccLastName.Text;
                user.email = entryAccEmail.Text;
                isUpdateAccLayoutShowing(false);
                isActivitySpinnerShowing(true);
                updateUserAccount();

            }

        }

        public async void updateUserAccount()  //sends user with new information to web api to be updated
        {
            user.updateUser(user.firstName, user.lastName, user.email, user.password);
            try
            {
                bool flag = await userController.updateModel(user);
                if (flag)
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Message", "User account updated successfully!", "Okay");
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isUpdateAccLayoutShowing(true);
                    await DisplayAlert("Message", "Error Occured!", "Okay");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isUpdateAccLayoutShowing(true);
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(ex.Message);
            }

        }

        private bool passwordsMatch(string password, string confirmPassword)    //checks to see if new password and confirm password match
        {
            return password.Equals(confirmPassword);
        }

        private void isActivitySpinnerShowing(bool status)  // displays/hides activity spinner
        {
            if (status.Equals(true))
            {
                activitySpinnerLayout.IsVisible = true;
                activitySpinnerLayout.IsEnabled = true;
                updateAccLoader.IsRunning = true;
                updateAccLoader.IsEnabled = true;
                updateAccLoader.IsVisible = true;

            }
            else
            {
                activitySpinnerLayout.IsVisible = false;
                activitySpinnerLayout.IsEnabled = false;
                updateAccLoader.IsRunning = false;
                updateAccLoader.IsEnabled = false;
                updateAccLoader.IsVisible = false;

            }
        }

        private void isUpdateAccLayoutShowing(bool status) //displays /hides update account form
        {
            if (status.Equals(true))
            {
                updateAccLayout.IsVisible = true;
                updateAccLayout.IsEnabled = true;
            }
            else
            {
                updateAccLayout.IsVisible = false;
                updateAccLayout.IsEnabled = false;
            }
        }
    }
}