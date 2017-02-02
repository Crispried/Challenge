﻿using Challange.Domain.Services.Zoom.Abstract;
using System.Drawing;

namespace Challange.Domain.Services.Zoom.Concrete
{
    public class Zoomer : IZoomer
    {
        private int _imgX = 0;
        private int _imgY = 0;
        private float zoom = 1.0F;
        private float _minZoom = 1.0F;
        private float _maxZoom = 3.0F;
        private IZoomCalculator zoomCalculator;

        public Zoomer(IZoomCalculator zoomCalculator)
        {
            this.zoomCalculator = zoomCalculator;
        }

        public float MinZoom
        {
            get
            {
                return _minZoom;
            }
            set
            {
                _minZoom = value;
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

        public float ImgX
        {
            get
            {
                return _imgX;
            }
        }

        public float ImgY
        {
            get
            {
                return _imgY;
            }
        }

        public ZoomData MakeZoom(Point pictureBoxLocation, int delta, Point mouseLocation)
        {
            float oldzoom = zoom;

            if(MouseIsScrollingDown(delta))
            {
                zoom = zoomCalculator.CalculateNegativeZoom(zoom, _minZoom);
            }
            else if(MouseIsScrollingUp(delta))
            {
                zoom = zoomCalculator.CalculatePositiveZoom(zoom, _maxZoom);
            }

            Point newLocation = zoomCalculator.CalculateNewImageLocation(zoom, _imgX, _imgY, oldzoom, mouseLocation, pictureBoxLocation);
            _imgX = newLocation.X;
            _imgY = newLocation.Y;

            return new ZoomData(zoom, _imgX, _imgY);
        }

        private bool MouseIsScrollingUp(int delta)
        {
            return delta > 0;
        }

        private bool MouseIsScrollingDown(int delta)
        {
            return delta < 0;
        }
    }
}
