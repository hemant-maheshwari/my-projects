using System;
namespace PocketCloset.Models
{
    public class Follower
    {
        public Follower()
        {
        }

        public Follower(int followerUserId, int followedUserId)
        {
            this.followedUserId = followedUserId;
            this.followerUserId = followerUserId;
        }

        public int followId { get; set; }
        public int followerUserId { get; set; } //Id of person who is following 
        public int followedUserId { get; set; } //id of person who is being followed
    
    }
}
