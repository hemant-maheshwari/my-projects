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
    public class ClothController : Controller, IClothController
    {
        private readonly IConfiguration config;
        public ClothController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createCloth([FromBody] Cloth cloth)
        {
            return saveCloth(cloth, "create");
        }

        [Route("createNew")]
        [HttpPost]
        public JsonResult createNewCloth([FromBody] Cloth cloth)
        {
            Response response = new Response();
            ClothDataHandler clothDataHandler = new ClothDataHandler(config);
            clothDataHandler.userId = cloth.userId;
            clothDataHandler.clothType = cloth.clothType;
            clothDataHandler.clothPicture = cloth.clothPicture;
            clothDataHandler.color = cloth.color;
            clothDataHandler.material = cloth.material;
            clothDataHandler.season = cloth.season;
            cloth = clothDataHandler.createNewCloth();
            response.data = JsonConvert.SerializeObject(cloth);
            return Json(response);
        }

        [Route("getAll/{userId}")]
        [HttpGet]
        public JsonResult getAllClothes(int userId)
        {
            Response response = new Response();
            ClothDataHandler clothDataHandler = new ClothDataHandler(config);
            try
            {
                List<Cloth> clothes = clothDataHandler.getAllClothes(userId);
                response.data = JsonConvert.SerializeObject(clothes);
                response.status = true;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        [Route("get/{clothId}")]
        [HttpGet]
        public JsonResult getCloth(int clothId)
        {
            Response response = new Response();
            ClothDataHandler clothDataHandler = new ClothDataHandler(config);
            try {
                Cloth cloth = clothDataHandler.getCloth(clothId);
                response.data = JsonConvert.SerializeObject(cloth);
                response.status = true;
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

        [Route("update")]
        [HttpPost]
        public JsonResult updateCloth([FromBody] Cloth cloth)
        {
            return saveCloth(cloth, "update");
        }

        private JsonResult saveCloth(Cloth cloth, string command) {
            Response response = new Response();
            ClothDataHandler clothDataHandler = new ClothDataHandler(config);
            clothDataHandler.clothId = cloth.clothId;
            clothDataHandler.userId = cloth.userId;
            clothDataHandler.clothType = cloth.clothType;
            clothDataHandler.clothPicture = cloth.clothPicture;
            clothDataHandler.color = cloth.color;
            clothDataHandler.material = cloth.material;
            clothDataHandler.season = cloth.season;
            if (command.Equals("create")) {
                response.status = clothDataHandler.createCloth();
            }
            if (command.Equals("update")) {
                response.status = clothDataHandler.updateCloth();
            }
            return Json(response);
        }
    }
}