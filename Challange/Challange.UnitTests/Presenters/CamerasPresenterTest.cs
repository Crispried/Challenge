using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using NSubstitute;
using Moq;
using Challange.Presenter.Presenters.CamerasPresenter;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class CamerasPresenterTest
    {
        private IApplicationController controller;
        private CamerasPresenter presenter;
        private ICamerasView view;
        private List<Device> argument;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<ICamerasView>();
            presenter = new CamerasPresenter(controller, view);
            argument = InitializeDevicesList();
            presenter.Run(argument);
        }

        [Test]
        public void Run()
        {
            // Arrange

            // Act
            presenter.Run(argument);

            // Assert
            Assert.True(presenter.CamerasListWindowIsOpened);
        }

        private List<Device> InitializeDevicesList()
        {
            List<Device> devicesList = new List<Device>();
            Device device = new Device();

            devicesList.Add(device);

            return devicesList;
        }
    }
}
