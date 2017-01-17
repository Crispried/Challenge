using NUnit.Framework;
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.FileSystem.Concrete;

namespace Challange.UnitTests.Services.FileSystem
{
    [TestFixture]
    class PathFormatterTest : TestCase
    {
        private IPathFormatter _pathFormatter;
        private const string _directoryName = "Directory";

        [SetUp]
        public void SetUp()
        {
            _pathFormatter = new PathFormatter();
        }

        [Test]
        public void FormatPathToGameInformationFileTest()
        {
            // Arrange
            // Act
            string pathToFile = _pathFormatter.FormatPathToGameInformationFile(_directoryName);
            // Assert
            Assert.AreEqual(_directoryName + "\\Game_Information.xml", pathToFile);
        }

        [Test]
        public void FilterFolderNameTest()
        {
            // Arrange
            string notFiltered = "a:b";
            // Act
            string filtered = _pathFormatter.FilterFolderName(notFiltered);
            // Assert
            Assert.IsTrue(filtered == "a_b");
        }
    }
}
