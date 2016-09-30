using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;

namespace Challange.Domain.Entities
{
    public class FpsContainer
    {
        private Dictionary<string, IFps> fpses;
        private List<string> keys;

        public FpsContainer(List<string> keys)
        {
            this.keys = keys;
            InitializeFpses();
        }

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

        private void InitializeFpses()
        {
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
    }
}
