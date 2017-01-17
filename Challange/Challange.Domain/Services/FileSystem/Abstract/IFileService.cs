using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem.Abstract
{
    public interface IFileService
    {
        bool CreateDirectory(string path);

        bool OpenFileOrFolder(string fullName);

        bool Archivate(string sourceDirectoryName, string destinationArchiveFileName);
    }
}
