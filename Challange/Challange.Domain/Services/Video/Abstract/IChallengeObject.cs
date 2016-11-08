using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IChallengeObject
    {

        void Initialize(string pathToRootDirectory,
                               string challengeFolderName);
        string PathToRootDirectory { get; }

        string ChallengeFolderName { get; }

        string PathToChallengeDirectory { get; set; }
    }
}
