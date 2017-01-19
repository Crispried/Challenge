using AForge.Video.FFMPEG;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Challange.Domain.Services.Video.Concrete
{
    [ExcludeFromCodeCoverage]
    public class ChallengeReader
    {
        private VideoFileReader reader;
        private string pathToChallenges;
        private List<Video> challenges;
        private FileInfo[] filesInfo;

        public ChallengeReader(string pathToChallenges)
        {
            reader = new VideoFileReader();
            this.pathToChallenges = pathToChallenges;
            DirectoryInfo di = new DirectoryInfo(pathToChallenges);
            filesInfo = di.GetFiles("*.mp4", SearchOption.TopDirectoryOnly);
            challenges = new List<Video>(filesInfo.Count());
        }

        public List<Video> Challenges
        {
            get
            {
                return challenges;
            }
        }

        public List<Video> ReadAllChallenges()
        {
            List<Bitmap> frames;
            for (int i = 0; i < challenges.Capacity; i++) // fileInfo.Count == challenge.Count
            {
                reader.Open(filesInfo[i].FullName);
                frames = new List<Bitmap>();
                for (int j = 0; j < reader.FrameCount; j++)
                {
                    frames.Add(reader.ReadVideoFrame());
                }
                challenges.Add(new Video(filesInfo[i].Name, frames));
            }
            return challenges;
        }

        public Dictionary<string, Bitmap> GetInitialData()
        {
            Dictionary<string, Bitmap> result = new Dictionary<string, Bitmap>();
            DirectoryInfo di = new DirectoryInfo(pathToChallenges);
            Bitmap tmpImage;
            var filesInfo = di.GetFiles("*.mp4", SearchOption.TopDirectoryOnly);
            foreach (var fileInfo in filesInfo)
            {
                reader.Open(fileInfo.FullName);
                tmpImage = reader.ReadVideoFrame();
                result.Add(fileInfo.Name, tmpImage);
            }
            return result;
        }
    }
}
