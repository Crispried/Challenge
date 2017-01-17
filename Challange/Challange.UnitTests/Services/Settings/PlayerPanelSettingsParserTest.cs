using NUnit.Framework;
using NSubstitute;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.FileSystem.Abstract;

namespace Challange.UnitTests.Services.SettingParsers
{
    [TestFixture]
    class PlayerPanelSettingsParserTest : TestCase
    {
        private PlayerPanelSettingsParser parser;
        private IXmlWorker fileWorker;
        private PlayerPanelSettings settings;
        private const string settingsPath = @"Settings\player_panel.xml";

        [SetUp]
        public void SetUp()
        {
            fileWorker = Substitute.For<IXmlWorker>();
            parser = new PlayerPanelSettingsParser(fileWorker);
            settings = InitializePlayerPanelSettings();
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
            fileWorker.Received().DeserializeXml<PlayerPanelSettings>(settingsPath);
        }

        [Test]
        public void GetSettingsRetunsNotNullSettingsTest()
        {
            // Arrange
            fileWorker.DeserializeXml<PlayerPanelSettings>(default(string)).ReturnsForAnyArgs(settings);
            // Act
            parser.GetSettings();
            // Assert
            Assert.NotNull(settings);
        }

        [Test]
        public void GetSettingsRetunsNullSettingsTest()
        {
            // Arrange
            var result = default(PlayerPanelSettings);
            fileWorker.DeserializeXml<PlayerPanelSettings>(default(string)).ReturnsForAnyArgs(result);
            // Act
            parser.GetSettings();
            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void SettingsFilePathPropertyTest()
        {
            // Arrange
            string path = "test_path";

            // Act
            parser.SettingsFilePath = path;

            // Assert
            Assert.AreEqual(path, parser.SettingsFilePath);
        }
    }
}
