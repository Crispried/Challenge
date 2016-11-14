using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace Challange.Domain.Entities
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
