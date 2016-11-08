using Challange.Domain.Services.Message;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Services.Message
{
    [TestFixture]
    class MessageParserTest
    {
        MessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            messageParser = new MessageParser();
        }

        [Test]
        public void GetMessage()
        {
            // Arrange
            string pathToFile = @"Message/message_info.xml";
            string caption = messageParser.GetMessage(MessageType.EmptyDeviceContainer).Caption;

            // Act
            ChallengeMessage receivedMessage = messageParser.GetMessage(MessageType.EmptyDeviceContainer, pathToFile);

            // Assert
            Assert.AreEqual(caption, receivedMessage.Caption);
        }
    }
}
