using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketClosetWebServiceAPI.Services;
using StadiumStatsWebAPI.DataStructure;
using StadiumStatsWebAPI.Model;
using StadiumStatsWebAPI.Service;

namespace StadiumStatsWebAPI.controller
{
    [Route("v1/api/[controller]")]
    public class AthleteController : Controller, IAthlete
    {

        private readonly IConfiguration config;
        private CRUDService crudService;
        private DataHandlerService dataHandlerService;
        public AthleteController(IConfiguration config)
        {
            this.config = config;
            crudService = new CRUDService(config.GetConnectionString("DefaultConnection"));
            dataHandlerService = new DataHandlerService(config.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        [Route("create")]
        public JsonResult create([FromBody] Athlete athlete)
        {
            Response response = new Response();
            int athleteId = crudService.create<Athlete>(athlete);
            if (athleteId != 0)
            {
                response.status = true;
            }
            else
            {
                response.status = false;
            }
            return Json(response);
        }

        [HttpGet]
        [Route("getAthletes/{userId}")]
        public JsonResult getAthletes(int userId)
        {
            Response response = new Response();
            List<Athlete> athletes = dataHandlerService.getAthletes(userId);
            response.data = JsonConvert.SerializeObject(athletes);
            response.status = true;
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}