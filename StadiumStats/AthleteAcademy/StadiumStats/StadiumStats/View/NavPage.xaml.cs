using ExpenseManager.Models;
using StadiumStats.Model;
using StadiumStats.Util;
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
    public partial class NavPage : TabbedPage
    {
        public NavPage(User user)
        {
            Application.Current.Properties[CommonSettings.GLOBAL_USER] = user;
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;

        }

    }
}