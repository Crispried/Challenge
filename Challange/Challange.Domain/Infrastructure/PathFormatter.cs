using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Infrastructure
{
    public class PathFormatter : IPathFormatter
    {
        public string FormatPathToGameInformationFile(string directoryName, 
                                                    string fileName)
        {
            return directoryName + @"\Game_Information.xml";
        }
    }
}
