using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class Video
    {
        private List<Fps> fpsList;
        private string name;
        private int fpsValue;

        public Video(string name, List<Fps> fpsList)
        {
            this.name = name;
            this.fpsList = fpsList;
            fpsValue = CountFps();
        }

        private int CountFps()
        {
            int sum = 0;
            foreach (var fps in fpsList)
            {
                sum += fps.Frames.Count;
            }
            return sum/fpsList.Count;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<Fps> FpsList
        {
            get
            {
                return fpsList;
            }
        }

        public int FpsValue
        {
            get
            {
                return fpsValue;
            }
        }
    }
}
