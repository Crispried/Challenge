using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using PylonC.NETSupportLibrary;
using NSubstitute;

namespace Challange.UnitTests.Services.StreamProcess.Concrete.Pylon
{
    class PylonCameraTest
    {
        private uint index;
        private string fullName;
        private PylonCamera camera;
        private IPylonImageProvider imageProvider;

        [SetUp]
        public void SetUp()
        {
            index = 1;
            fullName = "FullName";
            camera = new PylonCamera(index, fullName);
            imageProvider = Substitute.For<IPylonImageProvider>();
        }

        [Test]
        public void StartTest()
        {
            // Arrange

            // Act
            camera.Start();

            // Assert
            imageProvider.ReceivedWithAnyArgs().Open(index);
            imageProvider.ReceivedWithAnyArgs().ContinuousShot();
        }
    }
}
