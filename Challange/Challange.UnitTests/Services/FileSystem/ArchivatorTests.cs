using NUnit.Framework;
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.FileSystem.Concrete;

namespace Challange.UnitTests.Services.FileSystem
{
    [TestFixture]
    class ArchivatorTests : TestCase
    {
        // change to realtive path in the future
        private Archivator archivator;
        private IFileService fileService;
        private IProcessStarter processStarter;

        private string correctInputDirectoryPath = @"test\ArchivatorTest";
        private string correctOutputFilePath = @"test\ArchivatorTestResult.zip";

        [SetUp]
        public void SetUp()
        {
            processStarter = new ProcessStarter();
            fileService = new FileService(processStarter);
            archivator = new Archivator();
        }

        [Test]
        public void ArchivateMethodReturnsFalse(
                    [Values(null, "", @"c:\DirectoryNotFound")]
                    string incorrectInputDirectoryPath, 
                    [Values(null, "", @"c:\Archived\DirectoryNotFound")]
                    string incorrectOutputFilePath)
        {
            // Arrange
            // Act
            Archivate(incorrectInputDirectoryPath,
                                    incorrectOutputFilePath);
            // Assert
            Assert.IsFalse(fileService.FileExists(incorrectOutputFilePath));
        }
            
        [Test]
        public void ArchivateMethodReturnsTrueAndCreatesArchive()
        {
            // Arrange
            // Act
            Archivate(correctInputDirectoryPath,
                                    correctOutputFilePath);

            // Assert
            Assert.IsTrue(fileService.FileExists(correctOutputFilePath));

            // Delete
            fileService.DeleteFile(correctOutputFilePath);
        }

        private void Archivate(string from, string to)
        {
            archivator.Archivate(from, to);
        }
    }
}
