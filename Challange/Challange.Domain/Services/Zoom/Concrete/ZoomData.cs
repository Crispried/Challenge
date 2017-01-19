using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Zoom.Concrete
{
    public class ZoomData
    {
        private float zoom;
        private int imgx;
        private int imgy;

        public ZoomData(float zoom, int imgx, int imgy)
        {
            this.zoom = zoom;
            this.imgx = imgx;
            this.imgy = imgy;
        }

        public float Zoom
        {
            get
            {
                return zoom;
            }
        }

        public float GetImgX
        {
            get
            {
                return imgx;
            }
        }

        public float GetImgY
        {
            get
            {
                return imgy;
            }
        }
    }
}
