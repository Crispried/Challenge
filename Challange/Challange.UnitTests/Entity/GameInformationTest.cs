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
    class GameInformationTest
    {
        private string firstTeamName;
        private string secondTeamName;
        private string date;
        private GameInformation gameInfo;

        [SetUp]
        public void SetUp()
        {
            firstTeamName = "FirstTeam";
            secondTeamName = "SecondTeam";
            date = DateTime.Now.ToString();
            gameInfo = new GameInformation();
        }

        [Test]
        public void DirectoryNameIsProperlyFormattedWithDate()
        {
            // Arrange
            SetUpTeamNames();
            SetUpDate();

            // Act
            string expectedDirectoryName = firstTeamName + "_vs_" + secondTeamName + "(" + date + ")";

            // Assert
            Assert.AreEqual(expectedDirectoryName, GetFormattedDirectoryName());
        }

        [Test]
        public void DirectoryNameIsProperlyFormattedWithoutDate()
        {
            // Arrange
            SetUpTeamNames();

            // Act
            string expectedDirectoryName = firstTeamName + "_vs_" + secondTeamName + "()";

            // Assert
            Assert.AreEqual(expectedDirectoryName, GetFormattedDirectoryName());
        }

        private void SetUpTeamNames()
        {
            gameInfo.FirstTeam = firstTeamName;
            gameInfo.SecondTeam = secondTeamName;
        }

        private void SetUpDate()
        {
            gameInfo.Date = date;
        }

        private string GetFormattedDirectoryName()
        {
            return gameInfo.DirectoryName;
        }
    }
}
