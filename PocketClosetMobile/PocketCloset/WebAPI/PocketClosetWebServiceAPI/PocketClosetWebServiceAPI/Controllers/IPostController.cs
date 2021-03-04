using Microsoft.AspNetCore.Mvc;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Controllers
{
    public interface IPostController
    {
        JsonResult createPost(Post post);
        JsonResult getPost(int postId);
        JsonResult getAllPosts(int userId);
        JsonResult createNewPost(Post post);
        JsonResult getFeeds(int userId);

    }
}
