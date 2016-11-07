﻿using System;
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
using Challange.Domain.Entities;
using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class CamerasPresenterTest
    {
        private IApplicationController controller;
        private CamerasPresenter presenter;
        private ICamerasView view;
        private ICamerasContainer argument;
        private List<Device> devices;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<ICamerasView>();
            presenter = new CamerasPresenter(controller, view);
            devices = Substitute.For<List<Device>>();
        }

        [Test]
        public void RunIfDeviceListIsNotEmpty()
        {
            // Arrange
            Device device = CreateTestDevice();
            devices.Add(device);
            SetArgument(devices);
            presenter.Run(argument);
            // Act
            // Assert
            view.DidNotReceive().ShowNoConnectedCamerasLabel();
            view.ReceivedWithAnyArgs().FillCamerasListView(argument.GetCamerasNames);
            view.Received().Show();
        }

        [Test]
        public void RunIfDeviceListIsEmpty()
        {
            // Arrange
            devices.Clear();
            SetArgument(devices);
            presenter.Run(argument);
            // Act
            // Assert
            view.DidNotReceive().FillCamerasListView(argument.GetCamerasNames);
            view.Received().ShowNoConnectedCamerasLabel();
            view.Received().Show();
        }

        private void SetArgument(List<Device> devices)
        {
            argument = Substitute.For<ICamerasContainer>(devices);
        }

        private Device CreateTestDevice()
        {
            Device device = Substitute.For<Device>();
            device.FullName = "kurwa";
            return device;
        }

        private void InitializeDevicesList()
        {
            argument.GetCamerasNames.Add("s");
        }
    }
}
