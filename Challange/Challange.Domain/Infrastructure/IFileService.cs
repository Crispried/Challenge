using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Infrastructure
{
    public interface IFileService
    {
        bool FileExists(string path);

        void DeleteFile(string path);

        void CreateDirectory(string path);

        string FilterFolderName(string name);

        void OpenFileOrFolder(string fullName);
    }
}
