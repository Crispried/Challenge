using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class GameInformationTest : TestCase
    {
        private GameInformation gameInfo;

        [SetUp]
        public void SetUp()
        {
            gameInfo = new GameInformation();
        }

        [Test]
        public void DirectoryNamePropertyTest()
        {
            // Arrange
            string directoryName = "name";

            // Act
            gameInfo.DirectoryName = directoryName;

            // Assert
            Assert.AreEqual(directoryName, gameInfo.DirectoryName);
        }

        [Test]
        public void SetGameInformationTest()
        {
            // Arrange
            GameInformation gameInformation = InitializeGameInformation();

            // Act
            gameInfo.SetGameInformation(gameInformation);

            // Assert
            Assert.NotNull(gameInfo.FirstTeam);
            Assert.NotNull(gameInfo.SecondTeam);
            Assert.NotNull(gameInfo.Date);
            Assert.NotNull(gameInfo.GameStart);
            Assert.NotNull(gameInfo.Country);
            Assert.NotNull(gameInfo.City);
            Assert.NotNull(gameInfo.Part);
        }
    }
}
