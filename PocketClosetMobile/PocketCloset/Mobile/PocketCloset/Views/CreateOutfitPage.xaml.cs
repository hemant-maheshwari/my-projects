using Plugin.Media;
using Plugin.Media.Abstractions;
using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
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
    public partial class CreateOutfitPage : ContentPage
    {
        private ClothController clothController;
        private OutfitController outfitController;
        private User user;
        private string imagePathPant;
        private string imagePathShirt;
        public CreateOutfitPage(User user)
        {
            InitializeComponent();
            Init();

        }
        public CreateOutfitPage()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            clothController = new ClothController();
            outfitController = new OutfitController();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            

        }


        public async void uploadShirt(object sender, EventArgs e)
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

                img_shirt.Source = ImageSource.FromStream(() => photoStream);
                imagePathShirt = file.Path;



            };
        }

        public async void uploadPant(object sender, EventArgs e)
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

                img_pant.Source = ImageSource.FromStream(() => photoStream);
                imagePathPant = file.Path;
            };
        }

        public async void saveOutfit(object sender, EventArgs e)
        {
            string outfitName = await DisplayPromptAsync("Outfit Name", "Outfit Name");
            string imageStringPant = imageToBase64(imagePathPant);
            string imageStringShirt = imageToBase64(imagePathShirt);
            Cloth clothPant = new Cloth(user.userId, imageStringPant);
            Cloth newPant = await clothController.createCloth(clothPant);
            Cloth clothShirt = new Cloth(user.userId, imageStringShirt);
            Cloth newShirt = await clothController.createCloth(clothShirt);            
            Outfit outfitShirt = new Outfit(user.userId, outfitName, newShirt.clothId);            
            Outfit outfitPant = new Outfit(user.userId, outfitName, newPant.clothId);            
            bool flag = await outfitController.createModel(outfitShirt);
            if (flag)            {
                flag = await outfitController.createModel(outfitPant);
                if (flag) {
                    await DisplayAlert("Message", "Outfit Created!", "Okay");
                }                
            }
            else {
                await DisplayAlert("Message", "Outfit Creation Failed!", "Okay");
            }
        
            
        
        
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


    }
}