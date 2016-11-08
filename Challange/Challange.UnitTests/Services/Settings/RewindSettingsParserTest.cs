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
    class RewindSettingsParserTest : TestCase
    {
        private IFileWorker fileWorker;
        private RewindSettingsParser parser;
        private RewindSettings settings;

        [SetUp]
        public void SetUp()
        {
            fileWorker = new FileWorker();
            parser = new RewindSettingsParser(fileWorker);
            settings = InitializeRewindSettings();
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
            RewindSettings settings = parser.GetSettings();

            // Assert
            Assert.NotNull(settings);
        }

        // Test catch case

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
