using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;
using Challange.Domain.Services.Video.Abstract;

namespace Challange.Domain.Services.Video.Concrete
{
    public class Fps : IFps
    {
        private List<Bitmap> frames;

        public Fps()
        {
            frames = new List<Bitmap>();
        }

        public List<Bitmap> Frames
        {
            get
            {
                return frames;
            }
        }

        public void AddFrame(Bitmap frame)
        {
            frames.Add(frame);
        }

        public void DisposeFrames()
        {
            foreach (var frame in frames)
            {
                frame.Dispose();
            }
        }
    }

    [ExcludeFromCodeCoverage]
    public class NullFps : IFps
    {
        public List<Bitmap> Frames
        {
            get
            {
                return null;
            }
        }

        public void AddFrame(Bitmap frame)
        {

        }

        public void DisposeFrames()
        {

        }
    }
}
