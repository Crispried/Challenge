using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.Message;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Services.Message
{
    [TestFixture]
    class MessageParserTest : TestCase
    {
        private IXmlWorker _fileWorker;
        private MessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            _fileWorker = Substitute.For<IXmlWorker>();
            messageParser = new MessageParser(_fileWorker);
        }

        [Test]
        public void GetMessageWithCustomPathCallsDeserializeXmlTest()
        {
            // Arrange
            string defaultPathToFile = @"Message/message_info.xml";
            var messages = new List<ChallengeMessage>()
            {
                new ChallengeMessage() { MessageType = MessageType.EmptyDeviceContainer }
            };
            _fileWorker.DeserializeXml<List<ChallengeMessage>>(defaultPathToFile).Returns(messages);
            // Act
            messageParser.GetMessage(MessageType.EmptyDeviceContainer, defaultPathToFile);
            // Assert
            _fileWorker.Received().DeserializeXml<List<ChallengeMessage>>(defaultPathToFile);
        }

        [Test]
        public void GetMessageWithDefaultPathCallsDeserializeXmlTest()
        {
            // Arrange
            string defaultPathToFile = @"Message/message_info.xml";
            var messages = new List<ChallengeMessage>()
            {
                new ChallengeMessage() { MessageType = MessageType.EmptyDeviceContainer }
            };
            _fileWorker.DeserializeXml<List<ChallengeMessage>>(defaultPathToFile).Returns(messages);
            // Act
            messageParser.GetMessage(MessageType.EmptyDeviceContainer);
            // Assert
            _fileWorker.Received().DeserializeXml<List<ChallengeMessage>>(defaultPathToFile);
        }

        [Test]
        public void GetMessageWithDefaultPathReturnsAppropriateMessageTest()
        {
            // Arrange
            string defaultPathToFile = @"Message/message_info.xml";
            var messages = new List<ChallengeMessage>()
            {
                new ChallengeMessage() { MessageType = MessageType.EmptyDeviceContainer }
            };
            _fileWorker.DeserializeXml<List<ChallengeMessage>>(defaultPathToFile).Returns(messages);
            // Act
            var message = messageParser.GetMessage(MessageType.EmptyDeviceContainer);

            // Assert
            Assert.IsTrue(message.MessageType == MessageType.EmptyDeviceContainer);
        }

        [Test]
        public void GetMessageWithCustomPathReturnsAppropriateMessageTest()
        {
            // Arrange
            string defaultPathToFile = @"Message/message_info.xml";
            var messages = new List<ChallengeMessage>()
            {
                new ChallengeMessage() { MessageType = MessageType.EmptyDeviceContainer }
            };
            _fileWorker.DeserializeXml<List<ChallengeMessage>>(defaultPathToFile).Returns(messages);

            // Act
            var message = messageParser.GetMessage(MessageType.EmptyDeviceContainer, defaultPathToFile);

            // Assert
            Assert.IsTrue(message.MessageType == MessageType.EmptyDeviceContainer);
        }
    }
}
