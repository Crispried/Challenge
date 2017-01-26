using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IChallengeReader
    {
        List<Concrete.Video> Challenges { get; }

        int NumberOfVideos { get; }

        List<Concrete.Video> ReadAllChallenges(string pathToChallenges);

        List<string> GetVideoNames();
    }
}
