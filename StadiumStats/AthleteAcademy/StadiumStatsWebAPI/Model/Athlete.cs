using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumStatsWebAPI.Model
{
    public class Athlete
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string athletePic { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string athleteType { get; set; }

        public Athlete()
        {

        }
    }
}
