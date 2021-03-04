using System;
using System.Collections.Generic;
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
    public class PostController : Controller, IPostController
    {
        private readonly IConfiguration config;
        public PostController(IConfiguration config) {
            this.config = config;
        }

        [Route("createNew")]
        [HttpPost]
        public JsonResult createNewPost([FromBody] Post post)
        {
            Response response = new Response();
            PostDataHandler postDataHandler = new PostDataHandler(config);
            postDataHandler.clothId = post.clothId;
            postDataHandler.userId = post.userId;
            postDataHandler.isModelPresent = post.isModelPresent;
            postDataHandler.postId = post.postId;
            postDataHandler.price = post.price;
            postDataHandler.url = post.url;
            post = postDataHandler.createNewPost();
            response.data = JsonConvert.SerializeObject(post);
            return Json(response);
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createPost([FromBody] Post post)
        {
            Response response = new Response();
            PostDataHandler postDataHandler = new PostDataHandler(config);
            postDataHandler.clothId = post.clothId;
            postDataHandler.userId = post.userId;
            postDataHandler.isModelPresent = post.isModelPresent;
            postDataHandler.postId = post.postId;
            postDataHandler.price = post.price;
            postDataHandler.url = post.url;
            response.status = postDataHandler.createPost();
            return Json(response);
        }

        [Route("getAll/{userId}")]
        [HttpGet]
        public JsonResult getAllPosts(int userId)
        {
            return findPosts(userId, "getAll");
        }

        [Route("getFeeds/{userId}")]
        [HttpGet]
        public JsonResult getFeeds(int userId)
        {
            Response response = new Response();
            PostDataHandler postDataHandler = new PostDataHandler(config);
            try
            {
                List<FeedViewModel> feeds = postDataHandler.getAllFeeds(userId);
                response.data = JsonConvert.SerializeObject(feeds);                
                response.status = true;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        [Route("get/{postId}")]
        [HttpGet]
        public JsonResult getPost(int postId)
        {
            return findPosts(postId, "get");
        }

        public IActionResult Index()
        {
            return View();
        }

        private JsonResult findPosts(int searchId, string command) {
            Response response = new Response();
            PostDataHandler postDataHandler = new PostDataHandler(config);
            try {
                if (command.Equals("get")) {
                    Post post = postDataHandler.getPost(searchId);
                    response.data = JsonConvert.SerializeObject(post);
                }
                if (command.Equals("getAll")) {
                    List<Post> posts = postDataHandler.getAllPosts(searchId);
                    response.data = JsonConvert.SerializeObject(posts);
                }
                response.status = true;
            } catch (Exception ex) {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }
    }
}