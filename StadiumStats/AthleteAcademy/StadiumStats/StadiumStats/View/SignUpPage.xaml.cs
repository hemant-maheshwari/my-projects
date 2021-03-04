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
    public partial class SignUpPage : ContentPage
    {
        private UserController userController;
        public SignUpPage()
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

        public async void createUser(object sender, EventArgs e)
        {
            if (checkFormInputs())
            {
                isActivitySpinnerShowing(true);
                User user = new User(entryFirstName.Text, entryLastName.Text, entryEmail.Text, entryUsername.Text, entryPassword.Text,pickerUserType.SelectedItem.ToString());
                if (!checkUserExistence(user.username))
                {
                    bool flag = await saveUser(user);
                    if (flag)
                    {
                        showMessage("User created successfully");
                        App.Current.MainPage = new LoginPage();
                    }
                    else
                    {
                        isActivitySpinnerShowing(false);
                        showErrorMessage("Something went wrong");
                    }
                }
            }
        }

        private bool checkInput(string inputValue)
        {
            return inputValue == null;
        }

        private void showRequiredMessage(string fieldName)
        {
            DisplayAlert("Required", fieldName+" is required", "Okay");
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
            if (checkInput(entryFirstName.Text))
            {
                showRequiredMessage("First Name");
            }
            else if (checkInput(entryLastName.Text))
            {
                showRequiredMessage("Last Name");
            }
            else if (checkInput(entryEmail.Text))
            {
                showRequiredMessage("Email");
            }
            else if (checkInput(entryUsername.Text))
            {
                showRequiredMessage("User Name");
            }
            else if (checkInput(entryPassword.Text))
            {
                showRequiredMessage("Password");
            }
            else if (checkInput(entryConfirmPassword.Text))
            {
                showRequiredMessage("Confirm Password");
            }
            else if (!entryPassword.Text.Equals(entryConfirmPassword.Text))
            {
                showErrorMessage("Passwords dont match");
            }
            else if (pickerUserType.SelectedItem == null)
            {
                showRequiredMessage("User Type");
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private bool checkUserExistence(string username)
        {
            return false;
        }

        private async Task<bool> saveUser(User user)
        {
            return await userController.create(user);
        }

        public void goToLoginPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

        private void isActivitySpinnerShowing(bool status)
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
        }


        }
}