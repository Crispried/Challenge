using Challange.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class ChallengeObject
    {
        private string pathToRootDirectory;
        private string challengeFolderName;

        public ChallengeObject(string pathToRootDirectory,
                               string challengeFolderName)
        {
            this.pathToRootDirectory = pathToRootDirectory;
            this.challengeFolderName = challengeFolderName;
        }

        public string GetChallengeDirectoryPath
        {
            get
            {
                return FormatChallengeDirectoryPath();
            }
        }

        private string FormatChallengeDirectoryPath()
        {
            return pathToRootDirectory + @"\" +
            FileService.FilterFolderName(challengeFolderName) + @"\";
        }

    }
}
