using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem.Abstract
{
    public interface IPathFormatter
    {
        string FormatPathToGameInformationFile(string directoryName);

        string FilterFolderName(string name);
    }
}
