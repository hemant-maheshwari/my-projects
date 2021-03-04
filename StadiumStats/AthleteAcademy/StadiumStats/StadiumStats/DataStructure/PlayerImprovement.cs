using System;
using System.Collections.Generic;
using System.Text;

namespace StadiumStats.DataStructure
{
    public class PlayerImprovement
    {
        public double offense { get; set; }
        public double defense { get; set; }
        public double ballSkills { get; set; }

        public PlayerImprovement() {
            offense = 0;
            defense = 0;
            ballSkills = 0;
        }
    }
}
