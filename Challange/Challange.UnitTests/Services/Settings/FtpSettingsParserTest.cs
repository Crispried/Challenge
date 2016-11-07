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
    class FtpSettingsParserTest : TestCase
    {
        private IFileWorker fileWorker;
        private FtpSettingsParser parser;
        private FtpSettings settings;

        [SetUp]
        public void SetUp()
        {
            fileWorker = new FileWorker();
            parser = new FtpSettingsParser(fileWorker);
            settings = InitializeFtpSettings();
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
            FtpSettings settings = parser.GetSettings();

            // Assert
            Assert.NotNull(settings);
        }

        // Test catch case
    }
}
