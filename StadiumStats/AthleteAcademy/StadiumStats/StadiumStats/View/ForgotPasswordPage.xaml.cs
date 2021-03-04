using ExpenseManager.Models;
using StadiumStats.Controller;
using StadiumStats.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace StadiumStats.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        private UserController userController;
        private User user;
        public ForgotPasswordPage()
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

        public void goToLoginPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

        public void checkForUser(object sender, EventArgs e)
        {
            verifyUser(entryUsername.Text);
        }
        public async void verifyUser(string username) { 
            isForgotPasswordLayoutShowing(false);
            isActivitySpinnerShowing(true);
            try
            {
                User confirmedUser = await getUserFromUsername(username);
                if (confirmedUser.username.Equals(username))
                {
                    isActivitySpinnerShowing(false);
                    isUpdatePasswordLayoutShowing(true);
                    user = confirmedUser;
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isForgotPasswordLayoutShowing(true);
                    showErrorMessage("No user associated with username");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isForgotPasswordLayoutShowing(true);
                showErrorMessage("Error Occured in Database");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<User> getUserFromUsername(string username)
        {
            return await userController.getUserFromUsername(username);
        }
        public void passwordVerify(object sender, EventArgs e)
        {
            if (passwordsMatch(entryNewPassword.Text, entryConfirmNewPassword.Text).Equals(false))
            {
                showErrorMessage("Passwords do not match");
            }
            else
            {
                isUpdatePasswordLayoutShowing(false);
                isActivitySpinnerShowing(true);
                updatePassword(entryNewPassword.Text);

            }
        }
        private async void updatePassword(string password)
        {
            user.password = password;
            try
            {
                if (await userController.update(user))
                {
                    showMessage("Password Updated Succesfully");
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isForgotPasswordLayoutShowing(true);
                    showErrorMessage("Error Occured! Try Again");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isForgotPasswordLayoutShowing(true);
                showErrorMessage("Error Occurred");
                Debug.WriteLine(ex.Message);
            }
        }
        private bool passwordsMatch(string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }
        private void isActivitySpinnerShowing(bool status)
        {
            if (status.Equals(true))
            {
                activitySpinnerLayout.IsVisible = true;
                forgotPasswordLoader.IsVisible = true;
                forgotPasswordLoader.IsRunning = true;
                forgotPasswordLoader.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerLayout.IsVisible = false;
                forgotPasswordLoader.IsVisible = false;
                forgotPasswordLoader.IsRunning = false;
                forgotPasswordLoader.IsEnabled = false;
            }
        }

        private void isForgotPasswordLayoutShowing(bool status)
        {
            if (status.Equals(true))
            {
                forgotPasswordLayout.IsEnabled = true;
                forgotPasswordLayout.IsVisible = true;
            }
            if (status.Equals(false))
            {
                forgotPasswordLayout.IsVisible = false;
                forgotPasswordLayout.IsEnabled = false;
            }
        }

        private void isUpdatePasswordLayoutShowing(bool status)
        {
            if (status.Equals(true))
            {
                updatePasswordLayout.IsVisible = true;
                updatePasswordLayout.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                updatePasswordLayout.IsEnabled = false;
                updatePasswordLayout.IsVisible = false;
            }
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

        private bool checkFormInputs()
        {
            bool flag = false;
            if (checkInput(entryUsername.Text))
            {
                showRequiredMessage("Username");
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

    }
}