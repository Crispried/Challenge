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
        private PlayerPanelSettings _playerPanelSettings;

        [SetUp]
        public void SetUp()
        {
            _playerPanelSettings = InitializePlayerPanelSettings();
        }

        [Test]
        public void AutoSizeModePropertyTest()
        {
            // Arrange
            bool enabled = false;

            // Act
            _playerPanelSettings.AutosizeMode = enabled;

            // Assert
            Assert.AreEqual(enabled, _playerPanelSettings.AutosizeMode);
        }

        [Test]
        public void PlayerWidthPropertyTest()
        {
            // Arrange
            int playerWidth = 100;

            // Act
            _playerPanelSettings.PlayerWidth = playerWidth;

            // Assert
            Assert.AreEqual(playerWidth, _playerPanelSettings.PlayerWidth);
        }

        [Test]
        public void PlayerHeightPropertyTest()
        {
            // Arrange
            int playerHeight = 100;

            // Act
            _playerPanelSettings.PlayerHeight = playerHeight;

            // Assert
            Assert.AreEqual(playerHeight, _playerPanelSettings.PlayerHeight);
        }

        [Test]
        public void SetSettingsTest()
        {
            // Arrange
            PlayerPanelSettings newSettings = InitializePlayerPanelSettings();

            // Act
            _playerPanelSettings.SetSettings(newSettings);

            // Assert
            Assert.AreEqual(_playerPanelSettings.AutosizeMode, newSettings.AutosizeMode);
            Assert.AreEqual(_playerPanelSettings.PlayerWidth, newSettings.PlayerWidth);
            Assert.AreEqual(_playerPanelSettings.PlayerHeight, newSettings.PlayerHeight);
        }

        [Test]
        public void EqualsReturnsTrueIfAutosizeModeIsFalseTest()
        {
            // Arrange
            var playerPanelSettings = new PlayerPanelSettings()
            {
                AutosizeMode = false,
                PlayerHeight = 480,
                PlayerWidth = 640
            };
            // Act
            var areEqual = _playerPanelSettings.Equals(playerPanelSettings);
            // Assert
            Assert.True(areEqual);
        }

        [Test]
        public void EqualsReturnsTrueIfAutosizeModeIsTrueTest()
        {
            // Arrange
            _playerPanelSettings = new PlayerPanelSettings()
            {
                AutosizeMode = true,
                PlayerHeight = 480,
                PlayerWidth = 640
            };
            var playerPanelSettings = new PlayerPanelSettings()
            {
                AutosizeMode = true,
                PlayerHeight = 480,
                PlayerWidth = 640
            };
            // Act
            var areEqual = _playerPanelSettings.Equals(playerPanelSettings);
            // Assert
            Assert.True(areEqual);
        }

        [Test]
        public void EqualsReturnsFalseTest()
        {
            // Arrange
            var playerPanelSettings = new PlayerPanelSettings()
            {
                AutosizeMode = false,
                PlayerHeight = 200,
                PlayerWidth = 260
            };
            // Act
            var areEqual = _playerPanelSettings.Equals(playerPanelSettings);
            // Assert
            Assert.False(areEqual);
        }

        [Test]
        public void EqualsReturnsFalseOnNullTest()
        {
            // Arrange
            PlayerPanelSettings playerPanelSettings = null;
            // Act
            var areEqual = _playerPanelSettings.Equals(playerPanelSettings);
            // Assert
            Assert.False(areEqual);
        }
    }
}
