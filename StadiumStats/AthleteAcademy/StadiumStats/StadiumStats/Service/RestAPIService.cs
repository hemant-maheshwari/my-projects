using Newtonsoft.Json;
using StadiumStats.DataStructure;
using StadiumStats.Model;
using StadiumStats.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StadiumStats.Service
{

    public class RestAPIService: WebAPIConfiguration
    {

        private static string FORWARD_SLASH = "/";

        public async Task<User> checkUser(User user) {
            String url = WEB_API_BASE_URL + "user" + FORWARD_SLASH + "check";
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Response responseObject = await getHTTPResponse(response);
                    return getUserFromResponse(responseObject);
                }
                else
                {
                    Debug.WriteLine("Error Occured!");
                    return default(User);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(User);
            }
        }
        public async Task<User> getUserFromUsernameAsync(string username)
        {
            string url = WEB_API_BASE_URL + "user" + FORWARD_SLASH + "validateUsername" + FORWARD_SLASH + username;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getUserFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(User);
            }
        }

        private async Task<Response> getHTTPResponse(HttpResponseMessage response)
        {
            string result = await response.Content.ReadAsStringAsync();
            Response responseObject = JsonConvert.DeserializeObject<Response>(result);
            return responseObject;
        }

        private User getUserFromResponse(Response response)
        {
            string userString = response.data;
            User returnedModel = JsonConvert.DeserializeObject<User>(userString);
            return returnedModel;
        }

        public async Task<List<Athlete>> getAthletesForUser(int userId) {
            string url = WEB_API_BASE_URL + "athlete" + FORWARD_SLASH + "getAthletes" + FORWARD_SLASH + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getAthleteList(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<Athlete>);
            }
        }

        private List<Athlete> getAthleteList(Response response) {
            string athleteString = response.data;
            Athlete[] returnedModel = JsonConvert.DeserializeObject<Athlete[]>(athleteString);
            return returnedModel.ToList<Athlete>();
        }

        public async Task<List<Stats>> getAllStatsForAthlete(int athleteId)
        {
            string url = WEB_API_BASE_URL + "stats" + FORWARD_SLASH + "getAllStats" + FORWARD_SLASH + athleteId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getStatsList(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<Stats>);
            }
        }

        private List<Stats> getStatsList(Response response)
        {
            string statsString = response.data;
            Stats[] returnedModel = JsonConvert.DeserializeObject<Stats[]>(statsString);
            return returnedModel.ToList<Stats>();
        }



    }
}
