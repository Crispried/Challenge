using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

namespace Challange.Domain.Services.FileSystem.Concrete
{
    public class Archivator
    {
        public void Archivate(string sourceDirectoryName, string destinationArchiveFileName)
        {
            CreateArchiveFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }

        [ExcludeFromCodeCoverage]
        private void CreateArchiveFromDirectory(string from, string to)
        {
            try
            {
                ZipFile.CreateFromDirectory(from, to);
            }
            catch
            {
            }
        }
    }
}
