using PocketCloset.Controller;
using PocketCloset.Models;
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
    public partial class ForgotPasswordPage : ContentPage
    {
        UserController userController;
        SecQuestionController secQuestionController;
        User user;
        SecQuestion secQuestion;

        public ForgotPasswordPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            btnBackToLogin.TextColor = Constants.logoColor;

            userController = new UserController();
            secQuestionController = new SecQuestionController();
            user = new User();
            secQuestion = new SecQuestion();
        }

        public void goToLoginPage(object sender, EventArgs e) //takes user to login page
        {
            App.Current.MainPage = new LoginPage();
        }

        public void verifyUsernameForm(object sender, EventArgs e) //verifies if username form was input correctly
        {
            if (entryUsername.Text == "" || entryUsername.Text == null)
            {
                DisplayAlert("Invalid Username", "Pease enter your username.", "Okay");
                entryUsername.Focus();
            }
            else
            {
                isEnterUsernameLayoutShowing(false);
                isActivitySpinnerShowing(true);
                checkExistingUsername();
            }
        }


        private async void checkExistingUsername()  //sees if username exists in database already
        {
            try
            {
                User completeUser = await getUserFromUsername(entryUsername.Text);
                if (completeUser.userId != 0)
                {
                    secQuestion = await getSecurityQuestion(completeUser.userId);
                    lblSecQuestion.Text = secQuestion.question;
                    isActivitySpinnerShowing(false);
                    isEnterSecQuestionLayoutShowing(true);
                    user = completeUser;
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isEnterUsernameLayoutShowing(true);
                    entryUsername.Text = "";
                    await DisplayAlert("Invalid Username", "There is no account associated with this username.", "Okay");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isEnterUsernameLayoutShowing(true);
                entryUsername.Text = "";
                await DisplayAlert("Error", "Error occurred.", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task<SecQuestion> getSecurityQuestion(int userId) //gets security question from database based off of user id
        {
            return await secQuestionController.getModel(userId);
        }

        private async Task<User> getUserFromUsername(string username)  //sends username to controller to get user
        {
            return await userController.getUserFromUsername(username);
        }

        public void verifySecQuestionAnswerForm(object sender, EventArgs e) //verifies if update password form was inputted correctly
        {
            if (entrySecQuestionAnswer.Text == null || entrySecQuestionAnswer.Text == "")
            {
                DisplayAlert("Invalid Answer", "Please enter your answer.", "Okay");
                entrySecQuestionAnswer.Focus();
            }
            else if (entrySecQuestionAnswer.Text != secQuestion.answer)
            {
                entrySecQuestionAnswer.Text = "";
                DisplayAlert("Invalid Answer", "Incorrect answer. Try again.", "Okay");
                entrySecQuestionAnswer.Focus();
            }
            else
            {
                isEnterSecQuestionLayoutShowing(false);
                isUpdatePasswordLayoutShowing(true);
            }
        }


        public void verifyUpdatedPassword(object sender, EventArgs e)  //verifies if update password form was inputted correctly
        {
            if (entryNewPassword.Text == "" || entryNewPassword.Text == null)
            {
                DisplayAlert("Invalid Entry", "Please enter your new password.", "Okay");
                entryNewPassword.Focus();
            }
            else if (entryConfirmNewPassword.Text == "" || entryConfirmNewPassword.Text == null)
            {
                DisplayAlert("Invalid Entry", "Please confirm your new password", "Okay");
                entryConfirmNewPassword.Focus();
            }
            else if (passwordsMatch(entryNewPassword.Text, entryConfirmNewPassword.Text).Equals(false))
            {
                DisplayAlert("Invalid Password", "Passwords do not match.", "Okay");
                entryNewPassword.Text = "";
                entryConfirmNewPassword.Text="";
            }
            else
            {
                isUpdatePasswordLayoutShowing(false);
                isActivitySpinnerShowing(true);
                updatePassword(entryNewPassword.Text);

            }
        }

        private async void updatePassword(string password)  //sends updated password to user controller to be saved
        {
            user.password = password;
            try
            {
                bool flag = await userController.updateModel(user);
                if (flag)
                {
                    await DisplayAlert("Update Success", "Password updated successfully", "Okay");
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isUpdatePasswordLayoutShowing(true);
                    entryNewPassword.Text = "";
                    entryConfirmNewPassword.Text = "";
                    await DisplayAlert("Update Unsuccessful", "Something went wrong.", "Okay");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isUpdatePasswordLayoutShowing(true);
                await DisplayAlert("Error", "Error occurred", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private bool passwordsMatch(string password, string confirmPassword)  //checks to see if new password and confirm password match each other
        {
            return password.Equals(confirmPassword);
        }


        private void isActivitySpinnerShowing(bool status) // displays/hides  activity spinner
        {
            if (status.Equals(true))
            {
                activitySpinnerLayout.IsVisible = true;
                forgotPasswordLoader.IsVisible = true;
                forgotPasswordLoader.IsRunning = true;
                forgotPasswordLoader.IsEnabled = true;

            }
            else
            {
                activitySpinnerLayout.IsVisible = false;
                forgotPasswordLoader.IsVisible = false;
                forgotPasswordLoader.IsRunning = false;
                forgotPasswordLoader.IsEnabled = false;
            }
        }

        private void isEnterUsernameLayoutShowing(bool status)  // displays/hides enter username page
        {
            if (status.Equals(true))
            {
                enterUsernameLayout.IsEnabled = true;
                enterUsernameLayout.IsVisible = true;

            }
            else
            {
                enterUsernameLayout.IsVisible = false;
                enterUsernameLayout.IsEnabled = false;
            }
        }

        private void isUpdatePasswordLayoutShowing(bool status) // displays/hides update password page
        {
            if (status.Equals(true))
            {
                updatePasswordLayout.IsEnabled = true;
                updatePasswordLayout.IsVisible = true;

            }
            else
            {
                updatePasswordLayout.IsVisible = false;
                updatePasswordLayout.IsEnabled = false;
            }
        }

        private void isEnterSecQuestionLayoutShowing(bool status) // displays/hides security question page
        {
            if (status.Equals(true))
            {
                enterSecQuestionLayout.IsEnabled = true;
                enterSecQuestionLayout.IsVisible = true;

            }
            else
            {
                enterSecQuestionLayout.IsVisible = false;
                enterSecQuestionLayout.IsEnabled = false;
            }
        }
    }
}
