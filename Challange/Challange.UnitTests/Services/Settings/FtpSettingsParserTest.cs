using NUnit.Framework;
using NSubstitute;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.FileSystem.Abstract;

namespace Challange.UnitTests.Services.SettingParsers
{
    [TestFixture]
    class FtpSettingsParserTest : TestCase
    {
        private IFileWorker fileWorker;
        private FtpSettingsParser parser;
        private FtpSettings settings;
        private const string settingsPath = @"Settings\ftp.xml";

        [SetUp]
        public void SetUp()
        {
            fileWorker = Substitute.For<IFileWorker>();
            parser = new FtpSettingsParser(fileWorker);
            settings = InitializeFtpSettings();
        }

        [Test]
        public void SaveSettingsCallsSerializeXml()
        {
            // Arrange
            // Act
            parser.SaveSettings(settings);
            // Assert
            fileWorker.Received().SerializeXml(settings, settingsPath);
        }

        [Test]
        public void SaveSettingsRetunsTrueIfSettingsFileWasFound()
        {
            // Arrange
            fileWorker.SerializeXml(default(object), default(string)).ReturnsForAnyArgs(true);
            // Act
            var settingsAreSaved = parser.SaveSettings(settings);
            // Assert
            Assert.IsTrue(settingsAreSaved);
        }

        [Test]
        public void SaveSettingsRetunsFalseIfSettingsAreNull()
        {
            // Arrange
            fileWorker.SerializeXml(default(object), default(string)).ReturnsForAnyArgs(false);
            // Act
            bool settingsAreSaved = parser.SaveSettings(null);
            // Assert
            Assert.False(settingsAreSaved);
        }

        [Test]
        public void GetSettingsCallsDeserializeXmlTest()
        {
            // Arrange
            // Act
            parser.GetSettings();
            // Assert
            fileWorker.Received().DeserializeXml<FtpSettings>(settingsPath);
        }

        [Test]
        public void GetSettingsRetunsNotNullSettingsTest()
        {
            // Arrange
            fileWorker.DeserializeXml<FtpSettings>(default(string)).ReturnsForAnyArgs(settings);
            // Act
            parser.GetSettings();
            // Assert
            Assert.NotNull(settings);
        }

        [Test]
        public void GetSettingsRetunsNullSettingsTest()
        {
            // Arrange
            var result = default(FtpSettings);
            fileWorker.DeserializeXml<FtpSettings>(default(string)).ReturnsForAnyArgs(result);
            // Act
            parser.GetSettings();
            // Assert
            Assert.IsNull(result);
        }
    }
}
