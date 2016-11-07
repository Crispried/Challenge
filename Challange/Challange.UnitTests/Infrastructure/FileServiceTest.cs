using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using NSubstitute;

namespace Challange.UnitTests.Infrastructure
{
    [TestFixture]
    class FileServiceTest : TestCase
    {
        private IProcessStarter processStarter;

        [SetUp]
        public void SetUp()
        {
            processStarter = Substitute.For<IProcessStarter>();
        }

        [Test]
        public void FilterFolderNameReplacement()
        {
            // Arrange
            string folderName = "folder:name";

            // Act
            string filtered = FilterFolderName(folderName);
            string expected = "folder_name";

            // Assert
            Assert.AreEqual(expected, filtered);
        }

        [Test]
        public void CreateDirectoryTest()
        {
            // Arrange
            string path = "test2";

            // Act
            CreateDirectory(path);

            // Assert
            Assert.True(DirectoryExists(path));

            // Delete
            DeleteDirectory(path);
        }

        [Test]
        public void DeleteFileTest()
        {
            // Arrange
            string fileName = "testFile.txt";
            CreateFile(fileName);

            // Act
            DeleteFile(fileName);

            // Assert
            Assert.False(FileExists(fileName));
        }

        // FAILS and opens the window
        [Test]
        public void ProcessStartTest()
        {
            // Arrange
            string path = "test2";
            CreateDirectory(path);

            // Act
            OpenFileOrFolder(path);

            // Assert
            processStarter.Received().StartProcess(path);

            // Delete
            DeleteDirectory(path);
        }
    }
}
