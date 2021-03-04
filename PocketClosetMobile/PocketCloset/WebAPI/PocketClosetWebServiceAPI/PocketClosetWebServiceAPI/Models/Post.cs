using System;
namespace PocketCloset.Models
{
    public class Post
    {
        public Post()
        {
        }

        public int userId { get; set; }
        public int postId { get; set; }
        public int clothId { get; set; }
        public double price { get; set; }
        public string url { get; set; }
        public bool isModelPresent { get; set; }

    }
}
