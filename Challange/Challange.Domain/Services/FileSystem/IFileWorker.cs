using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem
{
    public interface IFileWorker
    {
        bool SerializeXml(object objectToSerialize, string outputPath);

        ObjectType DeserializeXml<ObjectType>(string settingsFilePath);
    }
}
