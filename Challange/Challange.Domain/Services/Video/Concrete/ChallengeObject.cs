using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Servuces.Video.Concrete
{
    public class ChallengeObject
    {
        private string pathToRootDirectory;
        private string challengeFolderName;
        private string pathToChallengeDirectory;

        public ChallengeObject(string pathToRootDirectory,
                               string challengeFolderName)
        {
            this.pathToRootDirectory = pathToRootDirectory;
            this.challengeFolderName = challengeFolderName;
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

        //public string GetChallengeDirectoryPath
        //{
        //    get
        //    {
        //        return FormatChallengeDirectoryPath();
        //    }
        //}

        //public void CreateDirectoryForChallenge()
        //{
        //    FileService.CreateDirectory(GetChallengeDirectoryPath);
        //}

        //private string FormatChallengeDirectoryPath()
        //{
        //    return pathToRootDirectory + @"\" +
        //        FileService.FilterFolderName(challengeFolderName) + @"\";
        //}

    }
}
