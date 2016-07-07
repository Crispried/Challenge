using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Infrastructure.Exceptions
{
    class UnableToMakeArchiveException : Exception
    {
        public UnableToMakeArchiveException()
        {

        }

        public UnableToMakeArchiveException(string message)
        : base(message)
        {
        }
    }
}
