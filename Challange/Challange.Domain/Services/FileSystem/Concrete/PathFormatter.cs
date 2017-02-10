using Challange.Domain.Services.FileSystem.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem.Concrete
{
    public class PathFormatter : IPathFormatter
    {
        public string FormatPathToGameInformationFile(string directoryName)
        {
            return directoryName + @"\Game_Information.xml";
        }

        public string FilterFolderName(string name)
        {
            return name.Replace(':', '_');
        }

        [ExcludeFromCodeCoverage]
        public string FormatPath(params string[] paths)
        {
            return FilterFolderName(Path.Combine(paths));
        }
    }
}
