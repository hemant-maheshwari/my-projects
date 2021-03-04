using System;
using PocketCloset.Models;
using PocketCloset.Service;

namespace PocketCloset.Controller
{
    public class SecQuestionController : BaseController<SecQuestion>
    {
        private RestAPIService restAPIService;

        public SecQuestionController()
        {
            restAPIService = new RestAPIService();
        }
    }
}
