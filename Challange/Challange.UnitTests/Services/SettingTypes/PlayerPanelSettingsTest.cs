using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests.Services.SettingTypes
{
    class PlayerPanelSettingsTest : TestCase
    {
        private PlayerPanelSettings playerPanelSettings;

        [SetUp]
        public void SetUp()
        {
            playerPanelSettings = new PlayerPanelSettings();
        }

        [Test]
        public void AutoSizeModePropertyTest()
        {
            // Arrange
            bool enabled = false;

            // Act
            playerPanelSettings.AutosizeMode = enabled;

            // Assert
            Assert.AreEqual(enabled, playerPanelSettings.AutosizeMode);
        }

        [Test]
        public void PlayerWidthPropertyTest()
        {
            // Arrange
            int playerWidth = 100;

            // Act
            playerPanelSettings.PlayerWidth = playerWidth;

            // Assert
            Assert.AreEqual(playerWidth, playerPanelSettings.PlayerWidth);
        }

        [Test]
        public void PlayerHeightPropertyTest()
        {
            // Arrange
            int playerHeight = 100;

            // Act
            playerPanelSettings.PlayerHeight = playerHeight;

            // Assert
            Assert.AreEqual(playerHeight, playerPanelSettings.PlayerHeight);
        }

        [Test]
        public void SetSettingsTest()
        {
            // Arrange
            PlayerPanelSettings newSettings = InitializePlayerPanelSettings();

            // Act
            playerPanelSettings.SetSettings(newSettings);

            // Assert
            Assert.AreEqual(playerPanelSettings.AutosizeMode, newSettings.AutosizeMode);
            Assert.AreEqual(playerPanelSettings.PlayerWidth, newSettings.PlayerWidth);
            Assert.AreEqual(playerPanelSettings.PlayerHeight, newSettings.PlayerHeight);
        }
    }
}
