using ExpenseManager.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using StadiumStats.Controller;
using StadiumStats.Model;
using StadiumStats.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StadiumStats.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAthlete : ContentPage
    {
        private User user;
        private string imagePath;

        private AthleteController athleteController;

        public AddAthlete()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            athleteController = new AthleteController();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
        }
        public void goToHomePage(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavPage(user);
        }
        private string imageToBase64(string imgPath)
        {
            using (var image = File.OpenRead(imgPath))
            {
                using (MemoryStream m = new MemoryStream())
                {

                    image.CopyTo(m);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }

        private ImageSource getImageSourceFromString(string imgString)
        {
            byte[] Base64Stream = Convert.FromBase64String(imgString);
            ImageSource imsSrc = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            return imsSrc;
        }
        public async void uploadAthletePicture(object sender, EventArgs e)
        {
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("no upload", "picking a photo is not supported", "okay");
                    return;
                }
                MediaFile file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null)
                    return;
                Stream photoStream = file.GetStream();
                athleteImage.Source = ImageSource.FromStream(() => photoStream);
                imagePath = file.Path;
            };
        }

        public async void addNewAthlete(object sender, EventArgs e)
        {
            if (checkFormInputs()) {
                string imageString = imageToBase64(imagePath);
                Athlete athlete = new Athlete(user.id, imageString, entryFirstName.Text, entryLastName.Text, athleteType.SelectedItem.ToString());
                bool flag = await athleteController.create(athlete);
                if (flag)
                {
                    showMessage("Athlete Added!");
                    App.Current.MainPage = new NavPage(user);
                }
                else {
                    showErrorMessage("Error Adding Athlete");
                }
            }
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
            else if (athleteType.SelectedItem == null)
            {
                showRequiredMessage("Athlete Type");
            }
            else if(imagePath.Equals("") || imagePath.Equals(null))
            {
                showRequiredMessage("Athlete Picture");
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
    }
}