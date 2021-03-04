using Plugin.Media;
using Plugin.Media.Abstractions;
using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
using PocketCloset.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPostPage : ContentPage
    {
        private UserController userController;
        private ClothController clothController;
        private PostController postController;
        private PostRecordController postRecordController;

        private User user;
        private string imagePath;

        public AddPostPage()
        {
            InitializeComponent();
            Init();
           

        }
        protected async override void OnAppearing() //populate feed with user posts
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            boxViewFollowing3.Color = Constants.logoColor;
        }
        public void Init() //initilize screen components
        {
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
            clothController = new ClothController();
            postController = new PostController();
            postRecordController = new PostRecordController();
        }
        public async void cancelPost(object sender, EventArgs e)
        {
           bool option = await DisplayAlert("Confirmation", "Do you want to delete this Post and return Home", "Yes", "No");
            Debug.WriteLine("Confirmation: " + (option?"Yes":"No"));
            if (option == true)
            {
                App.Current.MainPage = new NavPage(user);
            }
            else { };
                
        }

        public async void createPost(object sender, EventArgs e) // creation of a new post with required fields
        {
            
            try
            {
                string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
                string imageString = imageToBase64();
                Cloth cloth = new Cloth(user.userId, pickerCLothType.SelectedItem.ToString(), imageString, entryColor.Text, entryMaterial.Text, pickerSeason.SelectedItem.ToString());
                Cloth newCloth = await clothController.createCloth(cloth);
                if (newCloth != null)
                {
                    Post post = new Post(user.userId, newCloth.clothId, Double.Parse(entryPrice.Text), entryUrl.Text, switchModel.IsToggled);
                    Post newPost = await postController.createPost(post);
                    if (newPost != null)
                    {
                        PostRecord postRecord = new PostRecord(user.userId, newPost.postId, todayDate);
                        
                        bool flag = await postRecordController.createModel(postRecord);
                        if (flag)
                        {
                            await DisplayAlert("Message", "Post Create Successfully!", "Okay");
                            App.Current.MainPage = new NavPage(user);
                        }

                        else
                        {
                            await DisplayAlert("Message", "Error Occured!", "Okay");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public string imageToBase64() // converting image to base64
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

        public interface CameraInterface //interface for selecting picture
        {
            void BringUpPhotoGallery();
        }
        public async void pickPhotoButton(object sender, EventArgs e) // selecting picture from camera roll
        {
        
            addpostLayout.IsVisible = true;
            addpostLayout.IsEnabled = true;
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

                pickPhotoImage.Source = ImageSource.FromStream(() => photoStream);
                imagePath = file.Path;



            };
        }


    }
}