using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests.Services.SettingTypes
{
    class RewindSettingsTest : TestCase
    {
        private RewindSettings rewindSettings;

        [SetUp]
        public void SetUp()
        {
            rewindSettings = new RewindSettings();
        }

        [Test]
        public void ForwardPropertyTest()
        {
            // Arrange
            int forward = 10;

            // Act
            rewindSettings.Forward = forward;

            // Assert
            Assert.AreEqual(forward, rewindSettings.Forward);
        }

        [Test]
        public void BackwardPropertyTest()
        {
            // Arrange
            int backward = 10;

            // Act
            rewindSettings.Backward = backward;

            // Assert
            Assert.AreEqual(backward, rewindSettings.Backward);
        }

        [Test]
        public void SetSettingsTest()
        {
            // Arrange
            RewindSettings newSettings = InitializeRewindSettings();

            // Act
            rewindSettings.SetSettings(newSettings);

            // Assert
            Assert.AreEqual(rewindSettings.Forward, newSettings.Forward);
            Assert.AreEqual(rewindSettings.Backward, newSettings.Backward);
        }
    }
}
