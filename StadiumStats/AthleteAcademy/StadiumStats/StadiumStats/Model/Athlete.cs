using StadiumStats.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StadiumStats.Model
{
    public class Athlete
    {

        public int id { get; set; }

        public int userId { get; set; }
        public string athletePic { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string athleteType { get; set; }
        public string fullName {
            get {
                return firstName + " " + lastName;
            }
        }

        public Athlete() { }

        public Athlete(int userId, string athletePic, string firstName, string lastName, string athleteType)
        {
            this.userId = userId;
            this.athletePic = athletePic;
            this.firstName = firstName;
            this.lastName = lastName;
            this.athleteType = athleteType;
        }
    }
}
