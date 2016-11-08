using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;
using Challange.Domain.Entities;
using Challange.Domain.Services.Video.Abstract;

namespace Challange.Domain.Servuces.Video.Concrete
{
    public class FpsContainer : IFpsContainer
    {
        private Dictionary<string, IFps> fpses;
        private List<string> keys;

        public Dictionary<string, IFps> Fpses
        {
            get
            {
                return fpses;
            }
        }

        public List<string> GetKeys
        {
            get
            {
                return keys;
            }
        }

        public void InitializeFpses(List<string> keys)
        {
            this.keys = keys;
            fpses = new Dictionary<string, IFps>();
            foreach (var key in keys)
            {
                fpses.Add(key, new Fps());
            }
        }

        public IFps GetFpsByKey(string key)
        {
            IFps fps;
            fpses.TryGetValue(key, out fps);

            if(fps == null)
            {
                return new NullFps();
            }

            return fps;
        }

        public void RemoveFpsByKey(string key)
        {
            IFps fps;
            fpses.TryGetValue(key, out fps);

            if (fps != null)
            {
                fpses.Remove(key);
            }
        }
    }
}
