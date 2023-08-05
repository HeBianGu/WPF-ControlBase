// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Base.WpfBase
{
    public interface IServiceCollection
    {

    }
    /// <summary> App服务管理 </summary>
    public class ServiceCollection : IServiceCollection
    {

    }

    public static class ServiceCollectionExtention
    {
        /// <summary> 注入单例模式 </summary>
        public static IServiceCollection AddSingleton<T, R>(this IServiceCollection service) where T : class where R : class
        {
            //if (ServiceRegistry.Instance.IsRegistered<T>())
            //{
            //    throw new ArgumentException($"该服务已经注册<{typeof(T)}>");
            //}

            if (ServiceRegistry.Instance.IsRegistered<T>())
            {
                Logger.Instance?.Info($"该服务已经注册 <{ typeof(T)}> ");
                return service;
            }
            ServiceRegistry.Instance.Register<T, R>();
            return service;
        }

        /// <summary> 注入单例模式 </summary>

        public static IServiceCollection AddSingleton<T>(this IServiceCollection service) where T : class
        {
            if (ServiceRegistry.Instance.IsRegistered<T>())
            {
                Logger.Instance?.Info($"该服务已经注册 <{ typeof(T)}> ");
                return service;
            }
            ServiceRegistry.Instance.Register<T>();
            return service;
        }

        /// <summary> 注入单例模式 </summary>

        public static IServiceCollection AddSingleton<T>(this IServiceCollection service, T obj, bool force = false) where T : class
        {
            //if (ServiceRegistry.Instance.IsRegistered<T>())
            //{
            //    throw new ArgumentException($"该服务已经注册<{typeof(T)}>");
            //}
            ServiceRegistry.Instance.RegisterWithInstance<T>(obj, force);
            return service;
        }

        /// <summary> 注入模式 每一次获取的对象都不是同一个 </summary>
        public static IServiceCollection AddTransient<T, R>(this IServiceCollection service) where T : class where R : class
        {
            ServiceRegistry.Instance.Register<T, R>();
            return service;
        }

        /// <summary> 注入模式 每一次获取的对象都不是同一个 </summary>
        public static IServiceCollection AddTransient<T>(this IServiceCollection service) where T : class
        {
            ServiceRegistry.Instance.Register<T>();

            return service;
        }

        /// <summary> 获取服务 </summary>
        public static T GetService<T>(this IServiceCollection service) where T : class
        {
            return ServiceRegistry.Instance.GetInstance<T>();
        }
    }
}
