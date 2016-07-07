using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;

namespace Challange.UnitTests
{
    [TestFixture]
    class FileFormatterTests
    {
        private string correctOutputPathForXml = @"test\XmlFormatterResult";

        [Test]
        public void FormatXmlMethodReturnsFalse(
                    [Values(null, "", @"c:\Archived\DirectoryNotFound")]
                    string incorrectOutputPath)
        {
            // Arrange
            var gameInfroamtion = GetGameInformation();

            // Act
            bool result = FileFormatter.FormatXml(gameInfroamtion, incorrectOutputPath);

            // Assert
            Assert.IsFalse(FileService.FileExists(incorrectOutputPath));
            Assert.IsFalse(result);
        }

        [Test]
        public void FormatXmlMethodReturnsTrue()
        {
            // Arrange
            var gameInfroamtion = GetGameInformation();

            // Act
            bool result = FileFormatter.FormatXml(gameInfroamtion, correctOutputPathForXml);

            // Assert
            Assert.IsTrue(FileService.FileExists(correctOutputPathForXml));
            Assert.IsTrue(result);
            // because of we don't want to leave file which we created
            FileService.DeleteFile(correctOutputPathForXml);
        }

        private GameInformation GetGameInformation()
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
            GameInformation gameInformation = new GameInformation()
            {
                FirstTeam = "Red",
                SecondTeam = "Blue",
                FirstTeamScore = 10,
                SecondTeamScore = 15,
                WinnerTeam = "Blue",
                GameStart = new DateTime(2016, 12, 3, 6, 35, 0),
                GameEnd = new DateTime(2016, 12, 3, 7, 30, 0),
                Challenges = challenges
            };
            return gameInformation;
        }
    }
}
