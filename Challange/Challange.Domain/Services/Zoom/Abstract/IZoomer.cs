using Challange.Domain.Services.Zoom.Concrete;
using System.Drawing;

namespace Challange.Domain.Services.Zoom.Abstract
{
    public interface IZoomer
    {
        float MinZoom { get; set; }

        float Zoom { get; set; }

        float ImgX { get; }

        float ImgY { get; }

        ZoomData MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation);
    }
}
