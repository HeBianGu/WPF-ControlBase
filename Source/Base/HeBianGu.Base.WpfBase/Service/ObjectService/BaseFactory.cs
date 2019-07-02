
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    /// <summary> 单例工厂父类</summary>
    public abstract class BaseFactory<T> : IDisposableBF where T : class,new()
    {

        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static T t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static T Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new T();
                    }
                }
                return t;
            }
        }

        #endregion - 单例模式 End -

        #region - Start 多例模式 -

        /// <summary> 多例模式 </summary>
        static Dictionary<string, T> cache = null;

        /// <summary> 通过名称得到多例实例 </summary>
        public static T InstanceByName(string strKey)
        {
            lock (localLock)
            {
                if (cache == null)
                    cache = new Dictionary<string, T>();

                if (!cache.ContainsKey(strKey))
                    cache.Add(strKey, new T());

                return cache[strKey];
            }

        }

        #endregion - 多例模式 End -

        /// <summary> 外部不可以构造 </summary>
        protected BaseFactory()
        { }

        #region - 资源清理 -

        /// <summary> 供程序员显式调用的Dispose方法 </summary>
        public void Dispose()
        {
            //  调用带参数的Dispose方法，释放托管和非托管资源
            Dispose(true);

            //  手动调用了Dispose释放资源，那么析构函数就是不必要的了，这里阻止GC调用析构函数
            System.GC.SuppressFinalize(this);
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                ///TODO:在这里加入清理"托管资源"的代码，应该是xxx.Dispose();
                t = null;
            }
            ///TODO:在这里加入清理"非托管资源"的代码
        }

        /// <summary> 清理指定名称的缓存和单例 </summary>
        public void Dispose(string name)
        {
            Dispose(true);

            if (cache != null && cache.ContainsKey(name))
            {
                cache.Remove(name);
            }
        }

        /// <summary> 清理指定名称的缓存和单例 </summary>
        public static void Dispose(Predicate<KeyValuePair<string, T>> macth)
        {
            foreach (KeyValuePair<string, T> v in cache)
            {
                if (macth(v))
                {
                    cache.Remove(v.Key);
                }
            }
        }

        /// <summary> 供GC调用的析构函数 </summary>
        ~BaseFactory()
        {
            //  释放非托管资源
            Dispose(false);
        }

        #endregion

    }

    public interface IDisposableBF : IDisposable
    {
        void Dispose(string name);
    }
}
