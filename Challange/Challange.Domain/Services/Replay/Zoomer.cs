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
    public class Zoomer
    {
        private int imgx = 0;
        private int imgy = 0;
        private float zoom = 1;
        private float minZoom = 1;

        public ZoomData MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation)
        {
            float oldzoom = zoom;

            if (MouseIsScrollingUp(delta))
            {
                CalculatePositiveZoom();
            }
            else if (MouseIsScrollingDown(delta))
            {
                CalculateNegativeZoom();
            }

            CalculateNewImageLocation(oldzoom, mouseLocation, pictureBoxLocation);

            // Return data that is necessary to make a zoom
            return new ZoomData(zoom, imgx, imgy);
        }

        private bool MouseIsScrollingUp(int delta)
        {
            return delta > 0;
        }

        private bool MouseIsScrollingDown(int delta)
        {
            return delta < 0;
        }

        private void CalculatePositiveZoom()
        {
            zoom += 0.1F;
        }

        private void CalculateNegativeZoom()
        {
            if (zoom > minZoom)
            {
                zoom = Math.Max(zoom - 0.1F, 0.01F);
            }
            else
            {
                zoom = minZoom;
            }
        }

        private Point GetPictureBoxLocation(Control redrawnControl)
        {
            Point pictureBoxLocation = new Point();
            pictureBoxLocation.X = redrawnControl.Location.X;
            pictureBoxLocation.Y = redrawnControl.Location.Y;

            return pictureBoxLocation;
        }

        private void CalculateNewImageLocation(float oldzoom, Point mouseLocation, Point pictureBoxLocation)
        {
            int x = mouseLocation.X - pictureBoxLocation.X;
            int y = mouseLocation.Y - pictureBoxLocation.Y;

            int oldimagex = (int)(x / oldzoom);
            int oldimagey = (int)(y / oldzoom);

            int newimagex = (int)(x / zoom);
            int newimagey = (int)(y / zoom);

            imgx = newimagex - oldimagex + imgx;
            imgy = newimagey - oldimagey + imgy;
        }
    }
}
