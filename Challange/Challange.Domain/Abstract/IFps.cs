using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Challange.Domain.Abstract
{
    public interface IFps
    {
        List<Bitmap> Frames { get; }

        void AddFrame(Bitmap frame);
    }
}
