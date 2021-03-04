using PocketCloset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocketClosetWebServiceAPI.Handlers
{
    public interface ISecQuestionDataHandler
    {
        bool createSecQuestion();
        SecQuestion getSecQuestion(int userId);
    }
}
