using System;
namespace PocketCloset.Models
{
    public class PostRecord
    {
        public PostRecord()
        {
        }

        public PostRecord(int userId, int postId, string datePosted) {
            this.userId = userId;
            this.postId = postId;
            this.datePosted = datePosted;
        }

        public int postRecordId { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }
        public string datePosted { get; set; }

    }
}
