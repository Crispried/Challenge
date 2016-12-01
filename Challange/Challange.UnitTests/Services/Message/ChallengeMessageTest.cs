using Challange.Domain.Services.Message;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.UnitTests.Services.Message
{
    [TestFixture]
    class ChallengeMessageTest : TestCase
    {
        private ChallengeMessage challengeMessage;

        [SetUp]
        public void SetUp()
        {
            challengeMessage = new ChallengeMessage();
        }

        [Test]
        public void MessageTypePropertyTest()
        {
            // Arrange
            MessageType messageType = MessageType.FtpSettingsInvalid;
            challengeMessage.MessageType = messageType;

            // Act
            MessageType receivedMessageType = challengeMessage.MessageType;

            // Assert
            Assert.AreEqual(receivedMessageType, messageType);
        }

        [Test]
        public void CaptionPropertyTest()
        {
            // Arrange
            string caption = "Caption";
            challengeMessage.Caption = caption;

            // Act
            string receivedCaption = challengeMessage.Caption;

            // Assert
            Assert.AreEqual(receivedCaption, caption);
        }

        [Test]
        public void TextPropertyTest()
        {
            // Arrange
            string text = "Text";
            challengeMessage.Text = text;

            // Act
            string receivedText = challengeMessage.Text;

            // Assert
            Assert.AreEqual(receivedText, text);
        }

        [Test]
        public void MessageBoxButtonsPropertyTest()
        {
            // Arrange
            MessageBoxButtons buttonOK = MessageBoxButtons.OK;
            challengeMessage.MessageButtons = buttonOK;

            // Act
            MessageBoxButtons receivedButton = challengeMessage.MessageButtons;

            // Assert
            Assert.AreEqual(receivedButton, buttonOK);
        }

        [Test]
        public void MessageBoxIconPropertyTest()
        {
            // Arrange
            MessageBoxIcon errorIcon = MessageBoxIcon.Error;
            challengeMessage.MessageIcon = errorIcon;

            // Act
            MessageBoxIcon receivedIcon = challengeMessage.MessageIcon;

            // Assert
            Assert.AreEqual(receivedIcon, errorIcon);
        }
    }
}
