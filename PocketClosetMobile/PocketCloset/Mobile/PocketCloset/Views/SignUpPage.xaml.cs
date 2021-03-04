using System;
using System.Collections.Generic;
using System.Linq;
using PocketCloset.Models;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using PocketCloset.Controller;
using System.Diagnostics;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private UserController userController;
        private SecQuestionController secQuestionController;
        private ProfilePictureController profilePictureController;
        private User user;
        public SignUpPage()
        {
            InitializeComponent();
            Init();
        }
        void Init()  //initializes components of page
        {
            BackgroundColor = Constants.backgroundColor;
            btnSignIn.TextColor = Constants.logoColor;
            userController = new UserController();
            secQuestionController = new SecQuestionController();
            profilePictureController = new ProfilePictureController();
            user = new User();
            datePickerDOB.MaximumDate = DateTime.Today;
            isActivitySpinnerShowing(false);
            isSecQuestionLayoutShowing(false);

        }

        public void verifyUserForm(object sender, EventArgs e)  //verifies if sign up form was input correctly
        {
            if (pickerUserType.SelectedItem == null)
            {
                pickerUserType.Focus();
            }
            else if (pickerGenderType.SelectedItem == null)
            {
                pickerGenderType.Focus();
            }
            else if (entryFirstName.Text == " " || entryFirstName.Text == null)
            {
                DisplayAlert("Invalid First Name", "Please enter your first name.", "Okay");
                entryFirstName.Focus();
            }
            else if (entryLastName.Text == " " || entryLastName.Text == null)
            {
                DisplayAlert("Invalid Last Name", "Please enter your last name.", "Okay");
                entryLastName.Focus();
            }
            else if (datePickerDOB.Date.Equals("") || datePickerDOB.Date.Equals(null) || datePickerDOB.Date.Equals(DateTime.Now))
            {
                DisplayAlert("Invalid Date of Birth", "Please enter a valid birthday", "Okay");
                datePickerDOB.Focus();
            }
            else if (entryEmail.Text == null || !Regex.IsMatch(entryEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                DisplayAlert("Invalid Email", "Please enter a valid email.", "Okay");
                entryEmail.Focus();
                entryEmail.Text = "";
            }
            else if (entryUsername.Text == null || entryUsername.Text == "")
            {
                DisplayAlert("Invalid Username", "Please enter a valid username", "Okay");
                entryUsername.Focus();
            }
            else if (entryPassword.Text == null || entryPassword.Text == "")
            {
                DisplayAlert("Invalid Password", "Please enter a valid password.", "Okay");
                entryPassword.Focus();
            }
            else if (entryConfirmPassword.Text == null || entryConfirmPassword.Text == "")
            {
                DisplayAlert("Invalid Confirmation", "Please enter your password again.", "Okay");
                entryConfirmPassword.Focus();
                entryConfirmPassword.Text = "";
            }
            else if (entryConfirmPassword.Text != entryPassword.Text)
            {
                DisplayAlert("Invalid Password Confirmation", "Passwords do not match. Try again.", "Okay");
                entryConfirmPassword.Focus();
                entryConfirmPassword.Text = "";
            }
            else
            {
                isSignUpLayoutShowing(false);
                isActivitySpinnerShowing(true);
                createUserAccount();

            }
        }

            private async Task<bool> checkUsernameExistence(string username) //makes a call to web api to see if new username exists in database already
        {
            return await userController.checkUsername(username);
        }

        public async void createUserAccount() //creates new user objects, sends to web api to create new user in database
        {
            User newUser = new User(pickerUserType.SelectedItem.ToString(), pickerGenderType.SelectedItem.ToString(), entryFirstName.Text, entryLastName.Text, datePickerDOB.Date.ToShortDateString(), entryEmail.Text, entryUsername.Text, entryPassword.Text);
            try
            {
                if (!await checkUsernameExistence(newUser.username))
                {
                    bool flag = await userController.createModel(newUser);
                    if (flag)
                    {
                        user = await userController.getUserFromUsername(newUser.username);
                        createBlankProfilePicture();
                        isActivitySpinnerShowing(false);
                        isSignUpLayoutShowing(false);
                        isSecQuestionLayoutShowing(true);
                        await DisplayAlert("Message", "User account created successfully!", "Okay");
                    }
                    else
                    {
                        isActivitySpinnerShowing(false);
                        isSignUpLayoutShowing(true);
                        await DisplayAlert("Message", "Error Occured!", "Okay");
                    }
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    isSignUpLayoutShowing(true);
                    await DisplayAlert("Message", "Username already exists!", "Okay");
                }
            }
            catch (Exception ex)
            {
                isActivitySpinnerShowing(false);
                isSignUpLayoutShowing(true);
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        public void verifySecQuestionForm(object sender, EventArgs e)  //verifies if security question form was input correctly
        {
            if(entrySecQuestion.Text == "" || entrySecQuestion.Text == null)
            {
                DisplayAlert("Invalid Security Question", "Please enter a valid security question.", "Okay");
                entrySecQuestion.Focus();
            }
            else if(entrySecQuestionAnswer.Text == "" || entrySecQuestionAnswer.Text == null)
            {
                DisplayAlert("Invalid Security Answer", "Please enter a valid answer", "Okay");
                entrySecQuestionAnswer.Focus();
            }
            else
            {
                isSecQuestionLayoutShowing(false);
                isActivitySpinnerShowing(true);
                createSecurityQuestion();
            }
        }

        private async void createBlankProfilePicture() {
            ProfilePicture profilePicture = new ProfilePicture(user.userId, " ");
            await profilePictureController.createModel(profilePicture);
        }

        private async void createSecurityQuestion()  //creates security question object, sends to web api to be created in database
        {
            SecQuestion secQuestion = new SecQuestion(user.userId, entrySecQuestion.Text, entrySecQuestionAnswer.Text);
            try
            {
                bool flag = await secQuestionController.createModel(secQuestion);
                if (flag)
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Message", "Security Question Created Successfully!", "Okay");
                    App.Current.MainPage = new LoginPage();
                }
                else
                {
                    isActivitySpinnerShowing(false);
                    await DisplayAlert("Message", "Error Occurred.", "Okay");

                }
            }catch(Exception ex)
            {
                isActivitySpinnerShowing(false);
                isSecQuestionLayoutShowing(true);
                await DisplayAlert("Message", "Error Occured!", "Okay");
                Debug.WriteLine(ex.Message);
            }
        }

        private void isActivitySpinnerShowing(bool status)  // displays/hides activity spinner
        {
            if (status.Equals(true))
            {
                activitySpinnerLayout.IsVisible = true;
                signUpLoader.IsVisible = true;
                signUpLoader.IsEnabled = true;
                signUpLoader.IsRunning = true;

            }
            if (status.Equals(false))
            {
                activitySpinnerLayout.IsVisible = false;
                signUpLoader.IsVisible = false;
                signUpLoader.IsEnabled = false;
                signUpLoader.IsRunning = false;

            }
        }

        private void isSignUpLayoutShowing(bool status) // displays/hides sign up form
        {
            if (status.Equals(true))
            {
                signUpLayout.IsVisible = true;
                signUpLayout.IsEnabled = true;

            }
            if (status.Equals(false))
            {
                signUpLayout.IsVisible = false;
                signUpLayout.IsEnabled = false;

            }
        }

        private void isSecQuestionLayoutShowing(bool status) //displays/hides sec question form
        {
            if (status.Equals(true))
            {
                secQuestionLayout.IsVisible = true;
                secQuestionLayout.IsEnabled = true;
            }
            if (status.Equals(false))
            {
                secQuestionLayout.IsVisible = false;
                secQuestionLayout.IsEnabled = false;
            }
        }

        public void goToLoginPage(object sender, EventArgs e) //returns user to login page
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}