using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.SettingsService.SettingTypes;
using Moq;

namespace Challange.UnitTests
{
    [TestFixture]
    class FileWorkerTests : TestCase
    {
        private string correctOutputPathForXml = @"test\XmlFormatterResult.xml";

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
            bool result = SerializeXml(gameInformation, correctOutputPathForXml);

            // Assert
            Assert.IsTrue(FileExists(correctOutputPathForXml));
            Assert.IsTrue(result);

            // Delete
            DeleteFile(correctOutputPathForXml);
        }

        [Test]
        public void DeserializeXmlMethodReturnsTrue()
        {
            // Arrange
            var gameInformation = GetGameInformation();

            // Act
            SerializeXml(gameInformation, correctOutputPathForXml);
            PlayerPanelSettings result = DeserializeXml(correctOutputPathForXml);

            // Assert
            Assert.That(result, Is.TypeOf<PlayerPanelSettings>());

            // Delete
            DeleteFile(correctOutputPathForXml);
        }


        private GameInformation GetGameInformation()
        {
            GameInformation gameInformation = new GameInformation()
            {
                FirstTeam = "Red",
                SecondTeam = "Blue",
                FirstTeamScore = 10,
                SecondTeamScore = 15,
                WinnerTeam = "Blue",
                GameStart = new DateTime(2016, 12, 3, 6, 35, 0),
                GameEnd = new DateTime(2016, 12, 3, 7, 30, 0),
                Challenges = GetChallenges()
            };
            return gameInformation;
        }

        private List<ChallengeCase> GetChallenges()
        {
            List<ChallengeCase> challenges = new List<ChallengeCase>()
            {
                new ChallengeCase()
                {
                    ChallengeTime = DateTime.Now,
                    UrlToChallangeMovies = @"test\path\1"
                },
                new ChallengeCase()
                {
                    ChallengeTime = DateTime.Now,
                    UrlToChallangeMovies = @"test\path\2"
                }
            };
            return challenges;
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
