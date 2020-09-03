using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Common.LocalConfig
{
    public class XmlProvider
    {
        public void Save<T>(string filePath, T sourceObj, string xmlRootName = null)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            string folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                        new XmlSerializer(typeof(T)) :
                        new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
        }

        public T Load<T>(string filePath)
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return (T)result;
        }
    }
}
