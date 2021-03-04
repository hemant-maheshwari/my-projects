using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
   public interface IProfilePictureDataHandler
    {
        bool createProfilePicture();
        bool updateProfilePicture();
        bool deleteProfilePicture(int userId);
        ProfilePicture getProfilePicture(int userId);
    }
}
