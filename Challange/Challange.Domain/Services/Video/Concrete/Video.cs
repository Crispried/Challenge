using Challange.Domain.Services.Video.Abstract;
using System.Collections.Generic;
using System.Drawing;

namespace Challange.Domain.Services.Video.Concrete
{
    public class Video
    {
        private List<IFps> fpses;
        private List<Bitmap> frames;
        private string name;

        public Video(string name, List<IFps> fpses)
        {
            this.name = name;
            this.fpses = fpses;
            frames = GetAllVideoFrames();
        }

        public Video(string name, List<Bitmap> frames)
        {
            this.name = name;
            this.frames = frames;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<Bitmap> Frames
        {
            get
            {
                return frames;
            }
        }

        public int FrameIndex { get; set; }

        private List<Bitmap> GetAllVideoFrames()
        {
            List<Bitmap> result = new List<Bitmap>();
            foreach (var fps in fpses)
            {
                result.AddRange(fps.Frames);
            }
            return result;
        }

        public bool IsEnd()
        {
            return FrameIndex == frames.Count;
        }
    }
}
