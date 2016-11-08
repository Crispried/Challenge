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
    class NullSettingsContainerTest : TestCase
    {
        private INullSettingsContainer nullSettingsContainer;

        [SetUp]
        public void SetUp()
        {
            nullSettingsContainer = new NullSettingsContainer();
        }

        [Test]
        public void NullSettingTypesGetterTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.IsEmpty(nullSettingsContainer.NullSettingTypes);
        }

        [Test]
        public void IsEmptyTestReturnsTrueIfNoElementsAdded()
        {
            // Arrange

            // Act
            bool result = nullSettingsContainer.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Test]
        public void IsEmptyTestReturnsFalseIfElementsWereAdded()
        {
            // Arrange
            nullSettingsContainer.NullSettingTypes.Add(typeof(PlayerPanelSettings));

            // Act
            bool result = nullSettingsContainer.IsEmpty();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void CheckPlayerPanelSettingsOnNullTest()
        {
            // Arrange
            nullSettingsContainer.CheckPlayerPanelSettingOnNull(null);

            // Act

            // Assert
            Assert.AreEqual(1, nullSettingsContainer.NullSettingTypes.Count);
        }

        [Test]
        public void CheckChallengeSettingOnNullTest()
        {
            // Arrange
            nullSettingsContainer.CheckChallengeSettingOnNull(null);

            // Act

            // Assert
            Assert.AreEqual(1, nullSettingsContainer.NullSettingTypes.Count);
        }

        [Test]
        public void CheckFtpSettingOnNullTest()
        {
            // Arrange
            nullSettingsContainer.CheckFtpSettingOnNull(null);

            // Act

            // Assert
            Assert.AreEqual(1, nullSettingsContainer.NullSettingTypes.Count);
        }

        [Test]
        public void CheckRewindSettingOnNullTest()
        {
            // Arrange
            nullSettingsContainer.CheckRewindSettingOnNull(null);

            // Act

            // Assert
            Assert.AreEqual(1, nullSettingsContainer.NullSettingTypes.Count);
        }
    }
}
