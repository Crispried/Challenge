using Challange.Domain.Abstract;
using Challange.Domain.Services.Video.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: ChallengeReader provides List<Videos> 
namespace Challange.Domain.Servuces.Video.Concrete
{
    public class Video
    {
        private List<IFps> fpses;
        private string name;
        private int fpsValue;

        public Video(string name, List<IFps> fpses)
        {
            this.name = name;
            this.fpses = fpses;
            fpsValue = CountFps();
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public List<IFps> Fpses
        {
            get
            {
                return fpses;
            }
        }

        /// <summary>
        /// frames per second value
        /// </summary>
        public int FpsValue
        {
            get
            {
                return fpsValue;
            }
        }

        private int CountFps()
        {
            int sum = 0;
            foreach (var fps in fpses)
            {
                sum += fps.Frames.Count;
            }
            return sum/fpses.Count;
        }
    }
}
