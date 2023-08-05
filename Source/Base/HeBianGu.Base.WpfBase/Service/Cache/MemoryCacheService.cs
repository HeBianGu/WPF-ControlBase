// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public class MemoryCacheService
    {
        public static MemoryCacheService Instance = new MemoryCacheService();
        private MemoryCache _cache = new MemoryCache();

        public void Store(string key, object value)
        {
            _cache.Store(key, value);
        }

        public bool HasKey(string key)
        {
            return _cache.HasKey(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public object Get(string key, Func<object> ifNoExistFunc)
        {
            if (this.HasKey(key))
            {
                return this.Get(key);
            }
            else
            {
                object result = ifNoExistFunc?.Invoke();

                if (result != null)
                {
                    _cache.Store(key, result);
                }

                return this.Get(key);
            }
        }

    }
}
