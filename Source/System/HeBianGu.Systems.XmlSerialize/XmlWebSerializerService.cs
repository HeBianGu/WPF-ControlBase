// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace HeBianGu.Systems.XmlSerialize
{
    public class XmlWebSerializerService : IWebSerializerService
    {
        public T Load<T>(string url, out string message)
        {
            message = null;
            Uri uri = new Uri(url);
            using (WebClient client = new WebClient())
            {
                try
                {
                    string xml = client.DownloadString(uri);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(new StringReader(xml)) { XmlResolver = null };
                    return (T)xmlSerializer.Deserialize(xmlTextReader);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Logger.Instance?.Error(ex);
                }
            }

            return default(T);
        }
    }
}
