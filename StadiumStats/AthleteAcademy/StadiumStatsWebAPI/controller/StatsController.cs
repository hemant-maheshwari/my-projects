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
    public class StatsController : Controller, IStatsController
    {

        private readonly IConfiguration config;
        private CRUDService crudService;
        private DataHandlerService dataHandlerService;
        public StatsController(IConfiguration config)
        {
            this.config = config;
            crudService = new CRUDService(config.GetConnectionString("DefaultConnection"));
            dataHandlerService = new DataHandlerService(config.GetConnectionString("DefaultConnection"));
        }

        [HttpPost]
        [Route("create")]
        public JsonResult create([FromBody]Stats stats)
        {
            Response response = new Response();
            int statsId = crudService.create<Stats>(stats);
            if (statsId != 0)
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
        [Route("getAllStats/{athleteId}")]
        public JsonResult getAllStats(int athleteId)
        {
            Response response = new Response();
            List<Stats> athleteStats = dataHandlerService.getAllStats(athleteId);
            response.data = JsonConvert.SerializeObject(athleteStats);
            response.status = true;
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}