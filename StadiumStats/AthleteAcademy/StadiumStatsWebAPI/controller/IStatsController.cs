using Microsoft.AspNetCore.Mvc;
using StadiumStatsWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumStatsWebAPI.controller
{
    public interface IStatsController
    {
        JsonResult create(Stats stats);
        JsonResult getAllStats(int athleteId);

    }
}
