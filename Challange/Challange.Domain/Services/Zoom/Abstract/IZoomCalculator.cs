using System.Drawing;

namespace Challange.Domain.Services.Zoom.Abstract
{
    public interface IZoomCalculator
    {
        float CalculatePositiveZoom(float zoom);

        float CalculateNegativeZoom(float zoom, float minZoom);

        Point CalculateNewImageLocation(float zoom, float imgx, float imgy,
                            float oldzoom, Point mouseLocation, Point pictureBoxLocation);
    }
}
