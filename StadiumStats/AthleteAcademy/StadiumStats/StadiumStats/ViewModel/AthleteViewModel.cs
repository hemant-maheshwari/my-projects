
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace StadiumStats.ViewModel
{
    public class AthleteViewModel
    {
        public string firstName {get; set;}

        public string lastName { get; set; }

        public string athletePicture { get; set; }


        public ImageSource athleteImage
        {
            get
            {
                return getAthleteImage();
            }
        }

        public ImageSource getAthleteImage()
        {
            byte[] Base64Stream = Convert.FromBase64String(athletePicture);
            var src = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
            return src;
        }


        public AthleteViewModel(string firstName, string lastName, string athletePicture)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.athletePicture = athletePicture;
        }
    }
}
