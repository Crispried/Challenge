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
            string message = "Ooops it looks like there are not any connected devices.";

            // Act
            ChallengeMessage receivedMessage = messageParser.GetMessage(MessageType.EmptyDeviceContainer);

            // Assert
            Assert.AreEqual(receivedMessage.Text, message);
        }
    }
}
