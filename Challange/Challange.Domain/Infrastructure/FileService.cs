using System.Diagnostics;
using System.IO;

namespace Challange.Domain.Infrastructure
{
    public static class FileService
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static string FilterFolderName(string name)
        {
            return name.Replace(':', '_');
        }

        public static void OpenFileOrFolder(string fullName)
        {
            Process.Start(fullName);
        }
    }
}
