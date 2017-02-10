using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IVideoContainer
    {
        List<Concrete.Video> Videos { get; }

        IVideoContainer ConvertToVideoContainer(Dictionary<string, List<IFps>> videos);

        void AddVideo(Concrete.Video video);

        bool IsEmpty();
    }
}
