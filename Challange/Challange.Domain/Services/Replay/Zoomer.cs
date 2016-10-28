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
        private IZoomCalculator zoomCalculator;

        public Zoomer(IZoomCalculator zoomCalculator)
        {
            this.zoomCalculator = zoomCalculator;
        }

        public float MinZoom
        {
            get
            {
                return minZoom;
            }
            set
            {
                minZoom = value;
            }
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
            }
        }

        public float Imgx
        {
            get
            {
                return imgx;
            }
        }

        public float Imgy
        {
            get
            {
                return Imgy;
            }
        }

        //public ZoomData MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation)
        //{
        //    float oldzoom = zoom;

        //    if (MouseIsScrollingUp(delta))
        //    {
        //        CalculatePositiveZoom();
        //    }
        //    else if (MouseIsScrollingDown(delta))
        //    {
        //        CalculateNegativeZoom();
        //    }

        //    CalculateNewImageLocation(oldzoom, mouseLocation, pictureBoxLocation);

        //    // Return data that is necessary to make a zoom
        //    return new ZoomData(zoom, imgx, imgy);
        //}

        public ZoomData MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation)
        {
            float oldzoom = zoom;

            if(MouseIsScrollingDown(delta))
            {
                zoom = zoomCalculator.CalculateNegativeZoom(zoom, minZoom);
            }
            else if(MouseIsScrollingUp(delta))
            {
                zoom = zoomCalculator.CalculatePositiveZoom(zoom);
            }

            zoomCalculator.CalculateNewImageLocation(zoom, imgx, imgy, oldzoom, mouseLocation, pictureBoxLocation);

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

        private Point GetPictureBoxLocation(Control redrawnControl)
        {
            Point pictureBoxLocation = new Point();
            pictureBoxLocation.X = redrawnControl.Location.X;
            pictureBoxLocation.Y = redrawnControl.Location.Y;

            return pictureBoxLocation;
        }
    }
}
