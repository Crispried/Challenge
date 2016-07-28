using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Infrastructure;

namespace Challange.UnitTests
{
    class TestCase
    {
        protected void DeleteFile(string path)
        {
            FileService.DeleteFile(path);
        }

        protected bool FileExists(string path)
        {
            return FileService.FileExists(path);
        }
    }
}
