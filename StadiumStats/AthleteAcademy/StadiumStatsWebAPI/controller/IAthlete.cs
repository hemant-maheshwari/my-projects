using Microsoft.AspNetCore.Mvc;
using StadiumStatsWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadiumStatsWebAPI.controller
{
    public interface IAthlete
    {
        JsonResult create(Athlete athlete);

        JsonResult getAthletes(int userId);
    }
}
