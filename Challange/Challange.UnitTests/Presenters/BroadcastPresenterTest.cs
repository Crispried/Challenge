using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.PlayVideo.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.BroadcastPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class BroadcastPresenterTest : TestCase
    {
        private IApplicationController controller;
        private BroadcastPresenter presenter;
        private IBroadcastView view;
        private Tuple<object, BroadcastType> argument;
        private ICamerasProvider _camerasProvider;
        private IVideoPlayer _videoPlayer;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IBroadcastView>();
            _camerasProvider = Substitute.For<ICamerasProvider>();
            _videoPlayer = Substitute.For<IVideoPlayer>();
            presenter = new BroadcastPresenter(controller, view, _camerasProvider, _videoPlayer);
        }

        [Test]
        public void Run()
        {
            // Arrange
            argument = Tuple.Create(Substitute.For<object>(), default(BroadcastType));
            // Act
            presenter.Run(argument);
            // Assert
            view.Received().Show();
        }

        [Test]
        public void BroadcastShowCallbackIfBroadcastTypeIsStreamTest()
        {
            // Arrange
            var camera = Substitute.For<ICamera>();
            argument = Tuple.Create((object)camera, BroadcastType.Stream);
            var handler = Substitute.For<Action<object, EventArgs>>();
            presenter.Run(argument);
            // Act
            presenter.BroadcastShowCallback();
            // Assert
            _camerasProvider.ReceivedWithAnyArgs().StartCamera(camera, handler);
        }

        [Test]
        public void BroadcastShowCallbackIfBroadcastTypeIsReplayTest()
        {
            // Arrange
            var video = Substitute.For<Video>("video", new List<IFps>());
            argument = Tuple.Create((object)video, BroadcastType.Replay);
            presenter.Run(argument);
            // Act
            presenter.BroadcastShowCallback();
            // Assert
            _videoPlayer.Received().PlayVideo(video);
        }
    }
}
