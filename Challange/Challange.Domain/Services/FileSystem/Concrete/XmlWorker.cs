using Challange.Domain.Services.FileSystem.Abstract;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Challange.Domain.Services.FileSystem.Concrete
{
    [ExcludeFromCodeCoverage]
    public class XmlWorker : IXmlWorker
    {
        public bool SerializeXml(object objectToSerialize, string outputPath)
        {
            try
            {
                XmlSerializer xsSubmit = new XmlSerializer(objectToSerialize.GetType());
                using (StreamWriter stringWriter = new StreamWriter(
                     outputPath))
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        xsSubmit.Serialize(writer, objectToSerialize);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ObjectType DeserializeXml<ObjectType>(string settingsFilePath)
        {
            ObjectType instance = default(ObjectType);
            try
            {
                XmlSerializer serializer = new
                    XmlSerializer(typeof(ObjectType));
                FileStream fs = new FileStream(settingsFilePath,
                                                FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                
                instance = (ObjectType)serializer.Deserialize(reader);
                fs.Close();
            }
            catch
            {

            }
            return instance;
        }
    }
}
