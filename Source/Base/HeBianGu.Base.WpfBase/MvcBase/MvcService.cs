using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public static class MvcService
    {

        static List<Assembly> modules = new List<Assembly>();

        /// <summary> 自动注入标识 RegisterMvcAttribute 特性的ViewModel项 </summary>
        public static void UseMvc(this ServiceRegistry registry, Action<MvcConfig> configAction = null)
        {
            modules.Clear();

            MvcConfig config = new MvcConfig();

            configAction?.Invoke(config);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.ModulePath);

            if (Directory.Exists(path))
            {
                var dlls = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);

                modules = dlls.Select(l => Assembly.LoadFrom(l)).Where(l => l.GetCustomAttribute<ModuleAttribute>() != null)?.ToList();

            }

            modules.Add(Assembly.GetEntryAssembly());

            registry.UseAttribute<ControllerAttribute>();

            registry.UseAttribute<ViewModelAttribute>();

            //ServiceRegistry.Instance.Register<TClass>(false);

        }


        /// <summary> 注入含有指定特性的项 </summary>
        public static void UseAttribute<TAttribute>(this ServiceRegistry registry) where TAttribute : Attribute
        {
            // Do:注册模块内数据
            foreach (var assembly in modules)
            {
                // Do:注册应用程序集
                var types = assembly.GetTypes();

                foreach (var item in types)
                {
                    var attribute = item.GetCustomAttribute<TAttribute>();

                    if (attribute == null) continue;

                    MethodInfo mi = typeof(MvcService).GetMethod("RegisterMvc").MakeGenericMethod(item);

                    mi.Invoke(null, new object[] { });
                }
            }
        }

        public static void RegisterMvc<TClass>() where TClass : class
        {
            ServiceRegistry.Instance.Register<TClass>();
        }

        public static Type GetOfType(Predicate<Type> match)
        {
            foreach (var module in modules)
            {
                var find = module.GetTypeOfMatch(match);

                if (find != null) return find;
            }

            return null;
        }

        /// <summary>
        /// 获取当前所有模块的LinkAction
        /// </summary>
        /// <returns></returns>
        public static List<ILinkActionBase> GetLinkActions()
        {
            List<ILinkActionBase> result = new List<ILinkActionBase>();

            // Do:注册模块内数据
            foreach (var assembly in modules)
            {
                // Do:注册应用程序集
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    var attribute = type.GetCustomAttribute<ControllerAttribute>();

                    if (attribute == null) continue;

                    var actions = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    foreach (var action in actions)
                    {
                        var actionAtt = action.GetCustomAttribute<ActionAttribute>();

                        LinkActionBase linkAction = new LinkActionBase();

                        linkAction.Controller = attribute.Name;

                        //  Do ：取方法名称
                        linkAction.Action = action.Name;

                        linkAction.DisplayName = actionAtt?.DisplayName ?? action.Name;

                        linkAction.FullName = type.FullName;

                        linkAction.Logo = actionAtt?.Logo;

                        result.Add(linkAction);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前所有模块的LinkActionGroups
        /// </summary>
        /// <returns></returns>
        public static LinkActionGroups GetLinkActionGroups()
        {
            LinkActionGroups result = new LinkActionGroups(true);

            // Do:注册模块内数据
            foreach (var assembly in modules)
            {
                // Do:注册应用程序集
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    var attribute = type.GetCustomAttribute<ControllerAttribute>();

                    if (attribute == null) continue;

                    var actions = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    if (actions.Count() == 0) continue;

                    LinkActionGroup group = new LinkActionGroup();

                    group.DisplayName = attribute?.DisplayName ?? attribute.Name;

                    group.Logo = attribute.Logo;

                    foreach (var action in actions)
                    {
                        var actionAtt = action.GetCustomAttribute<ActionAttribute>();

                        LinkActionBase linkAction = new LinkActionBase();

                        linkAction.Controller = attribute.Name;

                        //  Do ：取方法名称
                        linkAction.Action = action.Name;

                        linkAction.DisplayName = actionAtt?.DisplayName ?? action.Name;

                        group.Links.Add(linkAction);
                    }

                    result.Add(group);
                }
            }

            return result;
        }
    }

    public class MvcConfig
    {
        public string ModulePath { get; set; } = "Module";
    }
}
