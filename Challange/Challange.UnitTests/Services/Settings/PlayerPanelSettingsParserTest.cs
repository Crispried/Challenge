using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.FileSystem;

namespace Challange.UnitTests.Services.SettingParsers
{
    [TestFixture]
    class PlayerPanelSettingsParserTest : TestCase
    {
        private PlayerPanelSettingsParser parser;
        private IFileWorker fileWorker;
        private PlayerPanelSettings settings;

        [SetUp]
        public void SetUp()
        {
            fileWorker = new FileWorker();
            parser = new PlayerPanelSettingsParser(fileWorker);
            settings = InitializePlayerPanelSettings();
        }

        [Test]
        public void SaveSettingsRetunsTrueIfSettingsFileWasFound()
        {
            // Arrange

            // Act
            bool settingsAreSaved = parser.SaveSettings(settings);

            // Assert
            Assert.True(settingsAreSaved);
        }

        [Test]
        public void SaveSettingsRetunsFalseIfSettingsAreNull()
        {
            // Arrange

            // Act
            bool settingsAreSaved = parser.SaveSettings(null);

            // Assert
            Assert.False(settingsAreSaved);
        }

        [Test]
        public void GetSettingsRetunsSettingsTest()
        {
            // Arrange

            // Act
            PlayerPanelSettings settings = parser.GetSettings();

            // Assert
            Assert.NotNull(settings);
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

        [Test]
        public void GetSettingsRetunsSettingsTestException()
        {
            // Arrange
            parser.SettingsFilePath = null;

            // Act
            var settings = parser.GetSettings();

            // Assert
            Assert.Null(settings);
        }
    }
}
