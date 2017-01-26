using AForge.Video.FFMPEG;
using Challange.Domain.Services.Video.Abstract;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Challange.Domain.Services.Video.Concrete
{
    [ExcludeFromCodeCoverage]
    public class ChallengeReader : IChallengeReader
    {
        private VideoFileReader reader;
        private List<Video> challenges;
        private FileInfo[] filesInfo;
        private int _numberOfVideos;

        public ChallengeReader()
        {
            reader = new VideoFileReader();
            challenges = new List<Video>();
        }

        public List<Video> Challenges
        {
            get
            {
                return challenges;
            }
        }

        public int NumberOfVideos
        {
            get
            {
                return _numberOfVideos;
            }
        }

        public List<Video> ReadAllChallenges(string pathToChallenges)
        {
            DirectoryInfo di = new DirectoryInfo(pathToChallenges);
            filesInfo = di.GetFiles("*.mp4", SearchOption.TopDirectoryOnly);
            List<Bitmap> frames;
            for (int i = 0; i < filesInfo.Length; i++)
            {
                reader.Open(filesInfo[i].FullName);
                frames = new List<Bitmap>();
                for (int j = 0; j < reader.FrameCount; j++)
                {
                    frames.Add(reader.ReadVideoFrame());
                }
                challenges.Add(new Video(filesInfo[i].Name, frames));
            }
            _numberOfVideos = filesInfo.Length;
            return challenges;
        }

        public List<string> GetVideoNames()
        {
            List<string> videoNames = new List<string>();
            foreach (var challenge in challenges)
            {
                videoNames.Add(challenge.Name);
            }
            return videoNames;
        }
    }
}
