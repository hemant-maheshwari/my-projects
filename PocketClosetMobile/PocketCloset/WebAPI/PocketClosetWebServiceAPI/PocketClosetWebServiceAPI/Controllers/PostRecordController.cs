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
    public class PostRecordController : Controller, IPostRecordController
    {
        private readonly IConfiguration config;
        public PostRecordController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createPostRecord([FromBody] PostRecord postRecord)
        {
            Response response = new Response();
            PostRecordDataHandler postRecordDataHandler = new PostRecordDataHandler(config);
            postRecordDataHandler.postId = postRecord.postId;
            postRecordDataHandler.postRecordId = postRecord.postRecordId;
            postRecordDataHandler.userId = postRecord.userId;
            postRecordDataHandler.datePosted = postRecord.datePosted;
            response.status = postRecordDataHandler.createPostRecord();
            return Json(response);
        }

        [Route("delete/{postRecordId}")]
        [HttpGet]
        public JsonResult deletePostRecord(int postRecordId)
        {
            Response response = new Response();
            PostRecordDataHandler postRecordDataHandler = new PostRecordDataHandler(config);
            response.status = postRecordDataHandler.deletePostRecord(postRecordId);
            return Json(response);
        }

        [Route("getAll/{userId}")]
        [HttpGet]
        public JsonResult getAllPostRecords(int userId)
        {
            Response response = new Response();
            PostRecordDataHandler postRecordDataHandler = new PostRecordDataHandler(config);
            try {
                List<PostRecord> postRecords = postRecordDataHandler.getAllPostRecords(userId);
                response.data = JsonConvert.SerializeObject(postRecords);
                response.status = true;
            }
            catch (Exception ex) {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}