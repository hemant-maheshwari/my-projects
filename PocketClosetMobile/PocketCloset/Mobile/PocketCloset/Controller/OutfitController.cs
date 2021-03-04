using PocketCloset.Models;
using PocketCloset.Service;
using PocketCloset.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PocketCloset.Controller
{
    class OutfitController: BaseController<Outfit>
    {
        private RestAPIService restAPIService;

        public OutfitController() {
            restAPIService = new RestAPIService();
        }

        public async Task<List<OutfitViewModel>> getOutfits(int userId) { //receiving a list of outfitViewModel
            return await restAPIService.getOutfits(userId);
        }
    }
}
