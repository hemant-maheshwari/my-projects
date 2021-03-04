using Microsoft.AspNetCore.Mvc;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Controllers
{
    public interface IOutfitController
    {
        JsonResult createOutfit(Outfit outfit);
        JsonResult updateOutfit(Outfit outfit);
        JsonResult deleteOutfit(int outfitId);
        JsonResult getOutfit(int outfitId);
        JsonResult getAllOutfits(int userId);

        JsonResult getOutfits(int userId);
    }
}
