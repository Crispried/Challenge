using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem
{
    public class PathFormatter : IPathFormatter
    {
        public string FormatPathToGameInformationFile(string directoryName)
        {
            return directoryName + @"\Game_Information.xml";
        }
    }
}
