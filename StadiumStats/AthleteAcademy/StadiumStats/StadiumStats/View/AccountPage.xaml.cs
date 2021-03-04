using StadiumStats.Controller;
using StadiumStats.Model;
using StadiumStats.Util;
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
    public partial class AccountPage : ContentPage
    {
        private User user;
        private UserController userController;
        public AccountPage()
        {
            InitializeComponent();
            userController = new UserController();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
        }

        public void logOut(object sender, EventArgs e) 
        {
            App.Current.MainPage = new LoginPage();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            fillAccountPage();
        }

        private void fillAccountPage()
        {
            entryAccFirstName.Text = user.firstName;
            entryAccLastName.Text = user.lastName;
            entryAccEmail.Text = user.email;
            entryAccPassword.Text = user.password;
            entryAccConfirmPassword.Text = user.password;
        }

        public void updateUser(object sender, EventArgs e)
        {
            if (checkFormInputs())
            {
                updateUserAccount();
            }
        }

        private async void updateUserAccount()
        {
            User newUser = new User(user.id,entryAccFirstName.Text, entryAccLastName.Text, entryAccEmail.Text, user.username, entryAccPassword.Text, user.userType);
            bool flag = await userController.update(newUser);
            if (flag)
            {
                user = newUser;
                showMessage("User Updated Succesfully");
            }
            else
            {
                showErrorMessage("Error Updating User");
            }
        }

        private bool checkFormInputs()
        {
            bool flag = false;
            if (checkInput(entryAccFirstName.Text))
            {
                showRequiredMessage("First Name");
            }
            else if (checkInput(entryAccLastName.Text))
            {
                showRequiredMessage("Last Name");
            }
            else if (checkInput(entryAccEmail.Text))
            {
                showRequiredMessage("Email");
            }
            else if (checkInput(entryAccPassword.Text))
            {
                showRequiredMessage("Password");
            }
            else if (checkInput(entryAccConfirmPassword.Text))
            {
                showRequiredMessage("Confirm Password");
            }
            else if (!entryAccPassword.Text.Equals(entryAccConfirmPassword.Text))
            {
                showErrorMessage("Passwords dont match");
            }
            else
            {
                flag = true;
            }
            return flag;
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

       /* private void isActivitySpinnerShowing(bool status)
        {
            if (status.Equals(true))
            {
                signUpLayout.IsVisible = false;
                signUpLayout.IsEnabled = false;
                activitySpinnerLayout.IsVisible = true;
                signUpSpinner.IsVisible = true;
                signUpSpinner.IsRunning = true;
                signUpSpinner.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerLayout.IsVisible = false;
                signUpLayout.IsVisible = true;
                signUpLayout.IsEnabled = true;
                signUpSpinner.IsVisible = false;
                signUpSpinner.IsRunning = false;
                signUpSpinner.IsEnabled = false;

            }
        }*/



    }
}