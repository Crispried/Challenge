using NUnit.Framework;
using Challange.Domain.Services.FileSystem;

namespace Challange.UnitTests.Services.FileSystem
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
