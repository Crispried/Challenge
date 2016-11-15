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
            ftpConnector = CreateFtpConnector(validHostName, login, password);

            // Act
            bool success = TestFtpConnection(ftpConnector);

            // Assert
            Assert.True(success);
        }

        [Test]
        public void IsFtpConnectionSuccessfulReturnsFalse()
        {
            // Arrange
            ftpConnector = CreateFtpConnector(invalidHostName, login, password);

            // Act
            bool success = TestFtpConnection(ftpConnector);

            // Assert
            Assert.False(success);
        }

        private FtpConnector CreateFtpConnector(string hostName, string login, string password)
        {
            return new FtpConnector(hostName, login, password);
        }

        private bool TestFtpConnection(FtpConnector ftpConnector)
        {
            return ftpConnector.IsFtpConnectionSuccessful();
        }
    }
}
