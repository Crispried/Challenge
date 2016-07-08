using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Challange.Domain.Infrastructure
{
    public static class FileFormatter
    {
        public static bool FormatXml(GameInformation gameInfo, string outputPath)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(GameInformation));
            try
            {
                using (StreamWriter stringWriter = new StreamWriter(
                     outputPath))
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        try
                        {
                            xsSubmit.Serialize(writer, gameInfo);
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
    }
}
