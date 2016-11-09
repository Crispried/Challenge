using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Services.Settings.SettingTypes;
using NSubstitute;
using Challange.Domain.Services.FileSystem;

namespace Challange.UnitTests.Services.FileSystem
{
    [TestFixture]
    class FileServiceTest : TestCase
    {
        private IProcessStarter processStarter;
        private IFileService fileService;

        [SetUp]
        public void SetUp()
        {
            processStarter = Substitute.For<IProcessStarter>();
            fileService = new FileService(processStarter);
        }

        [Test]
        public void FilterFolderNameReplacement()
        {
            // Arrange
            string folderName = "folder:name";

            // Act
            string filtered = fileService.FilterFolderName(folderName);
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
            fileService.CreateDirectory(path);

            // Assert
            Assert.True(Directory.Exists(path));

            // Delete
            fileService.DeleteFile(path);
        }

        [Test]
        public void DeleteFileTest()
        {
            // Arrange
            string fileName = "testFile.txt";
            File.Create(fileName).Dispose();

            // Act
            fileService.DeleteFile(fileName);

            // Assert
            Assert.False(fileService.FileExists(fileName));
        }

        // FAILS and opens the window
        [Test]
        public void ProcessStartTest()
        {
            // Arrange
            string path = "test2";
            fileService.CreateDirectory(path);

            // Act
            fileService.OpenFileOrFolder(path);

            // Assert
            processStarter.Received().StartProcess(path);

            // Delete
            fileService.DeleteFile(path);
        }
    }
}
