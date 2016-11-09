using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem
{
    [ExcludeFromCodeCoverage]
    public class ProcessStarter : IProcessStarter
    {
        public void StartProcess(string fullName)
        {
            Process.Start(fullName);
        }
    }
}
