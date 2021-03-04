using System;
namespace PocketCloset.Models
{
    public class Post
    {
        public Post()
        {
        }

        public Post(int userId, int clothId, double price, string url, bool isModelPresent) {
            this.userId = userId;
            this.clothId = clothId;
            this.price = price;
            this.url = url;
            this.isModelPresent = isModelPresent;
        }

        public int userId { get; set; }
        public int postId { get; set; }
        public int clothId { get; set; }
        public double price { get; set; }
        public string url { get; set; } //url to buy clothing article
        public bool isModelPresent { get; set; } //is model present in picture
      

    }
}
