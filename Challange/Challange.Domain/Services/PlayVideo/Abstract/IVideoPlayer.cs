using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.PlayVideo.Abstract
{
    public interface IVideoPlayer
    {
        int PlaybackSpeed { get; set; }

        Action<Bitmap, string> DrawAction { get; set; }

        void PlayVideo(Video.Concrete.Video video);

        void PlayVideos(List<Video.Concrete.Video> videos);

        void StartAllPlayers();

        void StopAllPlayers();

        void ClosePlayers();

        void RewindForward(Video.Concrete.Video video, int forwardValue);

        void RewindBackward(Video.Concrete.Video video, int backwardValue);
    }
}
