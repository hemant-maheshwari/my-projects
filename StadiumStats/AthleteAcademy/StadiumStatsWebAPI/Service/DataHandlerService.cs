using PocketClosetWebServiceAPI.Services;
using StadiumStats.Model;
using StadiumStatsWebAPI.Model;
using System.Collections.Generic;

namespace StadiumStatsWebAPI.Service
{
    public class DataHandlerService
    {
        private MySQLService mySQLService;
        public DataHandlerService(string connString) {
            mySQLService = new MySQLService(connString);
        }

        public User checkUser(User user) 
        {
            string query = "select * from user where username = '"+user.username+"' and password = '"+user.password+"';";
            return mySQLService.getResult<User>(query);
        }
        public User validateUser(string username)
        {
            string query = "select * from user where username = '"+username+"';";
            return mySQLService.getResult<User>(query);
        }

        public List<Athlete> getAthletes(int userId) {
            string query = "select * from athlete where userid = '" + userId + "';";
            return mySQLService.getAthletesForUser(query);
        }

        public List<Stats> getAllStats(int athleteId) {
            string query = "select * from stats where athleteId = '" + athleteId + "';";
            return mySQLService.getResults<Stats>(query);
        }

    }
}
