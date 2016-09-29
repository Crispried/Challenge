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

namespace Challange.UnitTests.Infrastructure
{
    [TestFixture]
    class FileServiceTest : TestCase
    {
        private string folderName;

        [SetUp]
        public void SetUp()
        {
            folderName = "folder:name";
        }

        [Test]
        public void FilterFolderNameReplacement()
        {
            // Arrange

            // Act
            string filtered = FilterFolderName(folderName);
            string expected = "folder_name";

            // Assert
            Assert.AreEqual(expected, filtered);
        }

        private string FilterFolderName(string name)
        {
            return FileService.FilterFolderName(name);
        }

        
    }
}
