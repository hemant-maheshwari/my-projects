using PocketCloset.Models;
using PocketCloset.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketCloset.Controller
{
    public class ProfilePictureController: BaseController<ProfilePicture>
    {
        private RestAPIService restAPIService;

        public ProfilePictureController() {
            restAPIService = new RestAPIService();
        }        

    }
}
