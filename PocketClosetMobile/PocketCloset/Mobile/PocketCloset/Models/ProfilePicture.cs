using System;
namespace PocketCloset.Models
{
    public class ProfilePicture
    {
        public ProfilePicture()
        {
        }

        public ProfilePicture(int userId, string proflePictureString)
        {
            this.userId = userId;
            this.profilePicture = proflePictureString;
        }

        public int userId { get; set; }
        public string profilePicture { get; set; } //profile picture
   

    }
}
