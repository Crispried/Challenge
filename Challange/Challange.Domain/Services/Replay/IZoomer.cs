using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Replay
{
    public interface IZoomer
    {
        ZoomData MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation);
    }
}
