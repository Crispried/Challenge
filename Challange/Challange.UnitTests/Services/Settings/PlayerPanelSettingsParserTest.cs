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

namespace Challange.UnitTests.Services.SettingParsers
{
    [TestFixture]
    class PlayerPanelSettingsParserTest : TestCase
    {
        private PlayerPanelSettingsParser parser;
        private PlayerPanelSettings settings;

        [SetUp]
        public void SetUp()
        {
           // parser = new PlayerPanelSettingsParser();
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
        public void GetSettingsRetunsSettingsIfSettingsFileWasFound()
        {
            // Arrange

            // Act
            PlayerPanelSettings settings = parser.GetSettings();

            // Assert
            Assert.NotNull(settings);
        }

        [Test]
        public void GetSettingsRetunsNullIfSettingsFileWasNotFound()
        {
            // Arrange
            parser.SettingsFilePath = "not_exists";

            // Act
            PlayerPanelSettings settings = parser.GetSettings();

            // Assert
            Assert.Null(settings);
        }
    }
}
