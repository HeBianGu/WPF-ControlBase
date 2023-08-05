// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 依赖注入服务 - 常用 </summary>
    public partial class ServiceRegistry
    {
        /// <summary> 注入服务 </summary>
        public void Register<TClass>() where TClass : class
        {
            this.Register<TClass>(false);
        }

        /// <summary> 注销服务器 </summary>
        public void Unregister<TClass>() where TClass : class
        {
            lock (this._syncLock)
            {
                Type serviceType = typeof(TClass);
                Type resolveTo;

                if (this._interfaceToClassMap.ContainsKey(serviceType))
                {
                    resolveTo = (this._interfaceToClassMap[serviceType] ?? serviceType);
                }
                else
                {
                    resolveTo = serviceType;
                }
                if (this._instancesRegistry.ContainsKey(serviceType))
                {
                    this._instancesRegistry.Remove(serviceType);
                }
                if (this._interfaceToClassMap.ContainsKey(serviceType))
                {
                    this._interfaceToClassMap.Remove(serviceType);
                }
                if (this._factories.ContainsKey(serviceType))
                {
                    this._factories.Remove(serviceType);
                }
                if (this._constructorInfos.ContainsKey(resolveTo))
                {
                    this._constructorInfos.Remove(resolveTo);
                }
            }
        }

        /// <summary> 获取服务 如果返回是空则表示没有注入 </summary>
        public TService GetInstance<TService>()
        {
            return (TService)this.DoGetService(typeof(TService), this._defaultKey, true);
        }

        public ILogService GetLogger()
        {
            return (ILogService)this.DoGetService(typeof(ILogService), this._defaultKey, true);
        }

        public void Register(Type interfaceType, Type classType)
        {
            //this.Register<String, String>(); 

            IEnumerable<MethodInfo> method = this.GetType().GetMethods().Where(l => l.Name == nameof(Register) && l.GetGenericArguments().Count() == 2 && l.GetParameters().Count() == 0);

            method.FirstOrDefault().MakeGenericMethod(interfaceType, classType)?.Invoke(this, null);
        }

        public IEnumerable<TService> GetAllAssignableFrom<TService>()
        {
            lock (this._factories)
            {
                foreach (KeyValuePair<Type, Dictionary<string, Delegate>> item in this._factories.Where(l => typeof(TService).IsAssignableFrom(l.Key)))
                {
                    foreach (KeyValuePair<string, Delegate> factory in item.Value)
                    {
                        yield return (TService)this.GetInstance(item.Key, factory.Key);
                    }
                }
            }

            foreach (KeyValuePair<Type, Dictionary<string, object>> item in this._instancesRegistry.Where(l => typeof(TService).IsAssignableFrom(l.Key)))
            {
                foreach (KeyValuePair<string, object> value in item.Value)
                {
                    yield return (TService)value.Value;
                }

            }
        }
    }

    /// <summary> 依赖注入服务 - 基本 </summary>
    public partial class ServiceRegistry : IServiceProvider
    {
        private readonly Dictionary<Type, ConstructorInfo> _constructorInfos = new Dictionary<Type, ConstructorInfo>();

        private readonly string _defaultKey = Guid.NewGuid().ToString();

        private readonly object[] _emptyArguments = new object[0];

        private readonly Dictionary<Type, Dictionary<string, Delegate>> _factories = new Dictionary<Type, Dictionary<string, Delegate>>();

        private readonly Dictionary<Type, Dictionary<string, object>> _instancesRegistry = new Dictionary<Type, Dictionary<string, object>>();

        private readonly Dictionary<Type, Type> _interfaceToClassMap = new Dictionary<Type, Type>();

        private readonly object _syncLock = new object();

        private static ServiceRegistry _instance;

        public static ServiceRegistry Instance
        {
            get
            {
                ServiceRegistry arg_14_0;

                if ((arg_14_0 = ServiceRegistry._instance) == null)
                {
                    arg_14_0 = (ServiceRegistry._instance = new ServiceRegistry());
                }
                return arg_14_0;
            }
        }

        public bool ContainsCreated<TClass>()
        {
            return this.ContainsCreated<TClass>(null);
        }


        public bool ContainsCreated<TClass>(string key)
        {
            Type classType = typeof(TClass);
            if (!this._instancesRegistry.ContainsKey(classType))
            {
                return false;
            }
            if (string.IsNullOrEmpty(key))
            {
                return this._instancesRegistry[classType].Count > 0;
            }
            return this._instancesRegistry[classType].ContainsKey(key);
        }

        public bool IsRegistered<T>()
        {
            Type classType = typeof(T);
            return this._interfaceToClassMap.ContainsKey(classType);
        }


        public bool IsRegistered<T>(string key)
        {
            Type classType = typeof(T);
            return this._interfaceToClassMap.ContainsKey(classType) && this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(key);
        }

        public void Register<TInterface, TClass>() where TInterface : class where TClass : class
        {
            this.Register<TInterface, TClass>(false);
        }

        public void Register<TInterface, TClass>(bool createInstanceImmediately) where TInterface : class where TClass : class
        {
            lock (this._syncLock)
            {
                Type interfaceType = typeof(TInterface);
                Type classType = typeof(TClass);
                if (this._interfaceToClassMap.ContainsKey(interfaceType))
                {
                    if (this._interfaceToClassMap[interfaceType] != classType)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is already a class registered for {0}.", new object[]
                        {
                            interfaceType.FullName
                        }));
                    }
                }
                else
                {
                    this._interfaceToClassMap.Add(interfaceType, classType);
                    this._constructorInfos.Add(classType, this.GetConstructorInfo(classType));
                }
                Func<TInterface> factory = new Func<TInterface>(this.MakeInstance<TInterface>);
                this.DoRegister<TInterface>(interfaceType, factory, this._defaultKey);
                if (createInstanceImmediately)
                {
                    this.GetInstance<TInterface>();
                }
            }
        }

        public void Register<TClass>(bool createInstanceImmediately) where TClass : class
        {
            Type classType = typeof(TClass);
            if (classType.GetTypeInfo().IsInterface)
            {
                throw new ArgumentException("An interface cannot be registered alone.");
            }
            lock (this._syncLock)
            {
                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(this._defaultKey))
                {
                    if (!this._constructorInfos.ContainsKey(classType))
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Class {0} is already registered.", new object[]
                        {
                            classType
                        }));
                    }
                }
                else
                {
                    if (!this._interfaceToClassMap.ContainsKey(classType))
                    {
                        this._interfaceToClassMap.Add(classType, null);
                    }
                    this._constructorInfos.Add(classType, this.GetConstructorInfo(classType));
                    Func<TClass> factory = new Func<TClass>(this.MakeInstance<TClass>);
                    this.DoRegister<TClass>(classType, factory, this._defaultKey);
                    if (createInstanceImmediately)
                    {
                        this.GetInstance<TClass>();
                    }
                }
            }
        }


        public void Register<TClass>(Func<TClass> factory) where TClass : class
        {
            this.Register<TClass>(factory, false);
        }


        public void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately) where TClass : class
        {
            if (factory == null)
            {
                throw new ArgumentNullException("factory");
            }
            lock (this._syncLock)
            {
                Type classType = typeof(TClass);
                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(this._defaultKey))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is already a factory registered for {0}.", new object[]
                    {
                        classType.FullName
                    }));
                }
                if (!this._interfaceToClassMap.ContainsKey(classType))
                {
                    this._interfaceToClassMap.Add(classType, null);
                }
                this.DoRegister<TClass>(classType, factory, this._defaultKey);
                if (createInstanceImmediately)
                {
                    this.GetInstance<TClass>();
                }
            }
        }


        public void Register<TClass>(Func<TClass> factory, string key) where TClass : class
        {
            this.Register<TClass>(factory, key, false);
        }


        public void Register<TClass>(Func<TClass> factory, string key, bool createInstanceImmediately) where TClass : class
        {
            lock (this._syncLock)
            {
                Type classType = typeof(TClass);
                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(key))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "There is already a factory registered for {0} with key {1}.", new object[]
                    {
                        classType.FullName,
                        key
                    }));
                }
                if (!this._interfaceToClassMap.ContainsKey(classType))
                {
                    this._interfaceToClassMap.Add(classType, null);
                }
                this.DoRegister<TClass>(classType, factory, key);
                if (createInstanceImmediately)
                {
                    this.GetInstance<TClass>(key);
                }
            }
        }


        public void Reset()
        {
            this._interfaceToClassMap.Clear();
            this._instancesRegistry.Clear();
            this._constructorInfos.Clear();
            this._factories.Clear();
        }

        public void Unregister<TClass>(TClass instance) where TClass : class
        {
            lock (this._syncLock)
            {
                Type classType = typeof(TClass);
                if (this._instancesRegistry.ContainsKey(classType))
                {
                    Dictionary<string, object> list = this._instancesRegistry[classType];
                    List<KeyValuePair<string, object>> pairs = (from pair in list
                                                                where pair.Value == instance
                                                                select pair).ToList<KeyValuePair<string, object>>();
                    for (int index = 0; index < pairs.Count<KeyValuePair<string, object>>(); index++)
                    {
                        string key = pairs[index].Key;
                        list.Remove(key);
                    }
                }
            }
        }

        public void Unregister<TClass>(string key) where TClass : class
        {
            lock (this._syncLock)
            {
                Type classType = typeof(TClass);
                if (this._instancesRegistry.ContainsKey(classType))
                {
                    Dictionary<string, object> list = this._instancesRegistry[classType];
                    List<KeyValuePair<string, object>> pairs = (from pair in list
                                                                where pair.Key == key
                                                                select pair).ToList<KeyValuePair<string, object>>();
                    for (int index = 0; index < pairs.Count<KeyValuePair<string, object>>(); index++)
                    {
                        list.Remove(pairs[index].Key);
                    }
                }
                if (this._factories.ContainsKey(classType) && this._factories[classType].ContainsKey(key))
                {
                    this._factories[classType].Remove(key);
                }
            }
        }

        private object DoGetService(Type serviceType, string key, bool cache = true)
        {
            object result;

            lock (this._syncLock)
            {
                if (string.IsNullOrEmpty(key))
                {
                    key = this._defaultKey;
                }
                Dictionary<string, object> instances = null;

                if (!this._instancesRegistry.ContainsKey(serviceType))
                {
                    if (!this._interfaceToClassMap.ContainsKey(serviceType))
                    {
                        //  Do ：修改没有注册抛出异常为返回空
                        //throw new Exception(string.Format(CultureInfo.InvariantCulture, "Type not found in cache: {0}.", new object[]
                        //{
                        //    serviceType.FullName
                        //}));

                        return null;
                    }
                    if (cache)
                    {
                        instances = new Dictionary<string, object>();
                        this._instancesRegistry.Add(serviceType, instances);
                    }
                }
                else
                {
                    instances = this._instancesRegistry[serviceType];
                }
                if (instances != null && instances.ContainsKey(key))
                {
                    result = instances[key];
                }
                else
                {
                    object instance = null;
                    if (this._factories.ContainsKey(serviceType))
                    {
                        if (this._factories[serviceType].ContainsKey(key))
                        {
                            instance = this._factories[serviceType][key].DynamicInvoke(null);
                        }
                        else
                        {
                            if (!this._factories[serviceType].ContainsKey(this._defaultKey))
                            {
                                throw new Exception(string.Format(CultureInfo.InvariantCulture, "Type not found in cache without a key: {0}", new object[]
                                {
                                    serviceType.FullName
                                }));
                            }
                            instance = this._factories[serviceType][this._defaultKey].DynamicInvoke(null);
                        }
                    }
                    if (cache && instances != null)
                    {
                        //  ToEdit ：BUG+ 20211208
                        if (!instances.ContainsKey(key))
                            instances.Add(key, instance);
                    }
                    result = instance;
                }
            }
            return result;
        }

        public void RegisterWithInstance<T>(T obj, bool force = false)
        {
            Dictionary<string, object> instances = new Dictionary<string, object>();
            instances.Add(_defaultKey, obj);

            if (force)
            {
                if (this._instancesRegistry.ContainsKey(typeof(T)))
                {
                    this._instancesRegistry.Remove(typeof(T));
                }
            }
            if (!this._instancesRegistry.ContainsKey(typeof(T)))
            {
                this._instancesRegistry.Add(typeof(T), instances);

            }
        }

        private void DoRegister<TClass>(Type classType, Func<TClass> factory, string key)
        {
            if (!this._factories.ContainsKey(classType))
            {
                Dictionary<string, Delegate> list = new Dictionary<string, Delegate>
                {
                    {
                        key,
                        factory
                    }
                };
                this._factories.Add(classType, list);
                return;
            }
            if (this._factories[classType].ContainsKey(key))
            {
                return;
            }
            this._factories[classType].Add(key, factory);
        }

        private ConstructorInfo GetConstructorInfo(Type serviceType)
        {
            Type resolveTo;
            if (this._interfaceToClassMap.ContainsKey(serviceType))
            {
                resolveTo = (this._interfaceToClassMap[serviceType] ?? serviceType);
            }
            else
            {
                resolveTo = serviceType;
            }
            ConstructorInfo[] constructorInfos = (from c in resolveTo.GetTypeInfo().DeclaredConstructors
                                                  where c.IsPublic
                                                  select c).ToArray<ConstructorInfo>();
            if (constructorInfos.Length > 1)
            {
                if (constructorInfos.Length > 2)
                {
                    return ServiceRegistry.GetPreferredConstructorInfo(constructorInfos, resolveTo);
                }
                if (constructorInfos.FirstOrDefault((ConstructorInfo i) => i.Name == ".cctor") == null)
                {
                    return ServiceRegistry.GetPreferredConstructorInfo(constructorInfos, resolveTo);
                }
                ConstructorInfo first = constructorInfos.FirstOrDefault((ConstructorInfo i) => i.Name != ".cctor");
                if (first == null || !first.IsPublic)
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture, "Cannot register: No public constructor found in {0}.", new object[]
                    {
                        resolveTo.Name
                    }));
                }
                return first;
            }
            else
            {
                if (constructorInfos.Length == 0 || (constructorInfos.Length == 1 && !constructorInfos[0].IsPublic))
                {
                    throw new Exception(string.Format(CultureInfo.InvariantCulture, "Cannot register: No public constructor found in {0}.", new object[]
                    {
                        resolveTo.Name
                    }));
                }
                return constructorInfos[0];
            }
        }

        private static ConstructorInfo GetPreferredConstructorInfo(IEnumerable<ConstructorInfo> constructorInfos, Type resolveTo)
        {
            ConstructorInfo preferredConstructorInfo = (from t in constructorInfos
                                                        let attribute = t.GetCustomAttribute(typeof(PreferredConstructorAttribute))
                                                        where attribute != null
                                                        select t).FirstOrDefault<ConstructorInfo>();
            if (preferredConstructorInfo == null)
            {
                throw new Exception(string.Format(CultureInfo.InvariantCulture, "Cannot register: Multiple constructors found in {0} but none marked with PreferredConstructor.", new object[]
                {
                    resolveTo.Name
                }));
            }
            return preferredConstructorInfo;
        }

        private TClass MakeInstance<TClass>()
        {
            Type serviceType = typeof(TClass);
            ConstructorInfo constructor = this._constructorInfos.ContainsKey(serviceType) ? this._constructorInfos[serviceType] : this.GetConstructorInfo(serviceType);
            ParameterInfo[] parameterInfos = constructor.GetParameters();
            if (parameterInfos.Length == 0)
            {
                return (TClass)constructor.Invoke(this._emptyArguments);
            }
            object[] parameters = new object[parameterInfos.Length];
            ParameterInfo[] array = parameterInfos;
            for (int i = 0; i < array.Length; i++)
            {
                ParameterInfo parameterInfo = array[i];
                parameters[parameterInfo.Position] = this.GetService(parameterInfo.ParameterType);
            }
            return (TClass)constructor.Invoke(parameters);
        }


        public IEnumerable<object> GetAllCreatedInstances(Type serviceType)
        {
            if (this._instancesRegistry.ContainsKey(serviceType))
            {
                return this._instancesRegistry[serviceType].Values;
            }
            return new List<object>();
        }


        public IEnumerable<TService> GetAllCreatedInstances<TService>()
        {
            Type serviceType = typeof(TService);
            return from instance in this.GetAllCreatedInstances(serviceType)
                   select (TService)instance;
        }



        public object GetService(Type serviceType)
        {
            return this.DoGetService(serviceType, this._defaultKey, true);
        }


        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            lock (this._factories)
            {
                if (this._factories.ContainsKey(serviceType))
                {
                    foreach (KeyValuePair<string, Delegate> factory in this._factories[serviceType])
                    {
                        this.GetInstance(serviceType, factory.Key);
                    }
                }
            }
            if (this._instancesRegistry.ContainsKey(serviceType))
            {
                return this._instancesRegistry[serviceType].Values;
            }
            return new List<object>();
        }


        public IEnumerable<TService> GetAllInstances<TService>()
        {
            Type serviceType = typeof(TService);
            return from instance in this.GetAllInstances(serviceType)
                   select (TService)instance;
        }
        public object GetInstance(Type serviceType)
        {
            return this.DoGetService(serviceType, this._defaultKey, true);
        }

        public object GetInstanceWithoutCaching(Type serviceType)
        {
            return this.DoGetService(serviceType, this._defaultKey, false);
        }


        public object GetInstance(Type serviceType, string key)
        {
            return this.DoGetService(serviceType, key, true);
        }


        public object GetInstanceWithoutCaching(Type serviceType, string key)
        {
            return this.DoGetService(serviceType, key, false);
        }

        public TService GetInstanceWithoutCaching<TService>()
        {
            return (TService)this.DoGetService(typeof(TService), this._defaultKey, false);
        }


        public TService GetInstance<TService>(string key)
        {
            return (TService)this.DoGetService(typeof(TService), key, true);
        }


        public TService GetInstanceWithoutCaching<TService>(string key)
        {
            return (TService)this.DoGetService(typeof(TService), key, false);
        }
    }

    [AttributeUsage(AttributeTargets.Constructor)]
    public sealed class PreferredConstructorAttribute : Attribute
    {

    }


}
