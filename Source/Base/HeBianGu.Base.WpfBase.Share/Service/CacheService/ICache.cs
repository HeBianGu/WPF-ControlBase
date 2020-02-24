using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public interface ICache : IDisposable
    {

        void Remove(string key);
        object Get(string key);
        T Get<T>(string key);
        T Get<T>(string key, T defaultValue);
        bool HasKey(string key);
        void Store(string key, object data);
        //void Store(string key, object data, DateTime expiryTime);
        void Flush();
        object this[string key] { get; }
    }
}
