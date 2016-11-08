using System.Diagnostics;
using System.IO;

namespace Challange.Domain.Services.FileSystem
{
    public class FileService : IFileService
    {
        private IProcessStarter processStarter;

        public FileService(IProcessStarter processStarter)
        {
            this.processStarter = processStarter;
        }

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
            processStarter.StartProcess(fullName);
        }
    }
}
