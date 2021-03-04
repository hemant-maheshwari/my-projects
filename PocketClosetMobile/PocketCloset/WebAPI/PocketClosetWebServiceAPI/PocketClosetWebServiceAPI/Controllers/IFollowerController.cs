using Microsoft.AspNetCore.Mvc;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Controllers
{
    public interface IFollowerController
    {
        JsonResult createFollower(Follower follower);
        JsonResult deleteFollower(int followId);
        JsonResult getFollowing(int userId);
        JsonResult getFollowers(int userId);
    }
}
