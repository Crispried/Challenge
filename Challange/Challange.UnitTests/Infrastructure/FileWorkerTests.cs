using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests.Infrastructure
{
    [TestFixture]
    class FileWorkerTests : TestCase
    {
        private string correctPathToGameInformation = @"test\XmlFormatterResult.xml";
        private string correctPathToSettings = @"test\settings.xml";

        [Test]
        public void SerializeXmlMethodReturnsFalse(
                    [Values(null, "", @"c:\Archived\DirectoryNotFound")]
                    string incorrectOutputPath)
        {
            // Arrange
            var gameInformation = GetGameInformation();

            // Act
            bool result = SerializeXml(gameInformation, incorrectOutputPath);

            // Assert
            Assert.IsFalse(FileExists(incorrectOutputPath));
            Assert.IsFalse(result);
        }

        [Test]
        public void SerializeXmlMethodReturnsTrue()
        {
            // Arrange
            var gameInformation = GetGameInformation();

            // Act
            bool result = SerializeXml(gameInformation, correctPathToGameInformation);

            // Assert
            Assert.IsTrue(FileExists(correctPathToGameInformation));
            Assert.IsTrue(result);

            // Delete
            DeleteFile(correctPathToGameInformation);
        }
        
        [Test]
        public void DeserializeXmlMethod()
        {
            // Arrange
            // Act
            PlayerPanelSettings result = DeserializeXml(correctPathToSettings);

            // Assert
            Assert.That(result, Is.TypeOf<PlayerPanelSettings>());
        }


        private GameInformation GetGameInformation()
        {
            GameInformation gameInformation = new GameInformation()
            {
                FirstTeam = "Red",
                SecondTeam = "Blue",
                Date = "09.08.2016",               
                GameStart = "18:36:00",
                Country = "England",
                City = "London",
                Part = "2"
            };
            return gameInformation;
        }

        private bool SerializeXml(GameInformation gameInformation, string outputPathForXml)
        {
            return FileWorker.SerializeXml(gameInformation, outputPathForXml);
        }

        private PlayerPanelSettings DeserializeXml(string outputPathForXml)
        {
            return FileWorker.DeserializeXml<PlayerPanelSettings>(outputPathForXml);
        }
    }
}
