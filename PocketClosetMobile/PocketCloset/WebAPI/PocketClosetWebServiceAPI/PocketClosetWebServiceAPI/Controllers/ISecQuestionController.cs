using Microsoft.AspNetCore.Mvc;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Controllers
{
    public interface ISecQuestionController
    {
        JsonResult createSecQuestion(SecQuestion secQuestion);
        JsonResult getSecQuestion(int userId);
    }
}
