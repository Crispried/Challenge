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
        private Dictionary<string, List<FPS>> videos;
        private Dictionary<string, int> videosFps;
        private string fullPathToFile;

        public ChallengeWriter(Dictionary<string, List<FPS>> videos, string fullPathToFile)
        {
            this.videos = videos;
            this.fullPathToFile = fullPathToFile;
        }

        public void WriteChallenge()
        {
            videosFps = CountFPS();
            WriteVideo();
        }

        private Dictionary<string, int> CountFPS()
        {
            var result = new Dictionary<string, int>();
            int tmpSum = 0;
            foreach (var video in videos)
            {
                foreach (var fps in video.Value)
                {
                    tmpSum += fps.Frames.Count;
                }          
                result.Add(video.Key, tmpSum / video.Value.Count);
                tmpSum = 0;
            }
            return result;
        }

        private void WriteVideo()
        {
            using (VideoFileWriter writer = new VideoFileWriter())
            {
                foreach (var video in videos)
                {
                    
                }
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
