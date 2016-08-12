using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class FPS
    {
        private List<Bitmap> frames;

        public FPS()
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
    }
}
