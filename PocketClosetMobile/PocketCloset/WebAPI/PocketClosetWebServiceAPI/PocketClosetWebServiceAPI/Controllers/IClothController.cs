using Microsoft.AspNetCore.Mvc;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Controllers
{
    public interface IClothController
    {
        JsonResult createCloth(Cloth cloth);
        JsonResult updateCloth(Cloth cloth);
        JsonResult getCloth(int clothId);
        JsonResult getAllClothes(int userId);
        JsonResult createNewCloth(Cloth cloth);
    }
}
