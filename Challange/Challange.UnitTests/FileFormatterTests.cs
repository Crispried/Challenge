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
            bool result = FormatXml(gameInfroamtion, incorrectOutputPath);

            // Assert
            Assert.IsFalse(FileExists(incorrectOutputPath));
            Assert.IsFalse(result);
        }

        [Test]
        public void FormatXmlMethodReturnsTrue()
        {
            // Arrange
            var gameInfroamtion = GetGameInformation();

            // Act
            bool result = FormatXml(gameInfroamtion, correctOutputPathForXml);

            // Assert
            Assert.IsTrue(FileExists(correctOutputPathForXml));
            Assert.IsTrue(result);

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

        private void DeleteFile(string path)
        {
            FileService.DeleteFile(path);
        }

        private bool FileExists(string path)
        {
            return FileService.FileExists(path);
        }

        private bool FormatXml(GameInformation gameInfroamtion, string outputPathForXml)
        {
            return FileFormatter.FormatXml(gameInfroamtion, outputPathForXml);
        }
    }
}
