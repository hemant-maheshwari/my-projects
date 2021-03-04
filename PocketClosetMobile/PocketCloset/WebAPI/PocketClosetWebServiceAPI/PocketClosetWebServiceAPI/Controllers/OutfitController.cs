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
    public class OutfitController : Controller, IOutfitController
    {
        private readonly IConfiguration config;
        public OutfitController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createOutfit([FromBody] Outfit outfit)
        {
            return saveOutfit(outfit, "create");
        }

        [Route("delete/{outfitId}")]
        [HttpGet]
        public JsonResult deleteOutfit(int outfitId)
        {
            Response response = new Response();
            OutfitDataHandler outfitDataHandler = new OutfitDataHandler(config);
            response.status = outfitDataHandler.deleteOutfit(outfitId);
            return Json(response);
        }

        [Route("getAll/{userId}")]
        [HttpGet]
        public JsonResult getAllOutfits(int userId)
        {
            return findOutfits(userId, "getAll");
        }

        [Route("get/{outfitId}")]
        [HttpGet]
        public JsonResult getOutfit(int outfitId)
        {
            return findOutfits(outfitId, "get");
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateOutfit([FromBody] Outfit outfit)
        {
            return saveOutfit(outfit, "update");
        }

        private JsonResult saveOutfit(Outfit outfit, string command) {
            Response response = new Response();
            OutfitDataHandler outfitDataHandler = new OutfitDataHandler(config);
            outfitDataHandler.outfitId = outfit.outfitId;
            outfitDataHandler.outfitName = outfit.outfitName;
            outfitDataHandler.clothId = outfit.clothId;
            outfitDataHandler.userId = outfit.userId;
            if (command.Equals("create")) {
                response.status = outfitDataHandler.createOutfit();
            }
            if (command.Equals("update")) {
                response.status = outfitDataHandler.updateOutfit();
            }
            return Json(response);            
        }

        private JsonResult findOutfits(int searchId, string command) {
            Response response = new Response();
            try {
                OutfitDataHandler outfitDataHandler = new OutfitDataHandler(config);
                response.status = true;
                if (command.Equals("get"))
                {
                    Outfit outfit = outfitDataHandler.getOutfit(searchId);
                    response.data = JsonConvert.SerializeObject(outfit);
                }
                if (command.Equals("getAll"))
                {
                    List<Outfit> outfits = outfitDataHandler.getAllOutfits(searchId);
                    response.data = JsonConvert.SerializeObject(outfits);
                }
            } catch (Exception ex) {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);            
        }

        [Route("getOutfits/{userId}")]
        [HttpGet]
        public JsonResult getOutfits(int userId)
        {
            Response response = new Response();
            try
            {
                OutfitDataHandler outfitDataHandler = new OutfitDataHandler(config);
                response.status = true;
                List<OutfitViewModel> outfits = outfitDataHandler.getOutfits(userId);
                response.data = JsonConvert.SerializeObject(outfits);                
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }
    }
}