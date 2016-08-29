using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class Video
    {
        private List<FPS> fpsList;
        private int fpsValue;

        public Video(List<FPS> fpsList)
        {
            this.fpsList = fpsList;
        }
    }
}
