using ExpenseManager.Models;
using Microcharts;
using SkiaSharp;
using StadiumStats.Controller;
using StadiumStats.Model;
using StadiumStats.Util;
using StadiumStats.ViewModel;
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
    public partial class HomePage : ContentPage
    {

        private User user;

        private AthleteController athleteController;

        public HomePage()
        {
            InitializeComponent();
            Init();
            
        }
        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            athleteController = new AthleteController();
        }
        public void addAthlete (object sender, EventArgs e)
        {
           App.Current.MainPage = new AddAthlete();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            List<Athlete> athletes = await athleteController.getAthletesForUser(user.id);
            List<AthleteViewModel> athleteViewModels = getAthletesViewModelFromAthletes(athletes);
            athleteListView.ItemsSource = athleteViewModels;
        }

        private List<AthleteViewModel> getAthletesViewModelFromAthletes(List<Athlete> athletes) {
            List<AthleteViewModel> athleteViewModels = new List<AthleteViewModel>();
            for (int i=0; i<athletes.Count; i++) {
                athleteViewModels.Add(getAthleteViewModel(athletes[i]));
            }
            return athleteViewModels;
        }

        private AthleteViewModel getAthleteViewModel(Athlete athlete) {
            AthleteViewModel athleteViewModel = new AthleteViewModel(athlete.firstName, athlete.lastName, athlete.athletePic);
            return athleteViewModel;
        }

    }
}