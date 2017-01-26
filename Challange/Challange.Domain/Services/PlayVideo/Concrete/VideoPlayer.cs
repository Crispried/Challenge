using Challange.Domain.Services.PlayVideo.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading;

namespace Challange.Domain.Services.PlayVideo.Concrete
{
    [ExcludeFromCodeCoverage]
    public class VideoPlayer : IVideoPlayer
    {
        private ManualResetEvent _shutdownEvent;
        private ManualResetEvent _pauseEvent;
        private Action<Bitmap, string> _drawAction;
        private List<Thread> _threads;
        private int _playbackSpeed = 30;

        public VideoPlayer()
        {
            _shutdownEvent = new ManualResetEvent(false);
            _pauseEvent = new ManualResetEvent(true);
            _threads = new List<Thread>();
        }

        public int PlaybackSpeed
        {
            get
            {
                return _playbackSpeed;
            }
            set
            {
                _playbackSpeed = value;
            }
        }

        public Action<Bitmap, string> DrawAction
        {
            get
            {
                return _drawAction;
            }
            set
            {
                _drawAction = value;
            }
        }

        public void PlayVideo(Video.Concrete.Video video)
        {
            Thread thread = new Thread(() => VideoBehaviour(video));
            _threads.Add(thread);
            thread.Start();
        }

        public void PlayVideos(List<Video.Concrete.Video> videos)
        {
            for (int i = 0; i < videos.Capacity; i++)
            {
                int capture = i;
                Thread thread = new Thread(() => VideoBehaviour(videos[capture]));
                _threads.Add(thread);
            }
            for (int i = 0; i < _threads.Count; i++)
            {
                _threads[i].Start();
            }
        }

        private void VideoBehaviour(Video.Concrete.Video video)
        {
            while (true)
            {
                _pauseEvent.WaitOne(Timeout.Infinite);

                if (_shutdownEvent.WaitOne(0))
                    break;

                if (!video.IsEnd())
                {
                    _drawAction(video.GetCurrentFrame(), video.Name);
                    video.FrameIndex++;
                }
                Thread.Sleep(_playbackSpeed);
            }
        }

        public void StartAllPlayers()
        {
            _pauseEvent.Set();
        }

        public void StopAllPlayers()
        {
            _pauseEvent.Reset();
        }

        public void ClosePlayers()
        {
            // Signal the shutdown event
            _shutdownEvent.Set();
            // Make sure to resume any paused threads
            StartAllPlayers();
            for (int i = 0; i < _threads.Count; i++)
            {
                // Wait for the thread to exit
                _threads[i].Join();
            }
        }

        public void RewindForward(Video.Concrete.Video video, int forwardValue)
        {
            if(video.FrameIndex + forwardValue >= video.Frames.Count)
            {
                video.FrameIndex = video.Frames.Count - 1;
            }
            else
            {
                video.FrameIndex += forwardValue;
            }
            _drawAction(video.Frames[video.FrameIndex], video.Name);
        }

        public void RewindBackward(Video.Concrete.Video video, int backwardValue)
        {
            if (video.FrameIndex - backwardValue < 0)
            {
                video.FrameIndex = 0;
            }
            else
            {
                video.FrameIndex -= backwardValue;
            }
            _drawAction(video.Frames[video.FrameIndex], video.Name);
        }
    }
}
