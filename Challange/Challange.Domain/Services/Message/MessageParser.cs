using Challange.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Message
{
    public static class MessageParser
    {
        private static string defaultPathToFile = @"Message/message_info.xml";

        public static ChallengeMessage GetMessage(MessageType type)
        {
            return GetMessage(type, defaultPathToFile);
        }

        public static ChallengeMessage GetMessage(MessageType type, string pathToFile)
        {
            List<ChallengeMessage> messages =
                FileWorker.DeserializeXml<List<ChallengeMessage>>(pathToFile);
            ChallengeMessage appropriateMessage =
                messages.Where(m => m.MessageType == type).FirstOrDefault();
            return appropriateMessage;
        }
    }
}
