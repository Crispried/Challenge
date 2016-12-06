using AForge.Video.FFMPEG;
using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Video.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Challange.Domain.Services.Video.Concrete
{
    [ExcludeFromCodeCoverage]
    public class ChallengeWriter
    {
        private List<Video> videos;
        private string pathToVideos;
        private int width;
        private int height;
        private int desiredFps;

        public ChallengeWriter(List<Video> videos, string pathToVideos, int desiredFps = 30)
        {
            this.pathToVideos = pathToVideos;
            this.videos = videos;
            this.desiredFps = desiredFps;
            if(videos.Count != 0)
            {
                width = GetWidth();
                height = GetHeight();
            }
        }

        public void WriteChallenge()
        {
            WriteVideo();
        }

        private void WriteVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                string tmpFileName;
                foreach (var video in videos)
                {
                    tmpFileName = pathToVideos + video.Name + ".mp4";
                    writer.Open(tmpFileName, width,
                        height, desiredFps, VideoCodec.MPEG4);
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
            return videos.First().Frames.ElementAt(0).Width;
        }

        private int GetHeight()
        {
            return videos.First().Frames.ElementAt(0).Height;
        }
    }
}
