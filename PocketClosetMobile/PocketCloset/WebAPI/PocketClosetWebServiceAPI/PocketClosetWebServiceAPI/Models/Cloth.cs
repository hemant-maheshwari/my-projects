using System;
namespace PocketCloset.Models
{
    public class Cloth
    {
        public Cloth()
        {
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
