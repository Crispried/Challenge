using System.Collections.Generic;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IFpsContainer
    {
        Dictionary<string, IFps> Fpses { get; }

        IFps GetFpsByKey(string key);

        void RemoveFpsByKey(string key);

        void InitializeFpses(List<string> keys);
    }
}
