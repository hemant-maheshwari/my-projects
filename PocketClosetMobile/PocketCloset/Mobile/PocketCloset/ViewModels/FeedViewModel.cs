using Plugin.Media.Abstractions;
using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PocketCloset.ViewModels
{
    public class FeedViewModel
    {
        private string format = "ddd, dd MMM, yyyy";
        
        public string userProfiePicture { get; set; }
        public string username { get; set; }
        public string clothPicture { get; set; }
        public string datePosted { get; set; }

        public string fancyDate
        {
            get {
                return DateTime.ParseExact(datePosted, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString(format);
            }
        }

        public ImageSource postImage {
            get { return getPostImage(); }            
        }

        public ImageSource profileImage
        {
            get { return getProfileImage(); }
        }

        public ImageSource getPostImage()
        {
            byte[] Base64Stream = Convert.FromBase64String(clothPicture);
            var src = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            return src;
        }
        public ImageSource getProfileImage()
        {            
            byte[] Base64Stream = Convert.FromBase64String(userProfiePicture);
            var src = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            return src;            
        }

    }
}
