// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

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
