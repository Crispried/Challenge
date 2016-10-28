using System.Diagnostics;
using System.IO;

namespace Challange.Domain.Infrastructure
{
    public class FileService : IFileService
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public string FilterFolderName(string name)
        {
            return name.Replace(':', '_');
        }

        public void OpenFileOrFolder(string fullName)
        {
            Process.Start(fullName);
        }
    }
}
