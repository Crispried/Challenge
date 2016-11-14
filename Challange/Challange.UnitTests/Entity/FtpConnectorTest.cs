using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class FtpConnectorTest
    {
        private string validHostName;
        private string invalidHostName;
        private string login;
        private string password;
        private FtpConnector ftpConnector;

        [SetUp]
        public void SetUp()
        {
            validHostName = "ftp://ftp.wsiz.rzeszow.pl";
            invalidHostName = "ftp://invalid.host.name";
            login = "w46999";
            password = "35162067160";
        }

        [Test]
        public void IsFtpConnectionSuccessfulReturnsTrue()
        {
            // Arrange
            ftpConnector = new FtpConnector(validHostName, login, password);

            // Act
            bool success = ftpConnector.IsFtpConnectionSuccessful();

            // Assert
            Assert.True(success);
        }

        [Test]
        public void IsFtpConnectionSuccessfulReturnsFalse()
        {
            // Arrange
            ftpConnector = new FtpConnector(invalidHostName, login, password);

            // Act
            bool success = ftpConnector.IsFtpConnectionSuccessful();

            // Assert
            Assert.False(success);
        }
    }
}
