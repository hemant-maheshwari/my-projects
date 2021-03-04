using StadiumStats.Model;
using StadiumStats.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StadiumStats.Controller
{
    public class StatsController: BaseController<Stats>
    {
        private RestAPIService restAPIService;

        public StatsController()
        {
            restAPIService = new RestAPIService();
        }

        public Task<List<Stats>> getAllStatsForAthlete(int athleteId) {
            return restAPIService.getAllStatsForAthlete(athleteId);
        }

    }
}
