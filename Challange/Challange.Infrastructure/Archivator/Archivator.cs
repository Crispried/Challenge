using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using Challange.Infrastructure.Exceptions;
using Challange.Infrastructure.Concrete;

namespace Challange.Infrastructure.Archivator
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
                catch (Exception e)
                {
                    throw new UnableToMakeArchiveException(e.Message);
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
