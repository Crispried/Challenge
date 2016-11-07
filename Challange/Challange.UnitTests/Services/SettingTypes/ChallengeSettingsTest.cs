using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests.Services.SettingTypes
{
    class ChallengeSettingsTest : TestCase
    {
        private ChallengeSettings challengeSettings;

        [SetUp]
        public void SetUp()
        {
            challengeSettings = new ChallengeSettings();
        }

        [Test]
        public void NumberOfPastFPSPropertyTest()
        {
            // Arrange
            int number = 10;

            // Act
            challengeSettings.NumberOfPastFPS = number;

            // Assert
            Assert.AreEqual(number, challengeSettings.NumberOfPastFPS);
        }

        [Test]
        public void NumberOfFutureFPSPropertyTest()
        {
            // Arrange
            int number = 10;

            // Act
            challengeSettings.NumberOfFutureFPS = number;

            // Assert
            Assert.AreEqual(number, challengeSettings.NumberOfFutureFPS);
        }

        [Test]
        public void SetSettingsTest()
        {
            // Arrange
            ChallengeSettings newSettings = InitializeChallengeSettings();

            // Act
            challengeSettings.SetSettings(newSettings);

            // Assert
            Assert.AreEqual(challengeSettings.NumberOfPastFPS, newSettings.NumberOfPastFPS);
            Assert.AreEqual(challengeSettings.NumberOfFutureFPS, newSettings.NumberOfFutureFPS);
        }
    }
}
