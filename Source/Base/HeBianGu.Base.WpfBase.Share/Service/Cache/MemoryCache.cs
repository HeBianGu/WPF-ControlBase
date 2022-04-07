// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Threading;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// Simple in-memory cache; used for development until enterprise caching (memcached, AppFrabric, etc) can be used
    /// </summary>
    public class MemoryCache : ICache
    {
        private static Dictionary<string, object> _list = new Dictionary<string, object>();

        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public void Remove(string key)
        {
            try
            {
                _lock.EnterWriteLock();
                _list.Remove(key);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public object Get(string key)
        {
            try
            {
                _lock.EnterReadLock();
                return _list[key];
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public object this[string key]
        {

            get
            {
                return Get(key);
            }
        }

        public T Get<T>(string key)
        {
            return (T)Get(key);
        }

        public T Get<T>(string key, T defaultValue)
        {
            if (HasKey(key) == false) return defaultValue;
            return Get<T>(key);
        }

        public bool HasKey(string key)
        {
            try
            {
                _lock.EnterReadLock();
                return _list.ContainsKey(key);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void Store(string key, object data)
        {
            try
            {
                _lock.EnterWriteLock();
                _list[key] = data;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void Flush()
        {
            try
            {
                _lock.EnterWriteLock();
                _list.Clear();
            }
            finally
            {
                _lock.EnterWriteLock();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        public void Dispose(bool isDisposing)
        {
            if (isDisposing == true)
            {
                try
                {
                    _lock.EnterWriteLock();
                    foreach (KeyValuePair<string, object> kvp in _list)
                    {
                        try
                        {
                            if (kvp.Value is IDisposable)
                            {
                                ((IDisposable)kvp.Value).Dispose();
                            }
                        }
                        catch
                        {  //ignore disposing exceptions

                        }
                    }
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                _lock.Dispose();

                GC.SuppressFinalize(this);
            }

        }
        ~MemoryCache()
        {
            Dispose(false);
        }
    }
}
