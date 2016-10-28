using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Message
{
    public interface IMessageParser
    {
        ChallengeMessage GetMessage(MessageType type);

        ChallengeMessage GetMessage(MessageType type, string pathToFile);
    }
}
