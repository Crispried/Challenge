using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IChallengeWriter
    {
        void WriteChallenge(IVideoContainer videoContainer, string pathToVideos,
                                   int desiredFps = 30);
    }
}
