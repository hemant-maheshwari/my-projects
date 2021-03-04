using System;
namespace PocketCloset.Models
{
    public class Cloth
    {
        public Cloth()
        {
        }

        public Cloth(int userId, string clothType, string clothPicture, string color, string material, string season) {
            this.userId = userId;
            this.clothType = clothType;
            this.clothPicture = clothPicture;
            this.color = color;
            this.material = material;
            this.season = season;
        }

        public Cloth(int userId, string clothPicture) {
            this.userId = userId;
            this.clothPicture = clothPicture;
        }

        public int clothId { get; set; }
        public int userId { get; set; }
        public string clothType { get; set; }
        public string clothPicture { get; set; }
        public string color { get; set; }
        public string material { get; set; }
        public string season { get; set; }
            


    }
}
