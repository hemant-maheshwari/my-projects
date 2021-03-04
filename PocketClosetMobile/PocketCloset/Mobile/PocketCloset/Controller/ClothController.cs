using PocketCloset.Models;
using PocketCloset.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PocketCloset.Controller
{
    class ClothController: BaseController<Cloth>
    {
        private RestAPIService restAPIService;

        public ClothController()
        {
            restAPIService = new RestAPIService();
        }

        public async Task<Cloth> createCloth(Cloth cloth) { //receiving a cloth from rest api
            return await restAPIService.createCloth(cloth);
        }

    }
}
