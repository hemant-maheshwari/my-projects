using System;
namespace PocketCloset.Models
{
    public class PostRecord
    {
        public PostRecord()
        {
        }

        public int postRecordId { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }
        public string datePosted { get; set; }

    }
}
