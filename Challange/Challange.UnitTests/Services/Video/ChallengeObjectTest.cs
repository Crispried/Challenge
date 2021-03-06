﻿using NUnit.Framework;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class ChallengeObjectTest : TestCase
    {
        private IChallengeObject challengeObject;
        private string pathToRootDirectory = "challengeRoot";
        private string challengeFolderName = "challengeFolder:folder";

        [SetUp]
        public void SetUp()
        {
            challengeObject = new ChallengeObject();
            challengeObject.Initialize(pathToRootDirectory, challengeFolderName);
        }

        [Test]
        public void GetPathToRootDirectoryTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(pathToRootDirectory, challengeObject.PathToRootDirectory);
        }

        [Test]
        public void GetChallengeFolderNameTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(challengeFolderName, challengeObject.ChallengeFolderName);
        }

        [Test]
        public void GetPathToChallengeDirectory()
        {
            // Arrange
            // Act
            var path = pathToRootDirectory + @"/" + challengeFolderName.Replace(":", "_");
            // Assert
            Assert.AreEqual(path, challengeObject.PathToChallengeDirectory);
        }

        [Test]
        public void SetPathToChallengeDirectory()
        {
            // Arrange
            // Act
            challengeObject.PathToChallengeDirectory = "bla";
            // Assert
            Assert.AreEqual("bla", challengeObject.PathToChallengeDirectory);
        }
    }
}
