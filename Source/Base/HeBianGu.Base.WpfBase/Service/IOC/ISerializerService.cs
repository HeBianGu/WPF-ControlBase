// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface ISerializerService
    {
        object Load(string filePath, Type type);
        T Load<T>(string filePath);
        void Save(string filePath, object sourceObj, string xmlRootName = null);
        object CloneXml(object o);
    }

    public class XmlSerialize : ServiceInstance<ISerializerService>
    {

    }

    public interface IWebSerializerService
    {
        T Load<T>(string uri, out string message);
    }

    public class XmlWebSerializer : ServiceInstance<IWebSerializerService>
    {

    }
}