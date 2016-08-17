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
        private List<FPS> video;
        private int fps;
        private string fullPathToFile;

        public ChallengeWriter(List<FPS> video, string fullPathToFile)
        {
            this.video = video;
            this.fullPathToFile = fullPathToFile;
        }

        public void WriteChallenge()
        {
            fps = CountFPS();
            WriteVideo();
        }

        private int CountFPS()
        {
            int sum = 0;
            foreach (var fps in video)
            {
                sum += fps.Frames.Count;
            }
            return sum / video.Count;
        }

        private void WriteVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                writer.Open(fullPathToFile, GetWidth(), GetHeight(), fps, VideoCodec.MPEG4);
                foreach (var fps in video)
                {
                    foreach (var frame in fps.Frames)
                    {
                        writer.WriteVideoFrame(frame);
                    }
                }
            }
        }

        private int GetWidth()
        {
            return video[0].Frames[0].Width;
        }

        private int GetHeight()
        {
            return video[0].Frames[0].Height;
        }
    }
}
