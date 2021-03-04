using System;
using System.Collections.Generic;

namespace PocketCloset.Models
{
    public class Outfit
    {
        public Outfit()
        {
        }

        public Outfit(int userId, string outfitName, int clothId)
        {
            this.clothId = clothId;
            this.outfitName = outfitName;
            this.userId = userId;
        }

        public int outfitId { get; set; }
        public int userId { get; set; }
        public string outfitName { get; set; }
        public int clothId { get; set; } //list of cloth ids



    }
}
