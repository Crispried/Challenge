using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Video.Concrete;
using Challange.Domain.Services.Zoom.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter
    {
        public override void Run(Tuple<string, RewindSettings> argument)
        {
            _challengeReader.ReadAllChallenges(argument.Item1); // if return null
            // show message that there weren't found any challenges in the folder
            rewindSettings = argument.Item2;
            _videoPlayer.DrawAction = DrawAction;
            View.DrawPlayers(_challengeReader.NumberOfVideos, _challengeReader.GetVideoNames());
            PlayAllVideos(); 
            View.Show();
        }

        public void OpenBroadcastForm(string videoName)
        {
            var video = _challengeReader.Challenges.Find(vid => vid.Name == videoName);
            var copyOfVideo = video.Clone();
            Controller.Run<BroadcastPresenter.BroadcastPresenter,
                Tuple<object, BroadcastPresenter.BroadcastType>>(
                Tuple.Create((object)copyOfVideo,
                BroadcastPresenter.BroadcastType.Replay));
        }

        public void PlayAllVideos()
        {
            _videoPlayer.PlayVideos(_challengeReader.Challenges);
        }

        public void StartAllPlayers()
        {
            _videoPlayer.StartAllPlayers();
        }

        public void StopAllPlayers()
        {
            _videoPlayer.StopAllPlayers();
        }

        public void RewindBackward()
        {
            for (int i = 0; i < _challengeReader.Challenges.Count; i++)
            {
                _videoPlayer.RewindBackward(_challengeReader.Challenges[i], rewindSettings.Backward);                     
            }
        }

        public void RewindForward()
        {
            for (int i = 0; i < _challengeReader.Challenges.Count; i++)
            {
                _videoPlayer.RewindForward(_challengeReader.Challenges[i], rewindSettings.Forward);
            }
        }

        public void OnFormClosing()
        {
            _videoPlayer.ClosePlayers();
        }

        public void PlaybackSpeedChanged(int newPlaybackSpeed)
        {
            _videoPlayer.PlaybackSpeed = newPlaybackSpeed;
        }

        /// <summary>
        /// Zooms in/out replayed videos in the fullscreen mode
        /// according to the mouse position
        /// </summary>
        public void MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation)
        {
            ZoomData zoomData = _zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);
            View.SetZoomData(zoomData);
        }
    }
}
