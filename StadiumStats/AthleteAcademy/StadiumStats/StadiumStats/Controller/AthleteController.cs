using StadiumStats.Model;
using StadiumStats.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StadiumStats.Controller
{
    public class AthleteController: BaseController<Athlete>
    {

        private RestAPIService restAPIService;

        public AthleteController() {
            restAPIService = new RestAPIService();
        }

        public async Task<List<Athlete>> getAthletesForUser(int userId) {
            return await restAPIService.getAthletesForUser(userId);
        }

    }
}
