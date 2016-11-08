using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using Challange.Domain.Services.FileSystem;

namespace Challange.UnitTests.Infrastructure
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
            archivator = new Archivator(fileService);
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
            bool result = Archivate(incorrectInputDirectoryPath,
                                    incorrectOutputFilePath);
            // Assert
            Assert.IsFalse(FileExists(incorrectOutputFilePath));
            Assert.IsFalse(result);
        }
            
        [Test]
        public void ArchivateMethodReturnsTrueAndCreatesArchive()
        {
            // Arrange
            // Act
            bool result = Archivate(correctInputDirectoryPath,
                                    correctOutputFilePath);

            // Assert
            Assert.IsTrue(FileExists(correctOutputFilePath));
            Assert.IsTrue(result);

            // Delete
            DeleteFile(correctOutputFilePath);
        }

        private bool Archivate(string from, string to)
        {
            return archivator.Archivate(from, to);
        }
    }
}
