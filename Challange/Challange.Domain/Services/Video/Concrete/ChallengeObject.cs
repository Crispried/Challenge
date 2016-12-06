using Challange.Domain.Services.Video.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Concrete
{
    public class ChallengeObject : IChallengeObject
    {
        private string pathToRootDirectory;
        private string challengeFolderName;
        private string pathToChallengeDirectory;

        public void Initialize(string pathToRootDirectory,
                               string challengeFolderName)
        {
            this.pathToRootDirectory = pathToRootDirectory;
            this.challengeFolderName = challengeFolderName;
            pathToChallengeDirectory = pathToRootDirectory + @"/" + challengeFolderName.Replace(":", "_");
        }

        public string PathToRootDirectory
        {
            get
            {
                return pathToRootDirectory;
            }
        }

        public string ChallengeFolderName
        {
            get
            {
                return challengeFolderName;
            }
        }

        public string PathToChallengeDirectory
        {
            get
            {
                return pathToChallengeDirectory;
            }
            set
            {
                pathToChallengeDirectory = value;
            }
        }
    }
}
