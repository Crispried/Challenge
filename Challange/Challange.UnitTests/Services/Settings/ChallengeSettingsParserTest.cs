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
    class ChallengeSettingsParserTest : TestCase
    {
        private IFileWorker fileWorker;
        private ChallengeSettingsParser parser;
        private ChallengeSettings settings;
        private const string settingsPath = @"Settings\challenge.xml";

        [SetUp]
        public void SetUp()
        {
            fileWorker = Substitute.For<IFileWorker>();
            parser = new ChallengeSettingsParser(fileWorker);
            settings = InitializeChallengeSettings();
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
            fileWorker.Received().DeserializeXml<ChallengeSettings>(settingsPath);
        }

        [Test]
        public void GetSettingsRetunsNotNullSettingsTest()
        {
            // Arrange
            fileWorker.DeserializeXml<ChallengeSettings>(default(string)).ReturnsForAnyArgs(settings);
            // Act
            parser.GetSettings();
            // Assert
            Assert.NotNull(settings);
        }

        [Test]
        public void GetSettingsRetunsNullSettingsTest()
        {
            // Arrange
            var result = default(ChallengeSettings);
            fileWorker.DeserializeXml<ChallengeSettings>(default(string)).ReturnsForAnyArgs(result);
            // Act
            parser.GetSettings();
            // Assert
            Assert.IsNull(result);
        }
    }
}
