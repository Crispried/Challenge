using Challange.Domain.Services.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Message
{
    public class MessageParser : IMessageParser
    {
        private string defaultPathToFile = @"Message/message_info.xml";

        public ChallengeMessage GetMessage(MessageType type)
        {
            return GetMessage(type, defaultPathToFile);
        }

        public ChallengeMessage GetMessage(MessageType type, string pathToFile)
        {
            FileWorker fileWorker = new FileWorker();
            List<ChallengeMessage> messages =
                fileWorker.DeserializeXml<List<ChallengeMessage>>(pathToFile);
            ChallengeMessage appropriateMessage =
                messages.Where(m => m.MessageType == type).FirstOrDefault();
            return appropriateMessage;
        }
    }
}
