using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using PylonC.NETSupportLibrary;
using NSubstitute;
using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.UnitTests.Services.StreamProcess.Concrete.Pylon
{
    class CameraTest
    {
        private uint index;
        private string fullName;
        private ICamera camera;
        private ImageProvider imageProvider;

        [SetUp]
        public void SetUp()
        {
            index = 1;
            fullName = "FullName";
            camera = Substitute.For<ICamera>();
            imageProvider = Substitute.For<ImageProvider>();
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
