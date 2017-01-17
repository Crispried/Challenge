using Challange.Domain.Services.FileSystem.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Challange.Domain.Services.Message
{
    public class MessageParser : IMessageParser
    {
        private IFileWorker _fileWorker;
        private string defaultPathToFile = @"Message/message_info.xml";

        public MessageParser(IFileWorker fileWorker)
        {
            _fileWorker = fileWorker;
        }

        public ChallengeMessage GetMessage(MessageType type)
        {
            return GetMessage(type, defaultPathToFile);
        }

        public ChallengeMessage GetMessage(MessageType type, string pathToFile)
        {
            List<ChallengeMessage> messages =
                _fileWorker.DeserializeXml<List<ChallengeMessage>>(pathToFile);
            ChallengeMessage appropriateMessage =
                messages.Where(m => m.MessageType == type).FirstOrDefault();
            return appropriateMessage;
        }
    }
}
