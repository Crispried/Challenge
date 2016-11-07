using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Infrastructure
{
    public interface IProcessStarter
    {
        void StartProcess(string fullName);
    }
}
