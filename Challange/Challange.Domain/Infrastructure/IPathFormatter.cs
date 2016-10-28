using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Infrastructure
{
    public interface IPathFormatter
    {
        string FormatPathToGameInformationFile(string directoryName,
                                                    string fileName);
    }
}
