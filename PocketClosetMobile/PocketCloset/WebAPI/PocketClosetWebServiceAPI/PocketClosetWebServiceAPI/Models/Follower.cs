using System;
namespace PocketCloset.Models
{
    public class Follower
    {
        public Follower()
        {
        }

        public int followId { get; set; }
        public int followerUserId { get; set; }
        public int followedUserId { get; set; }

    }
}
