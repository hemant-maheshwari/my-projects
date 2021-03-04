using PocketCloset.Models;
using PocketCloset.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCloset.Controller
{
    public class PostRecordController: BaseController<PostRecord>
    {
        private RestAPIService restAPIService;

        public PostRecordController()
        {
            restAPIService = new RestAPIService();
        }
    }
}
