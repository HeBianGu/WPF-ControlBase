using System;

namespace HeBianGu.Base.WpfBase
{
    public interface IXmlSerializerService
    {
        object Load(string filePath, Type type);
        T Load<T>(string filePath);
        void Save(string filePath, object sourceObj, string xmlRootName = null);
    }
}