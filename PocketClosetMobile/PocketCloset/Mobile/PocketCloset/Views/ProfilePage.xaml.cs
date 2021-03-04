using Plugin.Media;
using Plugin.Media.Abstractions;
using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
using PocketCloset.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {

        private OutfitController outfitController;
        private User user;
        private FollowerController followerController;
        private FollowerController followingController;
        private ProfilePictureController profilePictureController;
        private String imagePath;



        public ProfilePage()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            profilePictureController = new ProfilePictureController();
            outfitController = new OutfitController();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            followerController = new FollowerController();
            followingController = new FollowerController();

            List<FollowViewModel> followers = await followerController.getAllFollowers(user.userId);
            lblFollowersCount.Text = followers.Count().ToString();

            List<FollowViewModel> following = await followingController.getAllFollowing(user.userId);
            lblFollowingCount.Text = following.Count().ToString();

            lblUsername.Text = user.username;
            lblname.Text = (user.firstName + " " + user.lastName);

            ProfilePicture profilePicture = await profilePictureController.getModel(user.userId);
            if (profilePicture != null) {
                if (profilePicture.profilePicture != " ")
                {
                    string profilePicString = profilePicture.profilePicture;
                    profilePic.Source = getImageSourceFromString(profilePicString);
                }
            }

            List<OutfitViewModel> outfitViewModels = await outfitController.getOutfits(user.userId);

            Image[] clothImageHolder = { pic1Shirt, pic1Pant, pic2Shirt, pic2Pant, pic3Shirt, pic3Pant, pic4Shirt, pic4Pant, pic5Shirt, pic5Pant, pic6Shirt, pic6Pant };

            for (int i = 0; i < outfitViewModels.Count; i = i + 2)
            {
                string shirtString = outfitViewModels[i].clothPicString;
                string pantString = outfitViewModels[i + 1].clothPicString;

                clothImageHolder[i].Source = getImageSourceFromString(shirtString);
                clothImageHolder[i + 1].Source = getImageSourceFromString(pantString);


            }




            int outfitCount = outfitViewModels.Count() / 2;

            lblPostsCount.Text = outfitCount.ToString();
;

        }

        private ImageSource getImageSourceFromString(string imgString) {
            byte[] Base64Stream = Convert.FromBase64String(imgString);
            ImageSource imsSrc = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            return imsSrc;
        }

        public void goToFollowersPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new FollowersPage();
        }

        public void goToFollowingPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new FollowingPage();
        }
        public void goToAccountPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new SettingsPage();
        }

        public async void uploadProfilePicture(object sender, EventArgs e)
        {
            

            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("no upload", "picking a photo is not supported", "ok");
                    return;
                }

                MediaFile file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null)
                    return;


                Stream photoStream = file.GetStream();

                profilePic.Source = ImageSource.FromStream(() => photoStream);
                imagePath = file.Path;


                string proflePictureString = imageToBase64();
                ProfilePicture profilePicture = new ProfilePicture(user.userId, proflePictureString);
                bool flag = await profilePictureController.updateModel(profilePicture);
                if (flag)
                {
                    await DisplayAlert("Message", "Profile Picture Changed!", "Okay");
                    profilePic.Source = getImageSourceFromString(profilePicture.profilePicture);
                }
                /*if (flag) {
                    flag = await profilePictureController.createModel(profilePicture);
                    if (flag)
                    {
                        await DisplayAlert("Message", "Profile Picture Changed!", "Okay");
                    }
                }*/

            };
        }

        public string imageToBase64()
        {

            using (var image = File.OpenRead(imagePath))
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

    }
}