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
    public class SecQuestionController : Controller, ISecQuestionController
    {
        private readonly IConfiguration config;
        public SecQuestionController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createSecQuestion([FromBody] SecQuestion secQuestion)
        {
            return saveSecQuestion(secQuestion, "create");
        }

        [Route("get/{userId}")]
        [HttpGet]
        public JsonResult getSecQuestion(int userId)
        {
            Response response = new Response();
            SecQuestionDataHandler secQuestionDataHandler = new SecQuestionDataHandler(config);
            try
            {
                SecQuestion secQuestion = secQuestionDataHandler.getSecQuestion(userId);
                response.data = JsonConvert.SerializeObject(secQuestion);
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

        private JsonResult saveSecQuestion(SecQuestion secQuestion, string command)
        {
            Response response = new Response();
            SecQuestionDataHandler secQuestionDataHandler = new SecQuestionDataHandler(config);
            secQuestionDataHandler.queId = secQuestion.queId;
            secQuestionDataHandler.userId = secQuestion.userId;
            secQuestionDataHandler.question = secQuestion.question;
            secQuestionDataHandler.answer = secQuestion.answer;
            if (command.Equals("create"))
            {
                response.status = secQuestionDataHandler.createSecQuestion();
            }            
            return Json(response);
        }
    }
}