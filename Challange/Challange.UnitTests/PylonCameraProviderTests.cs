using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using Moq;

namespace Challange.UnitTests
{
    class PylonCameraProviderTests
    {
        [Test]
        public void GetConnectedCamerasInvokesDeviceEnumerator()
        {
            // Can we test if library method from static class is being invoked?
            // Arrange
            
            // Act

            // Assert
        }
    }
}
