using Newtonsoft.Json;
using PocketCloset.Models;
using PocketCloset.Util;
using PocketCloset.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PocketCloset.Service
{
    public class RestAPIService : WebAPIConfiguration
    {

        private async Task<Response> getHTTPResponse(HttpResponseMessage response)   //get response object from http response
        {
            string result = await response.Content.ReadAsStringAsync();
            Response responseObject = JsonConvert.DeserializeObject<Response>(result);
            return responseObject;
        }

        private User getUserFromResponse(Response response)     //get user object from response object
        {
            string userString = response.data;
            User user = JsonConvert.DeserializeObject<User>(userString);
            return user;
        }

        private Cloth getClothFromResponse(Response response) {                ///get cloth object from response object
            string clothString = response.data;
            Cloth cloth = JsonConvert.DeserializeObject<Cloth>(clothString);
            return cloth;
        }

        private Post getPostFromResponse(Response response)                  //get post object from response object
        {
            string postString = response.data;
            Post post = JsonConvert.DeserializeObject<Post>(postString);
            return post;
        }

        private List<FeedViewModel> getFeedViewModelsFromResponse(Response response) {                   //get list of feed view model from response object
            string feedsString = response.data;
            FeedViewModel[] models = JsonConvert.DeserializeObject<FeedViewModel[]>(feedsString);
            return models.ToList<FeedViewModel>();
        }

        private List<FollowViewModel> getFollowViewModelsFromResponse(Response response)                          //get list of follow view model from response object
        {
            string listString = response.data;
            FollowViewModel[] followViewModelArray = JsonConvert.DeserializeObject<FollowViewModel[]>(listString);
            return followViewModelArray.ToList<FollowViewModel>();
        }

        private List<OutfitViewModel> getOutfitViewModelsFromResponse(Response response)                              //get list of outfit view model from response object
        { 
            string listString = response.data;
            OutfitViewModel[] outfitViewModelArray = JsonConvert.DeserializeObject<OutfitViewModel[]>(listString);
            return outfitViewModelArray.ToList<OutfitViewModel>();
        }

        public async Task<bool> checkUsernameAsync(string username)           //checking to see if username exists  in database
        {
            string url = WEB_API_BASE_URL + "user/check/" + username;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return responseObject.status;
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return false;
            }
        }

        public async Task<User> checkUserAsync(User user)                            //checking to see if user exists  in database
        {
            string url = WEB_API_BASE_URL + "user/login/";
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

        public async Task<User> getUserFromUsernameAsync(string username)                  //receiving a user from webservice 
        {
            string url = WEB_API_BASE_URL + "user/validateUsername/" + username;
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
        public async Task<List<FollowViewModel>> getAllFollowersAsync(int userId)      //receiving a list of Follow view model from webservice 
        {
            string url = WEB_API_BASE_URL + "follower/getFollowers/" + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getFollowViewModelsFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<FollowViewModel>);
            }
        }
        
        public async Task<List<FollowViewModel>> getAllFollowingAsync(int userId)     //receiving a list of Follow view model from webservice 
        {
            string url = WEB_API_BASE_URL + "follower/getFollowing/" + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getFollowViewModelsFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<FollowViewModel>);
            }
        }

        public async Task<Cloth> createCloth(Cloth cloth) {                //receiving a cloth from webservice 
            string url = WEB_API_BASE_URL + "cloth/createNew/";
            var json = JsonConvert.SerializeObject(cloth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getClothFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(Cloth);
            }
        }

        public async Task<Post> createPost(Post post)      //receiving a post from webservice 
        {
            string url = WEB_API_BASE_URL + "post/createNew/";
            var json = JsonConvert.SerializeObject(post);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getPostFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(Post);
            }
        }

        public async Task<List<FeedViewModel>> getFeeds(int userId) {  //receiving a list feed view model from webservice 
            string url = WEB_API_BASE_URL + "post/getFeeds/" + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getFeedViewModelsFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<FeedViewModel>);
            }
        }

        public async Task<List<OutfitViewModel>> getOutfits(int userId) { //receiving a list of outfit view model from webservice 
            string url = WEB_API_BASE_URL + "outfit/getOutfits/" + userId;
            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Response responseObject = await getHTTPResponse(response);
                return getOutfitViewModelsFromResponse(responseObject);
            }
            else
            {
                Debug.WriteLine("Error Occured!");
                return default(List<OutfitViewModel>);
            }
        }

    }
}
