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
    class PathFormatterTest
    {
        private IPathFormatter pathFormatter;
        private string directoryName = "Directory";

        [SetUp]
        public void SetUp()
        {
            pathFormatter = new PathFormatter();
        }

        [Test]
        public void FormatPathToGameInformationFileTest()
        {
            // Arrange

            // Act
            string pathToFile = pathFormatter.FormatPathToGameInformationFile(directoryName);

            // Assert
            Assert.AreEqual(directoryName + "\\Game_Information.xml", pathToFile);
        }
    }
}
