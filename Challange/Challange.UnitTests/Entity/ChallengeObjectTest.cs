using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class ChallengeObjectTest
    {
        private ChallengeObject challengeObject;
        private string pathToRootDirectory = "challengeRoot";
        private string challengeFolderName = "challengeFolder:folder";

        [SetUp]
        public void SetUp()
        {
            challengeObject = new ChallengeObject(pathToRootDirectory, challengeFolderName);  
        }

        [Test]
        public void GetChallengeDirectoryPathReturnsFormattedPath()
        {
            // Arrange

            // Act
            string expectedPath = "challengeRoot\\challengeFolder_folder\\";

            // Assert
            Assert.AreEqual(expectedPath, GetChallengeDirectoryPath());
        }

        private string GetChallengeDirectoryPath()
        {
            return challengeObject.PathToChallengeDirectory;
        }
    }
}
