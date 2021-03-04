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

namespace PocketClosetWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class ProfilePictureController : Controller, IProfilePictureController
    {
        private readonly IConfiguration config;
        public ProfilePictureController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            return saveProfilePicture(profilePicture, "create");
        }

        [Route("delete/{userId}")]
        [HttpGet]
        public JsonResult deleteProfilePicture(int userId)
        {
            Response response = new Response();
            ProfilePictureDataHandler profilePictureDataHandler = new ProfilePictureDataHandler(config);
            response.status = profilePictureDataHandler.deleteProfilePicture(userId);
            return Json(response);
        }

        [Route("get/{userId}")]
        [HttpGet]
        public JsonResult getProfilePicture(int userId)
        {
            Response response = new Response();
            ProfilePictureDataHandler profilePictureDataHandler = new ProfilePictureDataHandler(config);
            try
            {
                ProfilePicture profilePicture = profilePictureDataHandler.getProfilePicture(userId);
                response.data = JsonConvert.SerializeObject(profilePicture);
                response.status = true;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            return saveProfilePicture(profilePicture, "update");
        }

        private JsonResult saveProfilePicture(ProfilePicture profilePicture, string command) {
            Response response = new Response();
            ProfilePictureDataHandler profilePictureDataHandler = new ProfilePictureDataHandler(config);
            profilePictureDataHandler.userId = profilePicture.userId;
            profilePictureDataHandler.profilePicture = profilePicture.profilePicture;
            if (command.Equals("create"))
            {
                response.status = profilePictureDataHandler.createProfilePicture();
            }
            if (command.Equals("update"))
            {
                response.status = profilePictureDataHandler.updateProfilePicture();
            }
            return Json(response);
        }
    }
}