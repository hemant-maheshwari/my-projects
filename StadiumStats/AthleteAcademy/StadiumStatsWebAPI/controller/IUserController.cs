using Microsoft.AspNetCore.Mvc;
using StadiumStats.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumStatsWebAPI.controller
{
    public interface IUserController
    {
        JsonResult create(User user);
        JsonResult check(User user);
        JsonResult update(User user);
        JsonResult validateUsername(string username); 
    }
}
