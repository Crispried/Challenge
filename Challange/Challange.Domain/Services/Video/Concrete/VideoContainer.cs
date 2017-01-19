using Challange.Domain.Services.Video.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Concrete
{
    public class VideoContainer : IVideoContainer
    {
        private List<Video> _videos;

        public VideoContainer()
        {
            _videos = new List<Video>();
        }

        public List<Video> Videos
        {
            get
            {
                return _videos;
            }
        }

        public IVideoContainer ConvertToVideoContainer(IChallengeBuffers challengeBuffers)
        {
            var videos = challengeBuffers.UniteBuffers();
            var videoContainer = new VideoContainer();
            foreach (var video in videos)
            {
                videoContainer.AddVideo(new Video(video.Key, video.Value));
            }
            return videoContainer;
        }

        public void AddVideo(Video video)
        {
            _videos.Add(video);
        }

        public bool IsEmpty()
        {
            return _videos.Count == 0 ? true : false;
        }
    }
}
