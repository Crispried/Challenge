using System.Collections.Generic;
using Challange.Domain.Services.Video.Abstract;

namespace Challange.Domain.Services.Video.Concrete
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
            return fps == null ? new NullFps() : fps;
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
