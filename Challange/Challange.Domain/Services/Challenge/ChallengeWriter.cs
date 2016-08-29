using AForge.Video.FFMPEG;
using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Challenge
{
    public class ChallengeWriter
    {
        private List<Video> videos;
        private string pathToVideos;

        public ChallengeWriter(List<Video> videos, string pathToVideos)
        {
            this.videos = videos;
            this.pathToVideos = pathToVideos;
        }

        public void WriteChallenge()
        {
            WriteVideo();
        }

        private void WriteVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                foreach (var video in videos)
                {
                    writer.Open(pathToVideos+video.Name+".mp4", GetWidth(video),
                        GetHeight(video), video.FpsValue, VideoCodec.MPEG4);
                    foreach (var fps in video.FpsList)
                    {
                        foreach (var frame in fps.Frames)
                        {
                            writer.WriteVideoFrame(frame);
                        }
                    }
                }
            }
        }

        private int GetWidth(Video video)
        {
            return video.FpsList[0].Frames[0].Width;
        }

        private int GetHeight(Video video)
        {
            return video.FpsList[0].Frames[0].Height;
        }
    }
}
