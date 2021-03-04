using StadiumStats.Model;
using StadiumStats.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StadiumStats.Controller
{
    class UserController : BaseController<User>
    {
        private RestAPIService restAPIService;

        public UserController() {
            restAPIService = new RestAPIService();
        }

        public async Task<User> checkUser(User user)
        {
            return await restAPIService.checkUser(user);
        }
        public async Task<User> getUserFromUsername(string username)
        {
            return await restAPIService.getUserFromUsernameAsync(username);
        }

    }
}
