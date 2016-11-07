using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests.Services.SettingTypes
{
    class FtpSettingsTest : TestCase
    {
        private FtpSettings ftpSettings;

        [SetUp]
        public void SetUp()
        {
            ftpSettings = new FtpSettings();
        }

        [Test]
        public void FtpAddressPropertyTest()
        {
            // Arrange
            string ftpAddress = "ftp://test.test";

            // Act
            ftpSettings.FtpAddress = ftpAddress;

            // Assert
            Assert.AreEqual(ftpAddress, ftpSettings.FtpAddress);
        }

        [Test]
        public void UserNamePropertyTest()
        {
            // Arrange
            string userName = "UserName";

            // Act
            ftpSettings.UserName = userName;

            // Assert
            Assert.AreEqual(userName, ftpSettings.UserName);
        }

        [Test]
        public void PasswordPropertyTest()
        {
            // Arrange
            string password = "Password";

            // Act
            ftpSettings.Password = password;

            // Assert
            Assert.AreEqual(password, ftpSettings.Password);
        }

        [Test]
        public void SetSettingsTest()
        {
            // Arrange
            FtpSettings newSettings = InitializeFtpSettings();

            // Act
            ftpSettings.SetSettings(newSettings);

            // Assert
            Assert.AreEqual(ftpSettings.FtpAddress, newSettings.FtpAddress);
            Assert.AreEqual(ftpSettings.UserName, newSettings.UserName);
            Assert.AreEqual(ftpSettings.Password, newSettings.Password);
        }
    }
}
