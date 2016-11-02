using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Challange.Domain.Services.Replay
{
    public interface IZoomCalculator
    {
        float CalculatePositiveZoom(float zoom);

        float CalculateNegativeZoom(float zoom, float minZoom);

        Point CalculateNewImageLocation(float zoom, float imgx, float imgy,
                            float oldzoom, Point mouseLocation, Point pictureBoxLocation);
    }
}
