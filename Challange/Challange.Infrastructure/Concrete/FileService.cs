using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Challange.Infrastructure.Concrete
{
    public static class FileService
    {
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}
