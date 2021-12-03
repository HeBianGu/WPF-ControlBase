using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Xml;

namespace HeBianGu.Base.WpfBase
{
    public class LanguageService
    {
        public static LanguageService Instance = new LanguageService();

        public string GetConfigByKey(string key)
        {
            XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

            if (provider.Document == null) return null;

            XmlElement root = provider.Document.DocumentElement as XmlElement;

            return root.SelectSingleNode(key)?.InnerText;

        }

        public string GetMessageByCode(string code)
        {
            XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

            if (provider.Document == null) return null;

            XmlElement root = provider.Document.DocumentElement as XmlElement;

            var elements = root?.GetElementsByTagName("Message");

            foreach (XmlNode item in elements)
            {
                foreach (XmlAttribute attribute in item.Attributes)
                {
                    if (attribute.Name == "Code")
                    {
                        if (attribute.Value == code)
                        {
                            return item.InnerText;
                        }
                    }
                }
            }

            return null;
        }
    }
}
