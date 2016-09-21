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
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void FilterFolderNameReplacement()
        {
            // Arrange
            string name = "folder:name";

            // Act
            string filtered = FilterFolderName(name);

            // Assert
            Assert.AreEqual("folder_name", filtered);
        }

        private string FilterFolderName(string name)
        {
            return FileService.FilterFolderName(name);
        }

        
    }
}
