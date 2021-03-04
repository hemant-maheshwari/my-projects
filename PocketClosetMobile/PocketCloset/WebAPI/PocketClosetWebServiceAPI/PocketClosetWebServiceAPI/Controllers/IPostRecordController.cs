using Microsoft.AspNetCore.Mvc;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Controllers
{
    public interface IPostRecordController
    {
        JsonResult createPostRecord(PostRecord postRecord);
        JsonResult deletePostRecord(int postRecordId);
        JsonResult getAllPostRecords(int userId);
    }
}
