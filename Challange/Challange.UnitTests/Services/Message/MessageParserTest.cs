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
        private string pathToFile;

        [SetUp]
        public void SetUp()
        {
            pathToFile = @"Message/message_info.xml";
        }

        [Test]
        public void GetMessage()
        {
            // Arrange
            // Act
            //var message = MessageParser.GetMessage(
            //    MessageType.ChallengeSettingsFileParseProblem, pathToFile);
            //// Assert
            //Assert.IsTrue(message.MessageType == 
            //    MessageType.ChallengeSettingsFileParseProblem);
        }
    }
}
