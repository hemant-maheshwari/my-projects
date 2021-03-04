using ExpenseManager.Models;
using LibVLCSharp.Shared;
using MediaManager;
using Microcharts;
using Plugin.Media.Abstractions;
using SkiaSharp;
using StadiumStats.Controller;
using StadiumStats.DataStructure;
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
    public partial class ImprovementsPage : ContentPage
    {
        private User user;
        private AthleteController athleteController;
        private StatsController statsController;

        public ImprovementsPage()
        {
            InitializeComponent();
            Init();

        }

        public void Init()
        {
            BackgroundColor = Constants.backgroundColor;
            statsController = new StatsController();
            athleteController = new AthleteController();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Chart1.Chart = new RadarChart { Entries = entries };
            user = Application.Current.Properties[CommonSettings.GLOBAL_USER] as User;
            initializeAthleteList();

        }


        private async void initializeAthleteList()
        {
            List<Athlete> athletes = await athleteController.getAthletesForUser(user.id);
            athletePicker.ItemsSource = athletes;
        }

        public void improveGraph(int a, string label1, int b, string label2, int c, string label3)
        {
            List<ChartEntry> entries = new List<ChartEntry>
            {
                new ChartEntry(a)
             {
                 Color = SKColor.Parse("#000000"),
                 Label = label1,
                 TextColor = SKColor.Parse("#000000"),
                 ValueLabel = a.ToString()
             },
               new ChartEntry(b)
             {
                 Color = SKColor.Parse("#00FA18"),
                 Label = label2,
                 TextColor = SKColor.Parse("#000000"),
                 ValueLabel = b.ToString()
             },
               new ChartEntry(c)
             {
                 Color = SKColor.Parse("#FF0050"),
                 Label = label3,
                 TextColor = SKColor.Parse("#000000"),
                 ValueLabel = c.ToString()
             }
        };
            Chart1.Chart = new RadarChart { Entries = entries };

        }
        public async void showImprovement(object sender, EventArgs e)
        {
            improvePicDecide("", "");
            if (athletePicker.SelectedIndex != -1)
            {

                Athlete athlete = (Athlete)athletePicker.SelectedItem;
                List<Stats> athleteStats = await statsController.getAllStatsForAthlete(athlete.id);
                if (athlete.athleteType.Equals("Player"))
                {
                    PlayerImprovement playerImprovement = getPlayerImprovement(athleteStats);
                    compareSkills(playerImprovement);
                }
                else
                {
                    GoalieImprovement goalieImprovement = getGoalieImprovement(athleteStats);
                    compareSkills(goalieImprovement);
                }
            }
        }

        private void compareSkills(PlayerImprovement playerImprovement)
        {
            if (playerImprovement.ballSkills < 1 && playerImprovement.defense < 1 && playerImprovement.offense < 1)
            {
                showMessage("Player Stats Must Be Added");
                App.Current.MainPage = new StatsPage();
            }
            else if (playerImprovement.offense <= playerImprovement.defense && playerImprovement.offense <= playerImprovement.ballSkills)
            {
                showMessage("Player Offense Needs Work");
                improvePicDecide("playerOffenseDrill1.jpg", "playerOffenseDrill2.jpg");

            }
            else if (playerImprovement.defense <= playerImprovement.offense && playerImprovement.defense <= playerImprovement.ballSkills)
            {
                showMessage("Player Defense Needs Work");
                improvePicDecide("playerDefenseDrill1.jpg", "playerDefenseDrill2.png");
            }
            else
            {
                showMessage("Player Ball Skills Needs Work");
                improvePicDecide("playerBallSkillsDrill1.jpg", "playerBallSkillsDrill2.jpg");
            }
            int po = Convert.ToInt32(playerImprovement.offense);
            int pd = Convert.ToInt32(playerImprovement.defense);
            int pb = Convert.ToInt32(playerImprovement.ballSkills);
            improveGraph(po, "Offense", pd, "Defense", pb, "Ball Skills");
        }

        private void compareSkills(GoalieImprovement goalieImprovement)
        {
            if (goalieImprovement.handSkills <= goalieImprovement.defense && goalieImprovement.handSkills <= goalieImprovement.ballSkills)
            {
                showMessage("Goalie Hand Skills Needs Work");
                improvePicDecide("goalieHandSkillsDrill1.jpg", "goalieHandSkillsDrill2.jpg");
            }
            else if (goalieImprovement.defense <= goalieImprovement.handSkills && goalieImprovement.defense <= goalieImprovement.ballSkills)
            {
                showMessage("Goalie Defense Needs Work");
                improvePicDecide("goalieDefenseDrill1.jpg", "goalieDefenseDrill2.jpg");
            }
            else
            {
                showMessage("Goalie Ball Skills Needs Work");
                improvePicDecide("goalieBallSkillsDrill1.jpg", "goalieBallSkillsDrill2.png");
            }
            int gh = Convert.ToInt32(goalieImprovement.handSkills);
            int gd = Convert.ToInt32(goalieImprovement.defense);
            int gb = Convert.ToInt32(goalieImprovement.ballSkills);
            improveGraph(gb, "Ball Skills", gd, "Defense", gh, "Hand Skills");
        }

        private PlayerImprovement getPlayerImprovement(List<Stats> stats)
        {
            PlayerImprovement playerImprovement = new PlayerImprovement();
            for (int i = 0; i < stats.Count; i++)
            {
                playerImprovement.offense = playerImprovement.offense + stats[i].getPlayerOffense();
                playerImprovement.ballSkills = playerImprovement.ballSkills + stats[i].getPlayerBallSKills();
                playerImprovement.defense = playerImprovement.defense + stats[i].getPlayerDefense();
            }
            playerImprovement.offense = (playerImprovement.offense / stats.Count) / 6 * 100;
            playerImprovement.defense = (playerImprovement.defense / stats.Count) / 4 * 100;
            playerImprovement.ballSkills = (playerImprovement.ballSkills / stats.Count) / 6 * 100;

            return playerImprovement;
        }

        private GoalieImprovement getGoalieImprovement(List<Stats> stats)
        {
            GoalieImprovement goalieImprovement = new GoalieImprovement();
            for (int i = 0; i < stats.Count; i++)
            {
                goalieImprovement.handSkills = goalieImprovement.handSkills + stats[i].getGoalieHandSkills();
                goalieImprovement.ballSkills = goalieImprovement.ballSkills + stats[i].getGoalieBallSkills();
                goalieImprovement.defense = goalieImprovement.defense + stats[i].getGoalieDefense();
            }
            goalieImprovement.handSkills = (goalieImprovement.handSkills / stats.Count) / 4 * 100;
            goalieImprovement.defense = (goalieImprovement.defense / stats.Count) / 8 * 100;
            goalieImprovement.ballSkills = (goalieImprovement.ballSkills / stats.Count) / 4 * 100;

            return goalieImprovement;

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
        
        public void improvePicDecide(string url, string url2)
        {
            improvePic1.Source = url;
            improvePic2.Source = url2;
        }
    }
}       