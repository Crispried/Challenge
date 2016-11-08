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
using Challange.Domain.Services.Message;

namespace Challange.UnitTests.Services.Settings
{
    [TestFixture]
    class SettingsContextTest
    {
        private ISettingsContext settingsContext;
        ISettingsService<PlayerPanelSettings> playerPanelSettingsService;
        ISettingsService<ChallengeSettings> challengeSettingsService;
        ISettingsService<FtpSettings> ftpSettingsService;
        ISettingsService<RewindSettings> rewindSettingsService;

        [SetUp]
        public void SetUp()
        {
            playerPanelSettingsService = Substitute.For<ISettingsService<PlayerPanelSettings>>();
            challengeSettingsService = Substitute.For<ISettingsService<ChallengeSettings>>();
            ftpSettingsService = Substitute.For<ISettingsService<FtpSettings>>();
            rewindSettingsService = Substitute.For<ISettingsService<RewindSettings>>();
            settingsContext = new SettingsContext(playerPanelSettingsService, challengeSettingsService,
                            ftpSettingsService, rewindSettingsService);
        }

        [Test]
        public void GetPlayerPanelSettingTest()
        {
            // Arrange

            // Act
            settingsContext.GetPlayerPanelSetting();

            // Assert
            playerPanelSettingsService.Received().GetSetting();
        }

        [Test]
        public void SavePlayerPanelSettingTest()
        {
            // Arrange

            // Act
            settingsContext.SavePlayerPanelSetting(null);

            // Assert
            playerPanelSettingsService.Received().SaveSetting(null);
        }

        [Test]
        public void GetChallengeSettingTest()
        {
            // Arrange

            // Act
            settingsContext.GetChallengeSetting();

            // Assert
            challengeSettingsService.Received().GetSetting();
        }

        [Test]
        public void SaveChallengeSettingTest()
        {
            // Arrange

            // Act
            settingsContext.SaveChallengeSetting(null);

            // Assert
            challengeSettingsService.Received().SaveSetting(null);
        }

        [Test]
        public void GetFtpSettingTest()
        {
            // Arrange

            // Act
            settingsContext.GetFtpSetting();

            // Assert
            ftpSettingsService.Received().GetSetting();
        }

        [Test]
        public void SaveFtpSettingTest()
        {
            // Arrange

            // Act
            settingsContext.SaveFtpSetting(null);

            // Assert
            ftpSettingsService.Received().SaveSetting(null);
        }

        [Test]
        public void GetRewindSettingTest()
        {
            // Arrange

            // Act
            settingsContext.GetRewindSetting();

            // Assert
            rewindSettingsService.Received().GetSetting();
        }

        [Test]
        public void SaveRewindSettingTest()
        {
            // Arrange

            // Act
            settingsContext.SaveRewindSetting(null);

            // Assert
            rewindSettingsService.Received().SaveSetting(null);
        }
    }
}
