using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class FpsContainer
    {
        private Dictionary<string, Fps> fpses;
        private List<string> keys;

        public FpsContainer(List<string> keys)
        {
            this.keys = keys;
            InitializeFpses();
        }

        public Dictionary<string, Fps> Fpses
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
            fpses = new Dictionary<string, Fps>();
            foreach (var key in keys)
            {
                fpses.Add(key, new Fps());
            }
        }

        public Fps GetFpsByKey(string key)
        {
            Fps fps;
            fpses.TryGetValue(key, out fps);
            return fps;
        }
    }
}
