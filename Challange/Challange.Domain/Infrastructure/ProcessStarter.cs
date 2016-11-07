using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Infrastructure
{
    public class ProcessStarter : IProcessStarter
    {
        public void StartProcess(string fullName)
        {
            Process.Start(fullName);
        }
    }
}
