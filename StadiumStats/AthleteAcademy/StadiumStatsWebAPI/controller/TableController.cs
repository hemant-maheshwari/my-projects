using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PocketClosetWebServiceAPI.Services;
using StadiumStats.Model;
using StadiumStatsWebAPI.DataStructure;
using StadiumStatsWebAPI.Model;

namespace StadiumStatsWebAPI.controller
{
    [Route("v1/api/[controller]")]
    public class TableController : Controller
    {
        private TableServices tableServices;
       
        private readonly IConfiguration config;
        public TableController(IConfiguration config)
        {
            this.config = config;
            tableServices = new TableServices(config.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("create")]
        public JsonResult create() {
            Response response = new Response();
            
            response.status = true;
            return Json(response);
        }

        [HttpGet]
        [Route("createTable")]
        public JsonResult createTableInDB()
        {
            Response response = new Response();
            //tableServices.create<User>();
            //tableServices.create<Athlete>();
            tableServices.create<Stats>();
            response.status = true;
            return Json(response);
        }
    }
}