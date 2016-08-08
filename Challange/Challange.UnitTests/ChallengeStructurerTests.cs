using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using Challange.Domain.Infrastructure;
using Challange.Domain.Entities;
using System.Globalization;

namespace Challange.UnitTests
{
    [TestFixture]
    class ChallengeStructurerTests : TestCase
    {
        private string firstTeamName = "First Team";

        private string secondTeamName = "Second Team";

        private string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

        [Test]
        public void CreateDirectoryStructureForChallengeCreatesDirectorySchema()
        {
            // Arrange
            ChallengeStructurer cs = CreateChallengeStructurer();
            List<Camera> cameras = new List<Camera>();
            List<string> videoPaths = new List<string>();

            cameras = CreateCamerasAndAddToList(cameras);

            // Act
            videoPaths = cs.CreateDirectoryStructureForChallenge(cameras, firstTeamName, secondTeamName);

            // Assert
            foreach(string path in videoPaths)
            {
                Assert.True(File.Exists(path));
            }

            RemoveDirectorySchema(firstTeamName, secondTeamName);
        }

        [Test]
        public void CreateDirectoryStructureForChallengeReturnsCollectionOfVideoPaths()
        {
            // Arrange
            ChallengeStructurer cs = CreateChallengeStructurer();
            List<Camera> cameras = new List<Camera>();
            List<string> videoPaths = new List<string>();

            cameras = CreateCamerasAndAddToList(cameras);

            // Act
            videoPaths = cs.CreateDirectoryStructureForChallenge(cameras, firstTeamName, secondTeamName);

            // Assert
            Assert.IsNotNull(videoPaths);

            RemoveDirectorySchema(firstTeamName, secondTeamName);
        }

        [Test]
        public void CreateDirectoryStructureForChallengeNamesVideoWithTimestamp()
        {
            // Arrange
            ChallengeStructurer cs = CreateChallengeStructurer();
            List<Camera> cameras = new List<Camera>();
            List<string> videoPaths = new List<string>();

            cameras = CreateCamerasAndAddToList(cameras);

            // Act
            videoPaths = cs.CreateDirectoryStructureForChallenge(cameras, firstTeamName, secondTeamName);

            // Assert
            foreach(string path in videoPaths)
            {
                string videoName = GetVideoFileNameFromVideoPath(path);
                StringAssert.IsMatch(GetPatternToCheckTimestampFileName(), videoName);
            }

            RemoveDirectorySchema(firstTeamName, secondTeamName);
        }

        private ChallengeStructurer CreateChallengeStructurer()
        {
            return new ChallengeStructurer();
        }
    
        private List<Camera> CreateCamerasAndAddToList(List<Camera> cameras)
        {
            Camera mainCamera = new Camera("Main");
            Camera leftCamera = new Camera("Left");
            Camera rightCamera = new Camera("Right");

            cameras.Add(mainCamera);
            cameras.Add(leftCamera);
            cameras.Add(rightCamera);

            return cameras;
        }

        private string GetPatternToCheckTimestampFileName()
        {
            return "([0-9]{4})-([0-9]{1,2})-([0-9]{1,2})-([0-9]{1,2})-([0-9]{1,2})-([0-9]{1,2})";
        }

        private string GetVideoFileNameFromVideoPath(string path)
        {
            string[] splits = path.Split(Path.DirectorySeparatorChar);

            return splits.Last();
        }

        private void RemoveDirectorySchema(string firstTeamName, string secondTeamName)
        {
            Directory.Delete(desktopPath + "\\" + firstTeamName + "-vs-" + secondTeamName);
        }
    }
}
