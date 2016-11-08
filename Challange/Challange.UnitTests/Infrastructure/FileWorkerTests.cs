using NUnit.Framework;
using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.FileSystem;

namespace Challange.UnitTests.Infrastructure
{
    [TestFixture]
    class FileWorkerTests : TestCase
    {
        private FileWorker fileWorker;
        private string correctPathToGameInformation = @"test\XmlFormatterResult.xml";
        private string correctPathToSettings = @"test\settings.xml";

        [SetUp]
        public void SetUp()
        {
            fileWorker = new FileWorker();
        }

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
       //     Assert.IsFalse(FileExists(incorrectOutputPath));
            Assert.IsFalse(result);
        }

        [Test]
        public void SerializeXmlMethodReturnsFalseWithBadGameInformation()
        {
            // Arrange

            // Act
            bool result = SerializeXml(null, correctPathToGameInformation);

            // Assert
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
         //   Assert.IsTrue(FileExists(correctPathToGameInformation));
            Assert.IsTrue(result);

            // Delete
          //  DeleteFile(correctPathToGameInformation);
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

        private bool SerializeXml(object gameInformation, string outputPathForXml)
        {
            return fileWorker.SerializeXml(gameInformation, outputPathForXml);
        }

        private PlayerPanelSettings DeserializeXml(string outputPathForXml)
        {
            return fileWorker.DeserializeXml<PlayerPanelSettings>(outputPathForXml);
        }
    }
}
