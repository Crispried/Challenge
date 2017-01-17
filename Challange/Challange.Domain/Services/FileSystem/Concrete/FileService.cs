using Challange.Domain.Services.FileSystem.Abstract;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;

namespace Challange.Domain.Services.FileSystem.Concrete
{
    [ExcludeFromCodeCoverage]
    public class FileService : IFileService
    {
        public bool CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool OpenFileOrFolder(string fullName)
        {
            try
            {
                Process.Start(fullName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Archivate(string sourceDirectoryName, string destinationArchiveFileName)
        {
            try
            {
                ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
