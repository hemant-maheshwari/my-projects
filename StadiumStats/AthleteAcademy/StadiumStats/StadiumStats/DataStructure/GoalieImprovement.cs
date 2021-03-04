using System;
using System.Collections.Generic;
using System.Text;

namespace StadiumStats.DataStructure
{
    public class GoalieImprovement
    {
        public double ballSkills { get; set; }
        public double handSkills { get; set; }
        public double defense { get; set; }

        public GoalieImprovement()
        {
            ballSkills = 0;
            handSkills = 0;
            defense = 0;
        }
    }
}
