using Challange.Domain.Services.Message;
using Challange.Domain.Services.PlayVideo.Abstract;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.BroadcastPresenter;
using Challange.Presenter.Presenters.ChallengePlayerPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class ChallengePlayerPresenterTest : TestCase
    {
        private IApplicationController controller;
        private ChallengePlayerPresenter _presenter;
        private IChallengePlayerView view;
        private IMessageParser _messageParser;
        private IVideoPlayer _videoPlayer;
        private IChallengeReader _challengeReader;
        private RewindSettings _rewindSettings;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IChallengePlayerView>();
            _messageParser = Substitute.For<IMessageParser>();
            _videoPlayer = Substitute.For<IVideoPlayer>();
            _challengeReader = Substitute.For<IChallengeReader>();
            _presenter = new ChallengePlayerPresenter(controller, view,
                                                     _messageParser, _videoPlayer,
                                                     _challengeReader);
            _rewindSettings = InitializeRewindSettings();
        }

        [Test]
        public void Run()
        {
            // Arrange
            string pathToChallenges = "path";
            // Act
            _presenter.Run(Tuple.Create(pathToChallenges, _rewindSettings));
            // Assert
            _challengeReader.Received().ReadAllChallenges(pathToChallenges);
            _videoPlayer.ReceivedWithAnyArgs().DrawAction = default(Action<Bitmap, string>);
            _videoPlayer.Received().PlayVideos(_challengeReader.Challenges);
            view.Received().Show();
        }

        [Test]
        public void OpenBroadcastFormTest()
        {
            // Arrange
            string videoName = "video";
            var video = Substitute.For<Video>(videoName, new List<Bitmap>()
            {
                new Bitmap(2,2),
                new Bitmap(3,3)
            });
            var copyOfVideo = video.Clone();
            _challengeReader.Challenges.Returns(new List<Video>() { video });
            video.Clone().Returns(copyOfVideo);
            // Act
            _presenter.OpenBroadcastForm(videoName);
            // Assert
            video.Received().Clone();
            controller.Received().Run<BroadcastPresenter,
                Tuple<object, BroadcastType>>(Tuple.Create((object)copyOfVideo,
                                                            BroadcastType.Replay));
        }

        [Test]
        public void PlayAllVideosTest()
        {
            // Arrange
            // Act
            _presenter.PlayAllVideos();
            // Assert
            _videoPlayer.Received().PlayVideos(_challengeReader.Challenges);
        }

        [Test]
        public void StartAllPlayersTest()
        {
            // Arrange
            // Act
            _presenter.StartAllPlayers();
            // Assert
            _videoPlayer.Received().StartAllPlayers();
        }

        [Test]
        public void StopAllPlayersTest()
        {
            // Arrange
            // Act
            _presenter.StopAllPlayers();
            // Assert
            _videoPlayer.Received().StopAllPlayers();
        }

        [Test]
        public void RewindBackwardTest()
        {
            // Arrange
            _challengeReader.Challenges.Returns(new List<Video>());
            _challengeReader.Challenges.Add(Substitute.For<Video>(default(string), new List<IFps>()));
            _presenter.Run(Tuple.Create("any", _rewindSettings));
            // Act
            _presenter.RewindBackward();
            // Assert
            _videoPlayer.Received().RewindBackward(_challengeReader.Challenges[0], _rewindSettings.Backward);
        }

        [Test]
        public void RewindForwardTest()
        {
            // Arrange
            _challengeReader.Challenges.Returns(new List<Video>());
            _challengeReader.Challenges.Add(Substitute.For<Video>(default(string), new List<IFps>()));
            _presenter.Run(Tuple.Create("any", _rewindSettings));
            // Act
            _presenter.RewindForward();
            // Assert
            _videoPlayer.Received().RewindForward(_challengeReader.Challenges[0], _rewindSettings.Forward);
        }

        [Test]
        public void OnFormClosingTest()
        {
            // Arrange
            // Act
            _presenter.OnFormClosing();
            // Assert
            _videoPlayer.Received().ClosePlayers();
        }

        [Test]
        public void PlaybackSpeedChangedTest()
        {
            // Arrange
            var newPlaybackSpeed = 20;
            // Act
            _presenter.PlaybackSpeedChanged(newPlaybackSpeed);
            // Assert
            _videoPlayer.Received().PlaybackSpeed = newPlaybackSpeed;
            Assert.IsTrue(_videoPlayer.PlaybackSpeed == newPlaybackSpeed);
        }
    }
}
