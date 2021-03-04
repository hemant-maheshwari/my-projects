using PocketCloset.Models;
using PocketClosetWebServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public interface IOutfitDataHandler
    {
        bool createOutfit();
        bool updateOutfit();
        bool deleteOutfit(int outfitId);
        Outfit getOutfit(int outfitId);
        List<Outfit> getAllOutfits(int userId);

        List<OutfitViewModel> getOutfits(int userId);
    }
}
