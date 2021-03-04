using PocketCloset.Models;
using PocketCloset.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCloset.ViewModels
{
    class NavPageViewModel
    {
        public HomePage homepageTab { set; get; }
        public SearchPage searchpageTab { set; get; }
        public CreateOutfitPage createoutfitpageTab { set; get; }
        public ProfilePage profilepageTab { set; get; }
        public SettingsPage settingspageTab { set; get; }        
    }
}
