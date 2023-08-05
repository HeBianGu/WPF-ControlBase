// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Service.Mvc
{
    public class MvcService : IMvcService
    {
        private readonly Stack<ILinkAction> _history = new Stack<ILinkAction>();
        public MvcService()
        {

        }
        internal List<Assembly> Modules = new List<Assembly>();

        public Stack<ILinkAction> History => _history;

        public void Init()
        {
            this.Modules.Clear();

            if (MvcSetting.Instance.IsUseModule && MvcSetting.Instance.ModulePath != null)
            {
                foreach (string module in MvcSetting.Instance.ModulePath)
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, module);
                    if (Directory.Exists(path))
                    {
                        string[] dlls = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
                        List<Assembly> assemblies = dlls.Select(l => Assembly.LoadFrom(l)).Where(l => l.GetCustomAttribute<ModuleAttribute>() != null).OrderBy(l => l.GetCustomAttribute<ModuleAttribute>().Order)?.ToList();
                        this.Modules.AddRange(assemblies);
                    }
                }
            }

            if (MvcSetting.Instance.IsUseApp)
            {
                this.Modules.Insert(0, Assembly.GetEntryAssembly());
            }

            this.UseAttribute<ControllerAttribute>();
            this.UseAttribute<ViewModelAttribute>();

            //  ToDo：在这实例化有问题，这块只是注册
            //if (MvcSetting.Instance.UseAuthority)
            //{
            //    var types = Assembly.GetEntryAssembly().GetAllTypeOfMatch(l => l.GetCustomAttribute<ViewModelAttribute>() != null && typeof(IAuthority).IsAssignableFrom(l));
            //    foreach (var type in types)
            //    {
            //        IAuthority authority = ServiceRegistry.Instance.GetInstance(type) as IAuthority;
            //        AuthoritySetting.Instance.Authoritys.Add(authority);
            //    }
            //}
        }

        private void UseAttribute<TAttribute>() where TAttribute : Attribute
        {
            // Do:注册模块内数据
            foreach (Assembly assembly in this.Modules)
            {
                // Do:注册应用程序集
                Type[] types = assembly.GetTypes();
                foreach (Type item in types)
                {
                    TAttribute attribute = item.GetCustomAttribute<TAttribute>();
                    if (attribute == null) continue;
                    MethodInfo mi = typeof(MvcService).GetMethod("RegisterMvc").MakeGenericMethod(item);
                    mi.Invoke(this, new object[] { });
                }
            }
        }

        public void RegisterMvc<TClass>() where TClass : class
        {
            ServiceRegistry.Instance.Register<TClass>();
        }

        public Type GetOfType(Predicate<Type> match)
        {
            foreach (Assembly module in this.Modules)
            {
                Type find = module.GetTypeOfMatch(match);
                if (find != null)
                    return find;
            }
            return null;
        }

        public List<IAction> GetLinkActions()
        {
            List<IAction> result = new List<IAction>();
            // Do:注册模块内数据
            foreach (Assembly assembly in this.Modules)
            {
                // Do:注册应用程序集
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    ControllerAttribute attribute = type.GetCustomAttribute<ControllerAttribute>();
                    if (attribute == null) continue;
                    MethodInfo[] actions = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    foreach (MethodInfo action in actions)
                    {
                        ActionAttribute actionAtt = action.GetCustomAttribute<ActionAttribute>();
                        LinkAction linkAction = new LinkAction();
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

        public LinkActionGroups GetLinkActionGroups()
        {
            LinkActionGroups result = new LinkActionGroups(true);
            // Do:注册模块内数据
            foreach (Assembly assembly in this.Modules)
            {
                // Do:注册应用程序集
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    ControllerAttribute attribute = type.GetCustomAttribute<ControllerAttribute>();
                    if (attribute == null)
                        continue;
                    MethodInfo[] actions = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                    if (actions.Count() == 0)
                        continue;

                    LinkActionGroup group = new LinkActionGroup();
                    group.DisplayName = attribute?.DisplayName ?? attribute.Name;
                    group.Logo = attribute.Logo;

                    foreach (MethodInfo action in actions)
                    {
                        ActionAttribute actionAtt = action.GetCustomAttribute<ActionAttribute>();
                        ILinkAction linkAction = new LinkAction();
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

        /// <summary>
        /// 直接从路径加载 不走Controller
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="ActionName"></param>
        /// <returns></returns>
        public IActionResult GetActionResult(string controlName, string ActionName)
        {
            ActionResult result = new ActionResult();
            result.Uri = GetUri(controlName, ActionName);
            result.View = GetView(controlName, ActionName);
            result.ViewModel = GetViewModel(controlName);
            Application.Current.Dispatcher.Invoke(() =>
            {
                (result.View as FrameworkElement).DataContext = result.ViewModel;
            });

            return result;
        }

        /// <summary>
        /// 加载指定路径视图
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="ActionName"></param>
        /// <returns></returns>
        public Control GetView(string controlName, string ActionName)
        {
            Uri uri = GetUri(controlName, ActionName);
            string path = GetPath(controlName, ActionName);
            object content = MemoryCacheService.Instance.Get(path, () =>
            {
                return Application.Current.Dispatcher.Invoke(() =>
                {
                    return Application.LoadComponent(uri);
                });
            });
            return content as Control;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="ActionName"></param>
        /// <returns></returns>
        public Uri GetUri(string controlName, string ActionName)
        {
            Assembly ass = Assembly.GetEntryAssembly();
            string path = GetPath(controlName, ActionName);
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            return uri;
        }

        public string GetPath(string controlName, string ActionName)
        {
            Assembly ass = Assembly.GetEntryAssembly();
            string path = $"/{ass.GetName().Name};component/View/{controlName}/{ActionName}Control.xaml";
            return path;
        }

        public virtual object GetViewModel(string controlName)
        {
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch(l => l.Name == controlName + "ViewModel" || l.GetCustomAttribute<ViewModelAttribute>()?.Name == controlName);
            if (type == null) return null;
            return ServiceRegistry.Instance.GetInstance(type);
        }

        public Task<IActionResult> CreateActionResult(LinkAction linkAction)
        {
            string controlName = linkAction.Controller;
            string name = linkAction.Action;
            object[] args = linkAction.Parameter;
            IController control = GetController(linkAction);
            if (control == null)
            {
                if (MvcSetting.Instance.UseAutoMap)
                {
                    IActionResult result = GetActionResult(controlName, name);
                    return Task.FromResult(result);
                }
                throw new ArgumentException($"no found controller{controlName}，add controller and addMvc() first");
            }
            return GetActionResult(control, name, args) as Task<IActionResult>;
        }

        public IController GetController(LinkAction linkAction)
        {
            string controlName = linkAction.Controller;
            if (string.IsNullOrEmpty(linkAction.FullName))
            {
                Type type = Mvc.Instance.GetOfType(l => typeof(IController).IsAssignableFrom(l) && l.Name == controlName + "Controller");
                if (type == null)
                    return null;
                return ServiceRegistry.Instance.GetInstance(type) as IController;
            }
            else
            {
                //  Do ：如果有命名空间参数匹配命名空间
                Type type = Mvc.Instance.GetOfType(l => typeof(IController).IsAssignableFrom(l) && l.Name == controlName + "Controller" && l.FullName == linkAction.FullName);
                if (type == null)
                    return null;
                return ServiceRegistry.Instance.GetInstance(type) as IController;
            }
        }

        public object GetActionResult(IController controller, string action, object[] args = null)
        {
            Type[] types = args == null ? new Type[] { } : args.Select(l => l.GetType()).ToArray();
            MethodInfo method = controller.GetType().GetMethod(action, types);
            if (method == null)
            {
                throw new ArgumentException($"在控制器中{controller?.GetType().Name}没有找到对应方法名称，请添加该方法 : {action}");
            }
            //  Do：通过反射调用指定名称的方法
            return method.Invoke(controller, args);
        }

        public Task DoActionResult(LinkAction linkAction)
        {
            IController controller = GetController(linkAction);
            MethodInfo method = controller.GetType().GetMethod(linkAction.Action);
            //  Do：通过反射调用指定名称的方法
            return controller.GetType().GetMethod(linkAction.Action).Invoke(controller, linkAction.Parameter) as Task;
        }
    }
}
