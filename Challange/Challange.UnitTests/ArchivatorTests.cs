using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using Challange.Domain.Infrastructure;

namespace Challange.UnitTests
{
    class ArchivatorTests
    {
        [TestFixture]
        public class UnitTest1
        {
            // change to realtive path in the future
            private string correctInputDirectoryPath = @"test\ArchivatorTest";
            private string correctOutputFilePath = @"test\ArchivatorTestResult.zip";

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
                Assert.IsTrue(FileExists(correctInputDirectoryPath));
                Assert.IsTrue(result);

                // Delete
                DeleteFile(correctInputDirectoryPath);
            }

            private bool Archivate(string from, string to)
            {
                return Archivator.Archivate(from, to);
            }

            private bool FileExists(string path)
            {
                return File.Exists(path);
            }

            private void DeleteFile(string path)
            {
                FileService.DeleteFile(path);
            }
        }
    }
}
