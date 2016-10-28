using System.IO.Compression;

namespace Challange.Domain.Infrastructure
{
    public class Archivator
    {
        private IFileService fileService;

        public Archivator(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public bool Archivate(string sourceDirectoryName, string destinationArchiveFileName)
        {
            if (!fileService.FileExists(destinationArchiveFileName))
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

        private void CreateArchiveFromDirectory(string from, string to)
        {
            ZipFile.CreateFromDirectory(from, to);
        }
    }
}
