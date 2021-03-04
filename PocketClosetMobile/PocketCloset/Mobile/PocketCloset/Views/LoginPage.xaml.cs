using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using PocketCloset.Controller;
using PocketCloset.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private UserController userController;
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()  //initalizes components for pages
        { 
            BackgroundColor = Constants.backgroundColor;
            lblUsername.TextColor = Constants.initialScreensTextColor;
            lblPassword.TextColor = Constants.initialScreensTextColor;
            isActivitySpinnerShowing(false);
            userController = new UserController();
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            btnForgotPassword.TextColor = Constants.logoColor;
        }

        public void goToSignUpPage(object sender, EventArgs e) //takes uer to sign up page
        {
            App.Current.MainPage = new SignUpPage();
        }

        public void verifyLoginForm(object sender, EventArgs e) //verifies in login form was input correctly
        {
            if (entryUsername.Text == " " || entryUsername.Text == null)
            {
                DisplayAlert("Invalid username", "Please enter a username", "Okay");
                entryUsername.Focus();
            }
            else if (entryPassword.Text == " " || entryPassword.Text == null)
            {
                DisplayAlert("Invalid password", "Please enter a password.", "Okay");
                entryPassword.Focus();
            }
            else
            {
                isSignInLayoutShowing(false);
                isActivitySpinnerShowing(true);
                signIn();
            }
        }

        private async void signIn() //sends username and password to web api to see if account exists in database
        {
            User user = new User(entryUsername.Text, entryPassword.Text);
            try
            {
                user = await checkUserExistence(user);
                if (user != null)
                {
                    //DisplayAlert("Login", "Login Success", "Okay");
                    App.Current.MainPage = new NavPage(user);          //PASS USER AS PARAMETER!!!!!!!!!!!!!!!!!
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isSignInLayoutShowing(true);
                    await DisplayAlert("Login Failed", "Incorrect Username or Password", "Try Again");
                }
            }
            catch (Exception e)
            {
                isActivitySpinnerShowing(false);
                isSignInLayoutShowing(true);
                entryUsername.Text = "";
                entryPassword.Text = "";
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(e.Message);
            }
        }

        private async Task<User> checkUserExistence(User user) //checks if user exists in database
        {
            return await userController.checkUser(user);
        }

        private void isActivitySpinnerShowing(bool status) // displays/hides activity spinner
        {
            if (status.Equals(true))
            {
                activitySpinnerLayout.IsVisible = true;
                activitySpinnerLayout.IsEnabled = true;
                signInLoader.IsVisible = true;
                signInLoader.IsEnabled = true;
                signInLoader.IsRunning = true;
            }
            else
            {
                activitySpinnerLayout.IsVisible = false;
                activitySpinnerLayout.IsEnabled = false;
                signInLoader.IsVisible = false;
                signInLoader.IsEnabled = false;
                signInLoader.IsRunning = false;
            }
        }

        private void isSignInLayoutShowing(bool status) //displays/hides sign in form
        {
            if (status.Equals(true))
            {
                signInLayout.IsEnabled = true;
                signInLayout.IsVisible = true;
            }
            else
            { 
                signInLayout.IsEnabled = false;
                signInLayout.IsVisible = false;
            }
        }

        public void goToForgotPasswordPage(object sender, EventArgs e) //takes user to forgot password page
        {
            App.Current.MainPage = new ForgotPasswordPage();
        }

        //public void goToNavPage(object sender, EventArgs e)     //DELETE WHEN PUSHING
        //{                                                        //DELETE WHEN PUSHING
        //    App.Current.MainPage = new NavPage();                //DELETE WHEN PUSHING
        //}
    }
}
