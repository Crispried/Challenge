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

namespace Challange.UnitTests.Services
{
    [TestFixture]
    class SettingsServiceTest : TestCase
    {
        private ISettingsParser<ChallengeSettings> settingParser;
        private SettingsService<ChallengeSettings> settingService;
        private ChallengeSettings settings;

        [SetUp]
        public void SetUp()
        {
            settingParser = Substitute.For<ISettingsParser<ChallengeSettings>>();
            settingService = new SettingsService<ChallengeSettings>(settingParser);
            settings = InitializeChallengeSettings();
        }

        [Test]
        public void SaveSettingInvokesMethodFromParser()
        {
            // Arrange
            settingService.SaveSetting(settings);

            // Act
            // Assert
            settingParser.Received().SaveSettings(settings);
        }

        [Test]
        public void GetSettingInvokesMethodFromParser()
        {
            // Arrange
            settingService.GetSetting();

            // Act
            // Assert
            settingParser.Received().GetSettings();
        }
    }
}
