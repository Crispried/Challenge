using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using Challange.Domain.Abstract;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IFpsContainer
    {
        Dictionary<string, IFps> Fpses { get; }

        List<string> GetKeys { get; }

        IFps GetFpsByKey(string key);

        void RemoveFpsByKey(string key);
    }
}
