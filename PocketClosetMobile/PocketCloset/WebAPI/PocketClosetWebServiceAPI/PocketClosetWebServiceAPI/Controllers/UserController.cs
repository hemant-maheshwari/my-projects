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
    public class UserController : Controller, IUserController
    {
        private readonly IConfiguration config;

        public UserController(IConfiguration config) {
            this.config = config;
        }

        [Route("check/{username}")]
        [HttpGet]
        public JsonResult checkUsername(string username)
        {
            bool result = false;
            UserDataHandler userDataHandler = new UserDataHandler(config);
            result = userDataHandler.checkUsername(username);
            Response response = new Response();
            response.status = result;
            return Json(response);
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createUser([FromBody] User user)
        {
            return saveuser(user, "create");
        }

        [Route("get/{userId}")]
        [HttpGet]
        public JsonResult getUser(int userId)
        {
            Response response = new Response();
            UserDataHandler userDataHandler = new UserDataHandler(config);
            try {
                User user = userDataHandler.getUser(userId);
                response.status = true;
                response.data = JsonConvert.SerializeObject(user);
            } catch (Exception ex) {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public JsonResult login([FromBody] User user)
        {
            Response response = new Response();
            try
            {
                UserDataHandler userDataHandler = new UserDataHandler(config);
                userDataHandler.username = user.username;
                userDataHandler.password = user.password;
                User completeUser = userDataHandler.findUser();
                response.status = true;
                response.data = JsonConvert.SerializeObject(completeUser);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return Json(response);
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateUser([FromBody] User user)
        {
            return saveuser(user, "update");
        }

        [Route("validateUsername/{username}")]
        [HttpGet]
        public JsonResult validateUsername(string username)
        {
            Response response = new Response();
            try
            {
                UserDataHandler userDataHandler = new UserDataHandler(config);
                User user = userDataHandler.validateUser(username);
                response.status = true;
                response.data = JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return Json(response);
        }

        private JsonResult saveuser(User user, string command) {
            Response response = new Response();
            UserDataHandler userDataHandler = new UserDataHandler(config);
            userDataHandler.userId = user.userId;
            userDataHandler.username = user.username;
            userDataHandler.password = user.password;
            userDataHandler.firstName = user.firstName;
            userDataHandler.lastName = user.lastName;
            userDataHandler.email = user.email;
            userDataHandler.userType = user.userType;
            userDataHandler.gender = user.gender;
            userDataHandler.dob = user.dob;
            if (command.Equals("create")) {
                response.status = userDataHandler.createUser();
            }
            if (command.Equals("update")) {
                response.status = userDataHandler.updateUser();
            }
            return Json(response);
        }
    }
}