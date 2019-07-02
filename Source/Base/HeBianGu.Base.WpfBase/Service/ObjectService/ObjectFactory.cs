using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    /// <summary> 类型工厂 </summary>
    public abstract class ObjectFactory
    {
        static ObjectFactory _instance = new ObjectFactoryImpl();


        protected Dictionary<Type, Object> _objectCache = new Dictionary<Type, Object>();

        /// <summary> 获取对象实例 </summary>
        public static ObjectFactory Instance
        {
            get { return _instance; }
        }

        /// <summary> 说明 </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public T GetObject<T>(Type t, object[] args, bool isCreate)
        {
            if (isCreate)
            {
                return (T)CreateObject(t, args);
            }

            if (!_objectCache.ContainsKey(t))
            {
                _objectCache.Add(t, CreateObject(t, args));
            }

            return (T)_objectCache[t];
        }
        /// <summary> 获取单例 </summary>
        public T GetObject<T>(object[] args, bool isCreate)
        {
            return GetObject<T>(typeof(T), args, isCreate);
        }
        /// <summary> 获取单例 </summary>
        public T GetObject<T>(object[] args)
        {
            return GetObject<T>(typeof(T), args, false);
        }

        /// <summary> 重新创建 </summary>
        public T GetObject<T>(bool isCreate)
        {
            return GetObject<T>(typeof(T), null, isCreate);
        } 

        /// <summary> 获取单例 </summary>
        public T GetObject<T>()
        {
            return GetObject<T>(typeof(T), null, false);
        }

        /// <summary> 获取单例 </summary>
        private Object CreateObject(Type t, object[] args)
        {
            return Activator.CreateInstance(t, args);
        }
    }

    internal class ObjectFactoryImpl : ObjectFactory
    {

    }
}
