using ExpenseManager.Models;
using StadiumStats.Controller;
using StadiumStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StadiumStats.View
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

        public void Init()
        {
            LoginIcon.HeightRequest = Constants.LoginIconHeight;
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
        }

        public async void signIn(object sender, EventArgs e) {
            if (checkLoginInputs()) {
                isActivitySpinnerShowing(true);
                User user = new User(entryUsername.Text, entryPassword.Text);
                User finalUser = await checkUserExistence(user);
                if (finalUser != null)
                {
                    goToHomePage(finalUser);
                }
                else {
                    isActivitySpinnerShowing(false);
                    showErrorMessage("User not found!");
                }
            }
        }

        private bool checkInput(string inputValue)
        {
            return inputValue == null;
        }
        private void showRequiredMessage(string fieldName)
        {
            DisplayAlert("Required", fieldName + " is required", "Okay");
        }

        private void showErrorMessage(string errorMessage)
        {
            DisplayAlert("Error", errorMessage, "Okay");
        }

        private void showMessage(string message)
        {
            DisplayAlert("Message", message, "Okay");
        }

        private bool checkLoginInputs()
        {
            bool flag = false;
            if (checkInput(entryUsername.Text))
            {
                showRequiredMessage("User Name");
            }
            else if (checkInput(entryPassword.Text))
            {
                showRequiredMessage("Password");
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private async Task<User> checkUserExistence(User user)
        {
            return await userController.checkUser(user);
        }
        public void goToSignUpPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new SignUpPage();
        }

        public void goToForgotPasswordPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new ForgotPasswordPage(); 
        }

        public void goToHomePage(User user)
        {
            App.Current.MainPage = new NavPage(user);
        }
        private async void isActivitySpinnerShowing(bool status)
        {
            if (status.Equals(true))
            {
                signInLayout.IsVisible = false;
                signInLayout.IsEnabled = false;
                activitySpinnerLayout.IsVisible = true;
                loginPageSpinner.IsVisible = true;
                await loginPageSpinner.ProgressTo(.75,500, Easing.Linear);
                loginPageSpinner.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerLayout.IsVisible = false;
                signInLayout.IsVisible = true;
                signInLayout.IsEnabled = true;
                loginPageSpinner.IsVisible = false;
                loginPageSpinner.IsEnabled = false;
               
            }
        }

    }
}