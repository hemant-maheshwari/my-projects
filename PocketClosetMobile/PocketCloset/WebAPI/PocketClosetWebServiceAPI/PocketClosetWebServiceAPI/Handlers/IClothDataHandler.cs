using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public interface IClothDataHandler
    {
        bool createCloth();
        bool updateCloth();
        Cloth getCloth(int clothId);
        List<Cloth> getAllClothes(int userId);
        Cloth createNewCloth();
    }
}
