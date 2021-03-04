using ExpenseManager.Models;
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
    public partial class StatsPage : ContentPage
    {
        private User user;
        private Stats stats;
        private Athlete selectedAthlete;

        private AthleteController athleteController;
        private StatsController statsController;

        public StatsPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init() 
        {
            BackgroundColor = Constants.backgroundColor;
            athleteController = new AthleteController();
            statsController = new StatsController();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            hideAllStats();
            initializeAthleteList();
            selectedAthlete = new Athlete();
        }

        private void hideAllStats()
        {
            athletePicker.IsVisible = true;
            athletePicker.IsEnabled = true;
            playerStatsLayout.IsVisible = false;
            playerStatsLayout.IsEnabled = false;
            goalieStatsLayout.IsEnabled = false;
            goalieStatsLayout.IsVisible = false;
        }

        private async void initializeAthleteList() {
            List<Athlete> athletes = await athleteController.getAthletesForUser(user.id);
            athletePicker.ItemsSource = athletes;
        }

        public void showAthleteStats(object sender, EventArgs e)
        {
            if (athletePicker.SelectedIndex != -1) {
                selectedAthlete = (Athlete)athletePicker.SelectedItem;
                stats = new Stats();
                string athleteType = selectedAthlete.athleteType;
                if (athleteType.Equals("Player"))
                {
                    playerStatsLayout.IsVisible = true;
                    playerStatsLayout.IsEnabled = true;
                    goalieStatsLayout.IsEnabled = false;
                    goalieStatsLayout.IsVisible = false;
                }
                else if (athleteType.Equals("Goalie"))
                {
                    goalieStatsLayout.IsEnabled = true;
                    goalieStatsLayout.IsVisible = true;
                    playerStatsLayout.IsVisible = false;
                    playerStatsLayout.IsEnabled = false;
                }
            }            
        }

        private string getCurrentDate() {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        private static Color buttonChangeColor = Color.FromRgb(255,0,0);
        private static Color regularColor = Color.FromRgb(255,255,255);

        public void kickPrecisionGood(object sender, EventArgs e) {
            stats.kickPrecision = 1;
            kpGood.BackgroundColor = buttonChangeColor;
            kpBad.BackgroundColor = regularColor;
        }
        public void kickPrecisionBad(object sender, EventArgs e)
        {
            stats.kickPrecision = 0;
            kpGood.BackgroundColor = regularColor;
            kpBad.BackgroundColor = buttonChangeColor;
        }
        public void kickEaseGood(object sender, EventArgs e)
        {
            stats.kickEasiness = 1;
            keGood.BackgroundColor = buttonChangeColor;
            keBad.BackgroundColor = regularColor;
        }
        public void kickEaseBad(object sender, EventArgs e)
        {
            stats.kickEasiness = 0;
            keGood.BackgroundColor = regularColor;
            keBad.BackgroundColor = buttonChangeColor;
        }
        public void headballPrecisionGood(object sender, EventArgs e)
        {
            stats.headballPrecision = 1;
            hpGood.BackgroundColor = buttonChangeColor;
            hpBad.BackgroundColor = regularColor;   
        }
        public void headballPrecisionBad(object sender, EventArgs e)
        {
            stats.headballPrecision = 0;
            hpGood.BackgroundColor = regularColor;
            hpBad.BackgroundColor = buttonChangeColor;
        }
        public void headballCoordinationGood(object sender, EventArgs e)
        {
            stats.headballCoordination = 1;
            hcGood.BackgroundColor = buttonChangeColor;
            hcBad.BackgroundColor = regularColor;
        }
        public void headballCoordinationBad(object sender, EventArgs e)
        {
            stats.headballCoordination = 0;
            hcGood.BackgroundColor = regularColor;
            hcBad.BackgroundColor = buttonChangeColor;
        }
        public void throwInPrecisionGood(object sender, EventArgs e)
        {
            stats.throwInPrecision = 1;
            tpGood.BackgroundColor = buttonChangeColor;
            tpBad.BackgroundColor = regularColor;
        }
        public void throwInPrecisionBad(object sender, EventArgs e)
        {
            stats.throwInPrecision = 0;
            tpGood.BackgroundColor = regularColor;
            tpBad.BackgroundColor = buttonChangeColor;
        }
        public void throwInCoordinationGood(object sender, EventArgs e)
        {
            stats.throwInCoordination = 1;
            tcGood.BackgroundColor = buttonChangeColor;
            tcBad.BackgroundColor = regularColor;
        }
        public void throwInCoordinationBad(object sender, EventArgs e)
        {
            stats.throwInCoordination = 0;
            tcGood.BackgroundColor = regularColor;
            tcBad.BackgroundColor = buttonChangeColor;
        }
        public void trapCoordinationGood(object sender, EventArgs e)
        {
            stats.trapCoordination = 1;
            trcGood.BackgroundColor = buttonChangeColor;
            trcBad.BackgroundColor = regularColor;
        }
        public void trapCoordinationBad(object sender, EventArgs e)
        {
            stats.trapCoordination = 0;
            trcGood.BackgroundColor = regularColor;
            trcBad.BackgroundColor = buttonChangeColor;
        }
        public void trapEasinessGood(object sender, EventArgs e)
        {
            stats.trapEasiness = 1;
            treGood.BackgroundColor = buttonChangeColor;
            treBad.BackgroundColor = regularColor;
        }
        public void trapEasinessBad(object sender, EventArgs e)
        {
            stats.trapEasiness = 0;
            treGood.BackgroundColor = buttonChangeColor;
            treBad.BackgroundColor = regularColor;
        }
        public void tacklingPrecisionGood(object sender, EventArgs e)
        {
            stats.tacklingPrecision = 1;
            tacpGood.BackgroundColor = buttonChangeColor;
            tacpBad.BackgroundColor = regularColor;
        }
        public void tacklingPrecisionBad(object sender, EventArgs e)
        {
            stats.tacklingPrecision = 0;
            tacpGood.BackgroundColor = regularColor;
            tacpBad.BackgroundColor = buttonChangeColor;
        }
        public void tacklingRapidityGood(object sender, EventArgs e)
        {
            stats.tacklingRapidity = 1;
            tacrGood.BackgroundColor = buttonChangeColor;
            tacrBad.BackgroundColor = regularColor;
        }
        public void tacklingRapidityBad(object sender, EventArgs e)
        {
            stats.tacklingRapidity = 0;
            tacrGood.BackgroundColor = regularColor;
            tacrBad.BackgroundColor = buttonChangeColor;
        }
        public void playerDribblingCoordinationGood(object sender, EventArgs e)
        {
            stats.playerDribblingCoordination = 1;
            pdcGood.BackgroundColor = buttonChangeColor;
            pdcBad.BackgroundColor = regularColor;
        }
        public void playerDribblingCoordinationBad(object sender, EventArgs e)
        {
            stats.playerDribblingCoordination = 0;
            pdcGood.BackgroundColor = regularColor;
            pdcBad.BackgroundColor = buttonChangeColor;
        }
        public void playerDribblingEasinessGood(object sender, EventArgs e)
        {
            stats.playerDribblingEasiness = 1;
            pdeGood.BackgroundColor = buttonChangeColor;
            pdeBad.BackgroundColor = regularColor;
        }
        public void playerDribblingEasinessBad(object sender, EventArgs e)
        {
            stats.playerDribblingEasiness = 0;
            pdeGood.BackgroundColor = regularColor;
            pdeBad.BackgroundColor = buttonChangeColor;
        }
        public void footworkFluencyGood(object sender, EventArgs e)
        {
            stats.footworkFluency = 1;
            fwfGood.BackgroundColor = buttonChangeColor;
            fwfBad.BackgroundColor = regularColor;
        }
        public void footworkFluencyBad(object sender, EventArgs e)
        {
            stats.footworkFluency = 0;
            fwfGood.BackgroundColor = regularColor;
            fwfBad.BackgroundColor = buttonChangeColor;
        }
        public void footworkCoordinationGood(object sender, EventArgs e)
        {
            stats.footworkCoordination = 1;
            fwcGood.BackgroundColor = buttonChangeColor;
            fwcBad.BackgroundColor = regularColor;
        }
        public void footworkCoordinationBad(object sender, EventArgs e)
        {
            stats.footworkCoordination = 0;
            fwcGood.BackgroundColor = regularColor;
            fwcBad.BackgroundColor = buttonChangeColor;
        }
        public void ballProtectionCoordinationGood(object sender, EventArgs e)
        {
            stats.ballProtectionCoordination = 1;
            bpcGood.BackgroundColor = buttonChangeColor;
            bpcBad.BackgroundColor = regularColor;
        }
        public void ballProtectionCoordinationBad(object sender, EventArgs e)
        {
            stats.ballProtectionCoordination = 0;
            bpcGood.BackgroundColor = regularColor;
            bpcBad.BackgroundColor = buttonChangeColor;
        }
        public void ballProtectionRapidityGood(object sender, EventArgs e)
        {
            stats.ballProtectionRapidity = 1;
            bprGood.BackgroundColor = buttonChangeColor;
            bprBad.BackgroundColor = regularColor;
        }
        public void ballProtectionRapidityBad(object sender, EventArgs e)
        {
            stats.ballProtectionRapidity = 0;
            bprGood.BackgroundColor = regularColor;
            bprBad.BackgroundColor = buttonChangeColor;
        }


        /*Goalie Part*/



        public void ballCatchingPrecisionGood(object sender, EventArgs e)
        {
            stats.ballCatchingPrecision = 1;
            bcpGood.BackgroundColor = buttonChangeColor;
            bcpBad.BackgroundColor = regularColor;
        }
        public void ballCatchingPrecisionBad(object sender, EventArgs e)
        {
            stats.ballCatchingPrecision = 0;
            bcpGood.BackgroundColor = regularColor;
            bcpBad.BackgroundColor = buttonChangeColor;
        }
        public void ballCatchingCoordinationGood(object sender, EventArgs e)
        {
            stats.ballCatchingCoordination = 1;
            bccGood.BackgroundColor = buttonChangeColor;
            bccBad.BackgroundColor = regularColor;
        }
        public void ballCatchingCoordinationBad(object sender, EventArgs e)
        {
            stats.ballCatchingCoordination = 0;
            bccGood.BackgroundColor = regularColor;
            bccBad.BackgroundColor = buttonChangeColor;
        }
        public void plungePrecisionGood(object sender, EventArgs e)
        {
            stats.plungePrecision = 1;
            ppGood.BackgroundColor = buttonChangeColor;
            ppBad.BackgroundColor = regularColor;
        }
        public void plungePrecisionBad(object sender, EventArgs e)
        {
            stats.plungePrecision = 0;
            ppGood.BackgroundColor = regularColor;
            ppBad.BackgroundColor = buttonChangeColor;
        }
        public void plungeRapidityGood(object sender, EventArgs e)
        {
            stats.plungeRapidity = 1;
            prGood.BackgroundColor = buttonChangeColor;
            prBad.BackgroundColor = regularColor;
        }
        public void plungeRapidityBad(object sender, EventArgs e)
        {
            stats.plungeRapidity = 0;
            prGood.BackgroundColor = regularColor;
            prBad.BackgroundColor = buttonChangeColor;
        }
        public void boxingBallFluencyGood(object sender, EventArgs e)
        {
            stats.boxingBallFluency = 1;
            bbfGood.BackgroundColor = buttonChangeColor;
            bbfBad.BackgroundColor = regularColor;
        }
        public void boxingBallFluencyBad(object sender, EventArgs e)
        {
            stats.boxingBallFluency = 0;
            bbfGood.BackgroundColor = regularColor;
            bbfBad.BackgroundColor = buttonChangeColor;
        }
        public void boxingBallEaseGood(object sender, EventArgs e)
        {
            stats.boxingBallEase = 0;
            bbeGood.BackgroundColor = buttonChangeColor;
            bbeBad.BackgroundColor = regularColor;
        }
        public void boxingBallEaseBad(object sender, EventArgs e)
        {
            stats.boxingBallEase = 0;
            bbeGood.BackgroundColor = regularColor;
            bbeBad.BackgroundColor = buttonChangeColor;
        }
        public void divertingBallCoordinationGood(object sender, EventArgs e)
        {
            stats.divertingBallCoordination = 1;
            dbcGood.BackgroundColor = buttonChangeColor;
            dbcBad.BackgroundColor = regularColor;
        }
        public void divertingBallCoordinationBad(object sender, EventArgs e)
        {
            stats.divertingBallCoordination = 0;
            dbcGood.BackgroundColor = regularColor;
            dbcBad.BackgroundColor = buttonChangeColor;
        }
        public void divertingBallEaseGood(object sender, EventArgs e)
        {
            stats.divertingBallEase = 1;
            dbeGood.BackgroundColor = buttonChangeColor;
            dbeBad.BackgroundColor = regularColor;
        }
        public void divertingBallEaseBad(object sender, EventArgs e)
        {
            stats.divertingBallEase = 0;
            dbeGood.BackgroundColor = regularColor;
            dbeBad.BackgroundColor = buttonChangeColor;
        }
        public void handThrowingPrecisionGood(object sender, EventArgs e)
        {
            stats.handThrowingPrecision = 1;
            htpGood.BackgroundColor = buttonChangeColor;
            htpBad.BackgroundColor = regularColor;
        }
        public void handThrowingPrecisionBad(object sender, EventArgs e)
        {
            stats.handThrowingPrecision = 0;
            htpGood.BackgroundColor = regularColor;
            htpBad.BackgroundColor = buttonChangeColor;
        }
        public void handThrowingCoordinationGood(object sender, EventArgs e)
        {
            stats.handThrowingCoordination = 1;
            htcGood.BackgroundColor = buttonChangeColor;
            htcBad.BackgroundColor = regularColor;
        }
        public void handThrowingCoordinationBad(object sender, EventArgs e)
        {
            stats.handThrowingCoordination = 0;
            htcGood.BackgroundColor = regularColor;
            htcBad.BackgroundColor = buttonChangeColor;
        }
        public void blockingPrecisionGood(object sender, EventArgs e)
        {
            stats.blockingPrecision = 1;
            bpGood.BackgroundColor = buttonChangeColor;
            bpBad.BackgroundColor = regularColor;
        }
        public void blockingPrecisionBad(object sender, EventArgs e)
        {
            stats.blockingPrecision = 0;
            bpGood.BackgroundColor = regularColor;
            bpBad.BackgroundColor = buttonChangeColor;
        }
        public void blockingRapidityGood(object sender, EventArgs e)
        {
            stats.blockingRapidity = 1;
            brGood.BackgroundColor = buttonChangeColor;
            brBad.BackgroundColor = regularColor;
        }
        public void blockingRapidityBad(object sender, EventArgs e)
        {
            stats.blockingRapidity = 0;
            brGood.BackgroundColor = regularColor;
            brBad.BackgroundColor = buttonChangeColor;
        }
        public void goalieDribblingFluencyGood(object sender, EventArgs e)
        {
            stats.goalieDribblingFluency = 1;
            gdfGood.BackgroundColor = buttonChangeColor;
            gdfBad.BackgroundColor = regularColor;
        }
        public void goalieDribblingFluencyBad(object sender, EventArgs e)
        {
            stats.goalieDribblingFluency = 0;
            gdfGood.BackgroundColor = regularColor;
            gdfBad.BackgroundColor = buttonChangeColor;
        }
        public void goalieDribblingEaseGood(object sender, EventArgs e)
        {
            stats.goalieDribblingEase = 1;
            gdeGood.BackgroundColor = buttonChangeColor;
            gdeBad.BackgroundColor = regularColor;
        }
        public void goalieDribblingEaseBad(object sender, EventArgs e)
        {
            stats.goalieDribblingEase = 0;
            gdeGood.BackgroundColor = regularColor;
            gdeBad.BackgroundColor = buttonChangeColor;
        }
        public void ballKickPrecisionGood(object sender, EventArgs e)
        {
            stats.ballKickPrecision = 1;
            bkpGood.BackgroundColor = buttonChangeColor;
            bkpBad.BackgroundColor = regularColor;
        }
        public void ballKickPrecisionBad(object sender, EventArgs e)
        {
            stats.ballKickPrecision = 0;
            bkpGood.BackgroundColor = regularColor;
            bkpBad.BackgroundColor = buttonChangeColor;
        }
        public void ballKickFluencyGood(object sender, EventArgs e)
        {
            stats.ballKickFluency = 1;
            bkfGood.BackgroundColor = buttonChangeColor;
            bkfBad.BackgroundColor = regularColor;
        }
        public void ballKickFluencyBad(object sender, EventArgs e)
        {
            stats.ballKickFluency = 0;
            bkfGood.BackgroundColor = regularColor;
            bkfBad.BackgroundColor = buttonChangeColor;
        }

        public void playerStatSave(object sender, EventArgs e)
        {
             saveStats();
        }

        public void goalieStatSave(object sender, EventArgs e)
        {
            saveStats();
        }

        private async void saveStats() {
            stats.gameDate = getCurrentDate();
            stats.athleteId = selectedAthlete.id;
            bool flag = await statsController.create(stats);
            if (flag)
            {
                showMessage("Stats Added For " + selectedAthlete.fullName + "!");
                App.Current.MainPage = new NavPage(user);
            }
            else {
                showErrorMessage("Error Occured!");
            }
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