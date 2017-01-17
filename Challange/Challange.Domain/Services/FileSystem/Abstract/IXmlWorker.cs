using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.FileSystem.Abstract
{
    public interface IXmlWorker
    {
        bool SerializeXml(object objectToSerialize, string outputPath);

        ObjectType DeserializeXml<ObjectType>(string settingsFilePath);
    }
}
