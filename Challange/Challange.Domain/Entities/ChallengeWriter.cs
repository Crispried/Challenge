using AForge.Video.FFMPEG;
using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class ChallengeWriter
    {
        private List<Video> videos;
        private string pathToVideos;
        private int width;
        private int height;

        public ChallengeWriter(List<Video> videos, string pathToVideos)
        {
            this.videos = videos;
            this.pathToVideos = pathToVideos;
            this.width = GetWidth();
            this.height = GetHeight();
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
                        height, video.FpsValue, VideoCodec.MPEG4);
                    foreach (var fps in video.FpsList)
                    {
                        foreach (var frame in fps.Frames)
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
        }

        private int GetWidth()
        {
            return videos.First().FpsList[0].Frames[0].Width;
        }

        private int GetHeight()
        {
            return videos.First().FpsList[0].Frames[0].Height;
        }
    }
}
