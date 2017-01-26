using AForge.Video.FFMPEG;
using System.Linq;
using Challange.Domain.Services.Video.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Challange.Domain.Services.Video.Concrete
{
    [ExcludeFromCodeCoverage]
    public class ChallengeWriter : IChallengeWriter
    {
        public void WriteChallenge(IVideoContainer videoContainer, string pathToVideos,
                                   int desiredFps = 30)
        {
            if (!videoContainer.IsEmpty())
            {
                WriteVideo(videoContainer, pathToVideos, desiredFps);
            }
        }

        private void WriteVideo(IVideoContainer videoContainer, string pathToVideos, int desiredFps)
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                string tmpFileName;
                foreach (var video in videoContainer.Videos)
                {
                    tmpFileName = pathToVideos + video.Name + ".mp4";
                    writer.Open(tmpFileName, GetWidth(videoContainer),
                        GetHeight(videoContainer), desiredFps, VideoCodec.MPEG4);
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

        private int GetWidth(IVideoContainer videoContainer)
        {
            return videoContainer.Videos.First().Frames.ElementAt(0).Width;
        }

        private int GetHeight(IVideoContainer videoContainer)
        {
            return videoContainer.Videos.First().Frames.ElementAt(0).Height;
        }
    }
}
