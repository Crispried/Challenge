using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Challange.Domain.Infrastructure
{
    public class FileWorker : IFileWorker
    {
        public bool SerializeXml(object objectToSerialize, string outputPath)
        {
            XmlSerializer xsSubmit = new XmlSerializer(objectToSerialize.GetType());
            try
            {
                using (StreamWriter stringWriter = new StreamWriter(
                     outputPath))
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        try
                        {
                            xsSubmit.Serialize(writer, objectToSerialize);
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ObjectType DeserializeXml<ObjectType>(string settingsFilePath)
        {
            XmlSerializer serializer = new
                    XmlSerializer(typeof(ObjectType));
            FileStream fs = new FileStream(settingsFilePath,
                                            FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            ObjectType instance;
            instance = (ObjectType)serializer.Deserialize(reader);           
            fs.Close();
            return instance;
        }
    }
}
