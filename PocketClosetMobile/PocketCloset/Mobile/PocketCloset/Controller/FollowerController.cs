using PocketCloset.Models;
using PocketCloset.Service;
using PocketCloset.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PocketCloset.Controller 
{
    public class FollowerController : BaseController<Follower>
    {
        public RestAPIService restAPIService;


        public FollowerController()
        {
            restAPIService = new RestAPIService();
        }
        public async Task<List<FollowViewModel>> getAllFollowers(int userId) //receiving a list of FollowViewModel
        {
            return await restAPIService.getAllFollowersAsync(userId);
           
        }
        public async Task<List<FollowViewModel>> getAllFollowing(int userId) //receiving a list of FollowViewModel
        {
            return await restAPIService.getAllFollowingAsync(userId);
           
        }

    }
}

