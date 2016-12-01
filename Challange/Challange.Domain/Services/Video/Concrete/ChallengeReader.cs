using AForge.Video.FFMPEG;
using Challange.Domain.Abstract;
using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Servuces.Video.Concrete
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
            IFps tempFps;
            List<IFps> fpses;
            for (int i = 0; i < challenges.Capacity; i++) // fileInfo.Count == challenge.Count
            {
                reader.Open(filesInfo[i].FullName);
                fpses = new List<IFps>();
                tempFps = new Fps();
                for (int j = 0; j < reader.FrameCount; j++)
                {
                    tempFps.AddFrame(reader.ReadVideoFrame());
                }
                fpses.Add(tempFps);
                challenges.Add(new Video(filesInfo[i].Name, fpses));
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
