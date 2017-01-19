using NUnit.Framework;
using Challange.Domain.Services.Ftp.Abstract;
using Challange.Domain.Services.Ftp.Concrete;

namespace Challange.UnitTests.Services.Ftp
{
    [TestFixture]
    class FtpWorkerTest : TestCase
    {
        private string validHostName;
        private string invalidHostName;
        private string login;
        private string password;
        private IFtpWorker _ftpWorker;

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
            _ftpWorker = CreateFtpConnector(validHostName, login, password);

            // Act
            bool success = TestFtpConnection(_ftpWorker);

            // Assert
            Assert.True(success);
        }

        [Test]
        public void IsFtpConnectionSuccessfulReturnsFalse()
        {
            // Arrange
            _ftpWorker = CreateFtpConnector(invalidHostName, login, password);

            // Act
            bool success = TestFtpConnection(_ftpWorker);

            // Assert
            Assert.False(success);
        }

        private FtpWorker CreateFtpConnector(string hostName, string login, string password)
        {
            return new FtpWorker(hostName, login, password);
        }

        private bool TestFtpConnection(IFtpWorker ftpConnector)
        {
            return ftpConnector.IsFtpConnectionSuccessful();
        }
    }
}
