using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PocketCloset.Controller;
using PocketCloset.Models;
using PocketCloset.Util;
using Xamarin.Forms;
using System.Drawing.Imaging;
using Xamarin.Forms.PlatformConfiguration;

using Xamarin.Forms.Xaml;
using System.Diagnostics;
using PocketCloset.ViewModels;

namespace PocketCloset.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private UserController userController;
        private ClothController clothController;
        private PostController postController;
        private PostRecordController postRecordController;

        private User user;
        private string imagePath;
        

        public HomePage(){
            InitializeComponent();
            Init();
            boxViewFollowing1.Color = Constants.logoColor;
            
        }
        protected async override void OnAppearing() //populate feed with user posts
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            List<FeedViewModel> feeds = await postController.getAllFeeds(user.userId);
            feedListView.ItemsSource = feeds;
           // isActivitySpinnerShowing(false);
        }

        public void addPostForm(object sender, EventArgs e)
        {
            App.Current.MainPage = new AddPostPage();
        }
        public void Init() //initilize screen components
        {
            BackgroundColor = Constants.backgroundColor;
            userController = new UserController();
            clothController = new ClothController();
            postController = new PostController();
            postRecordController = new PostRecordController();         

        }

       
        
      
        }
        

    }

