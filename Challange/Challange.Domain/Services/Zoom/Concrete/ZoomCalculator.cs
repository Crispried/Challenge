using Challange.Domain.Services.Zoom.Abstract;
using System;
using System.Drawing;

namespace Challange.Domain.Services.Zoom.Concrete
{
    public class ZoomCalculator : IZoomCalculator
    {
        public float CalculatePositiveZoom(float zoom)
        {
            return zoom += 0.1F;
        }

        public float CalculateNegativeZoom(float zoom, float minZoom)
        {
            if (zoom > minZoom)
            {
                zoom = Math.Max(zoom - 0.1F, 0.01F);
            }
            else
            {
                zoom = minZoom;
            }
            return zoom;
        }

        public Point CalculateNewImageLocation(float zoom, float imgx, float imgy, 
                            float oldzoom, Point mouseLocation, Point pictureBoxLocation)
        {
            int x = mouseLocation.X - pictureBoxLocation.X;
            int y = mouseLocation.Y - pictureBoxLocation.Y;

            int oldimagex = (int)(x / oldzoom);
            int oldimagey = (int)(y / oldzoom);

            int newimagex = (int)(x / zoom);
            int newimagey = (int)(y / zoom);

            imgx = newimagex - oldimagex + imgx;
            imgy = newimagey - oldimagey + imgy;

            return new Point((int)imgx, (int)imgy);
        }
    }
}
