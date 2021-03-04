using PocketCloset.Models;
using PocketClosetWebServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public interface IPostDataHandler
    {
        bool createPost();
        Post getPost(int postId);
        List<Post> getAllPosts(int userId);
        Post createNewPost();

        List<FeedViewModel> getAllFeeds(int userId);
    }
}
