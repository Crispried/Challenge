using AForge.Video.FFMPEG;
using System.Linq;
using Challange.Domain.Services.Video.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Challange.Domain.Services.Video.Concrete
{
    [ExcludeFromCodeCoverage]
    public class ChallengeWriter
    {
        private IVideoContainer _videoContainer;
        private string pathToVideos;
        private int width;
        private int height;
        private int desiredFps;

        public ChallengeWriter(IVideoContainer videoContainer, string pathToVideos, int desiredFps = 30)
        {
            this.pathToVideos = pathToVideos;
            _videoContainer = videoContainer;
            this.desiredFps = desiredFps;
        }

        public void WriteChallenge()
        {
            if (!_videoContainer.IsEmpty())
            {
                WriteVideo();
            }
        }

        private void WriteVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                string tmpFileName;
                foreach (var video in _videoContainer.Videos)
                {
                    tmpFileName = pathToVideos + video.Name + ".mp4";
                    writer.Open(tmpFileName, GetWidth(),
                        GetHeight(), desiredFps, VideoCodec.MPEG4);
                    foreach (var frame in video.Frames)
                    {
                        try
                        {
                            writer.WriteVideoFrame(frame);
                        }
                        catch
                        {

                        }
                        finally
                        {
                            frame.Dispose();
                        }
                    }
                }
            }
        }

        private int GetWidth()
        {
            return _videoContainer.Videos.First().Frames.ElementAt(0).Width;
        }

        private int GetHeight()
        {
            return _videoContainer.Videos.First().Frames.ElementAt(0).Height;
        }
    }
}
