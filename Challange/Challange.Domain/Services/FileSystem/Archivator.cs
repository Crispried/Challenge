using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace Challange.Domain.Services.FileSystem
{
    public class Archivator
    {
        public void Archivate(string sourceDirectoryName, string destinationArchiveFileName)
        {
            try
            {
                CreateArchiveFromDirectory(sourceDirectoryName, destinationArchiveFileName);
            }
            catch
            {
            }
        }

        [ExcludeFromCodeCoverage]
        private void CreateArchiveFromDirectory(string from, string to)
        {
            ZipFile.CreateFromDirectory(from, to);
        }
    }
}
