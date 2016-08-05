using System.IO.Compression;

namespace Challange.Domain.Infrastructure
{
    public static class Archivator
    {
        public static bool Archivate(string sourceDirectoryName, string destinationArchiveFileName)
        {
            if (!FileService.FileExists(destinationArchiveFileName))
            {
                try
                {
                    CreateArchiveFromDirectory(sourceDirectoryName, destinationArchiveFileName);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private static void CreateArchiveFromDirectory(string from, string to)
        {
            ZipFile.CreateFromDirectory(from, to);
        }
    }
}
