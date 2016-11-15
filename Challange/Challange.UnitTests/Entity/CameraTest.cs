using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class CameraTest
    {
        private ICamera camera;
        private string cameraName;
        private string fullName;

        [SetUp]
        public void SetUp()
        {
            cameraName = "Camera";
            fullName = "FCamera";
        }

        [Test]
        public void AddFrameTest()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
