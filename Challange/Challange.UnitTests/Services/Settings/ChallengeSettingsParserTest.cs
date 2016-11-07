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
using Challange.Domain.Infrastructure;

namespace Challange.UnitTests.Services.SettingParsers
{
    [TestFixture]
    class ChallengeSettingsParserTest : TestCase
    {
        private IFileWorker fileWorker;
        private ChallengeSettingsParser parser;
        private ChallengeSettings settings;

        [SetUp]
        public void SetUp()
        {
            fileWorker = new FileWorker();
            parser = new ChallengeSettingsParser(fileWorker);
            settings = InitializeChallengeSettings();
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
            ChallengeSettings settings = parser.GetSettings();

            // Assert
            Assert.NotNull(settings);
        }

        // Test exception
    }
}
