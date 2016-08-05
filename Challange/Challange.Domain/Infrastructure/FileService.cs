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
    }
}
