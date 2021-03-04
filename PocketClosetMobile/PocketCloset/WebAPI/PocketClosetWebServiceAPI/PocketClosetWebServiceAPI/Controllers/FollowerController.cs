using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketCloset.Models;
using PocketClosetWebServiceAPI.Handlers;
using PocketClosetWebServiceAPI.Models;
using PocketClosetWebServiceAPI.ViewModels;

namespace PocketClosetWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class FollowerController : Controller, IFollowerController
    {
        private readonly IConfiguration config;

        public FollowerController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createFollower([FromBody] Follower follower)
        {
            Response response = new Response();
            FollowerDataHandler followerDataHandler = new FollowerDataHandler(config);
            followerDataHandler.followedUserId = follower.followedUserId;
            followerDataHandler.followerUserId = follower.followerUserId;
            response.status = followerDataHandler.createFollower();
            return Json(response);
        }

        [Route("delete/{followId}")]
        [HttpGet]
        public JsonResult deleteFollower(int followId)
        {
            Response response = new Response();
            FollowerDataHandler followerDataHandler = new FollowerDataHandler(config);
            response.status = followerDataHandler.deleteFollower(followId);
            return Json(response);
        }

        [Route("getFollowers/{userId}")]
        [HttpGet]
        public JsonResult getFollowers(int userId)
        {
            Response response = new Response();
            FollowerDataHandler followerDataHandler = new FollowerDataHandler(config);
            try {
                List<FollowViewModel> followViewModels = followerDataHandler.getFollowers(userId);
                response.data = JsonConvert.SerializeObject(followViewModels);
                response.status = true;
            } catch (Exception ex) {
                response.status = false;
                Debug.WriteLine(ex.Message);
            }            
            return Json(response);
        }

        [Route("getFollowing/{userId}")]
        [HttpGet]
        public JsonResult getFollowing(int userId)
        {
            Response response = new Response();
            FollowerDataHandler followerDataHandler = new FollowerDataHandler(config);
            try
            {
                List<FollowViewModel> followViewModels = followerDataHandler.getFollowing(userId);
                response.data = JsonConvert.SerializeObject(followViewModels);
                response.status = true;
            }
            catch (Exception ex)
            {
                response.status = false;
                Debug.WriteLine(ex.Message);
            }
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}