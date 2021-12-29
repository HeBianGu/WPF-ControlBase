using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ServiceRegistry.Instance.Register<T, R>();

            return service;
        }

        /// <summary> 注入单例模式 </summary>

        public static IServiceCollection AddSingleton<T>(this IServiceCollection service) where T : class
        {
            ServiceRegistry.Instance.Register<T>();
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
          return  ServiceRegistry.Instance.GetInstance<T>();
        }
    }
}
