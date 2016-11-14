using AForge.Video.FFMPEG;
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

        public ChallengeReader(string pathToChallenges)
        {
            reader = new VideoFileReader();
            this.pathToChallenges = pathToChallenges;
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
