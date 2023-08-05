// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace HeBianGu.Systems.XmlSerialize
{
    public class XmlSystemSerializer : ISerializerService
    {
        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            string folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                            new XmlSerializer(sourceObj.GetType()) :
                            new XmlSerializer(sourceObj.GetType(), new XmlRootAttribute(xmlRootName));
                        xmlSerializer.Serialize(writer, sourceObj);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance?.Error(ex);
#if DEBUG
                Trace.Assert(false);
                throw ex;
#endif
            }
        }

        public T Load<T>(string filePath)
        {
            return (T)this.Load(filePath, typeof(T));
        }

        public object Load(string filePath, Type type)
        {
            if (!File.Exists(filePath)) return null;

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    return xmlSerializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance?.Error(ex);
                //Trace.Assert(false);
                File.Delete(filePath);
            }

            return null;
        }

        public object CloneXml(object realObject)
        {
            using (Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(realObject.GetType());
                serializer.Serialize(stream, realObject);
                stream.Seek(0, SeekOrigin.Begin);
                return serializer.Deserialize(stream);
            }
        }
    }
}
