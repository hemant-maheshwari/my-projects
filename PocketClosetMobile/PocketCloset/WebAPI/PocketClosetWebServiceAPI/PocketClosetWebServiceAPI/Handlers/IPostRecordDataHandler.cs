using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public interface IPostRecordDataHandler
    {
        bool createPostRecord();
        bool deletePostRecord(int postRecordId);
        List<PostRecord> getAllPostRecords(int userId);
    }
}
