using System.Collections.Generic;
using System.Drawing;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IFps
    {
        List<Bitmap> Frames { get; }

        void AddFrame(Bitmap frame);

        void DisposeFrames();
    }
}
