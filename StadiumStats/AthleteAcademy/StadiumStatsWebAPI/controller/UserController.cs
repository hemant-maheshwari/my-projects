using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketClosetWebServiceAPI.Services;
using StadiumStats.Model;
using StadiumStatsWebAPI.DataStructure;
using StadiumStatsWebAPI.Service;

namespace StadiumStatsWebAPI.controller
{
    [Route("v1/api/[controller]")]
    public class UserController : Controller, IUserController
    {
        private readonly IConfiguration config;
        private CRUDService crudService;
        private DataHandlerService dataHandlerService;
        public UserController(IConfiguration config)
        {
            this.config = config;
            crudService = new CRUDService(config.GetConnectionString("DefaultConnection"));
            dataHandlerService = new DataHandlerService(config.GetConnectionString("DefaultConnection"));
        }


        [HttpPost]
        [Route("check")]
        public JsonResult check([FromBody]User user)
        {
            Response response = new Response();
            response.status = true;
            User finalUser = dataHandlerService.checkUser(user);
            response.data = JsonConvert.SerializeObject(finalUser);
            return Json(response);
        }

        [HttpPost]
        [Route("create")]
        public JsonResult create([FromBody]User user)
        {
            Response response = new Response();
            int userId = crudService.create<User>(user);
            if (userId != 0)
            {
                response.status = true;
            }
            else
            {
                response.status = false;
            }
            return Json(response);
        }
        public IActionResult Index() 
        {
            return View();
        }

        [HttpPost]
        [Route("update")]
        public JsonResult update([FromBody] User user)
        {
            Response response = new Response();
            crudService.update<User>(user);
            response.status = true;
            return Json(response);
        }

        
        [Route("validateUsername/{username}")]
        [HttpGet]
        public JsonResult validateUsername(string username)
        {
            Response response = new Response();
            User finalUser = dataHandlerService.validateUser(username);
            response.data = JsonConvert.SerializeObject(finalUser);
            response.status = true;
            return Json(response);
        }
    }
}