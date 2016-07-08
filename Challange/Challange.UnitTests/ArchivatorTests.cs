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
            // change on realtion path in future
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
                Assert.IsFalse(File.Exists(incorrectOutputFilePath));
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
                Assert.IsTrue(File.Exists(correctOutputFilePath));
                Assert.IsTrue(result);
                // because of we don't want to leave archive which we created
                FileService.DeleteFile(correctOutputFilePath);
            }

            private bool Archivate(string from, string to)
            {
                return Archivator.Archivate(from, to);
            }
        }
    }
}
