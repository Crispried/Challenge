using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;
using Challange.Infrastructure.Archivator;

namespace Challange.UnitTests
{
    class ArchivatorTests
    {
        [TestFixture]
        public class UnitTest1
        {
            private string inputDirectoryPath = @"c:\example\start";

            private string outputDirectoryPath = @"c:\example\result.zip";

            [Test]
            public void ArchivateMethodThrowsUnknownExceptionOne()
            {
                Assert.Throws<UnableToMakeArchiveException>(
                    delegate
                    {
                        Archivate(null, null);
                    }
                );
            }

            [Test]
            public void ArchivateMethodThrowsUnknownExceptionTwo()
            {
                Assert.Throws<UnableToMakeArchiveException>(
                    delegate
                    {
                        Archivate("", "");
                    }
                );
            }

            [Test]
            public void ArchivateMethodThrowsUnknownExceptionIfDirectoryWasNotFound()
            {
                Assert.Throws<UnableToMakeArchiveException>(
                    delegate
                    {
                        Archivate(@"c:\DirectoryNotFound", @"c:\Archived\DirectoryNotFound");
                    }
                );
            }

            [Test]
            public void ArchivateMethodReturnsTrueAndCreatesArchive()
            {
                // Arrange

                // Act
                bool result = Archivate(GetInputDirectoryPath(),
                                        GetOutputDirectoryPath());

                // Assert
                Assert.IsTrue(File.Exists(GetOutputDirectoryPath()));
                Assert.IsTrue(result);

                DeleteFile(GetOutputDirectoryPath());
            }

            private void DeleteFile(string path)
            {
                File.Delete(path);
            }

            private bool Archivate(string from, string to)
            {
                return Archivator.Archivate(from, to);
            }

            private string GetInputDirectoryPath()
            {
                return this.inputDirectoryPath;
            }

            private string GetOutputDirectoryPath()
            {
                return this.outputDirectoryPath;
            }
        }
    }
}
