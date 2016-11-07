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
    class CameraTest
    {
        private Camera camera;
        private string cameraName;
        private string fullName;

        [SetUp]
        public void SetUp()
        {
            cameraName = "Camera";
            fullName = "FCamera";
        }

        [Test]
        public void AddFrameAddsFrame()
        {
            // Arrange

            // Act

            // Assert
        }

    }
}
