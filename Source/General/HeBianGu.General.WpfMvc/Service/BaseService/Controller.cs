using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HeBianGu.General.WpfMvc
{
    /// <summary> 带有ViewMode泛型的控制器 </summary>
    public abstract class Controller<M> : Controller where M : MvcViewModelBase
    {
        public M ViewModel { get; set; } = ServiceRegistry.Instance.GetInstance<M>();

        ///// <summary> 验证实体模型是否可用 </summary>
        //public bool ModelState(object obj, out string message)
        //{
        //    var result = ObjectPropertyFactory.ModelState(obj, out List<string> errors);

        //    message = errors.FirstOrDefault();

        //    return result;
        //}

        ///// <summary> 验证实体模型是否可用 </summary>
        //public bool ModelState(object obj)
        //{
        //    string message;

        //    return this.ModelState(obj, out message);
        //}
    }

    /// <summary> 带有Dispatcher的控制器 </summary>
    public abstract class Controller : ControllerBase
    {
        public Dispatcher Dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        /// <summary> 应用主线程运行 </summary>
        public T Invoke<T>(Func<T> func)
        {
            return this.Dispatcher.Invoke(func);
        }

        /// <summary> 应用主线程运行 </summary>
        public void Invoke(Action action)
        {
            this.Dispatcher.Invoke(action);
        }

        /// <summary> 系统空闲时运行 </summary>
        public void BeginInvoke(Action action)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, action);
        }

        /// <summary> 异步运行 </summary>
        public void RunAsync(Action action)
        {
            Task.Run(action);
        }

        Dictionary<string, bool> _isInitailized = new Dictionary<string, bool>();
        /// <summary> 只运行一次的方法 </summary>
        public void RunOnlyInitailizing(Action action, [CallerMemberName] string name = "")
        {
            if (!_isInitailized.ContainsKey(name))
            {
                _isInitailized.Add(name, false);
            }

            bool isInitailized = _isInitailized[name];

            if (!isInitailized)
            {
                action?.Invoke();

                _isInitailized[name] = true;
            }
        }
    }

    /// <summary> 控制器基类 包含跳转页面的加载方式 </summary>
    public abstract class ControllerBase : IController
    {
        /// <summary> 跳转到指定页面 </summary>
        protected virtual IActionResult View([CallerMemberName] string name = "")
        {
            var route = this.GetType().GetCustomAttributes(typeof(RouteAttribute), true).Cast<RouteAttribute>();

            string controlName = null;

            if (route.FirstOrDefault() == null)
            {
                controlName = this.GetType().Name;
            }
            else
            {
                controlName = route.FirstOrDefault().Name;
            }

            var ass = Assembly.GetEntryAssembly().GetName();

            string path = $"/{ass.Name};component/View/{controlName}/{name}Control.xaml";

            //  Message：获取页面
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);

            //  Message：加载视图
            var content = MemoryCacheService.Instance.Get(path, () =>
             {
                 return Application.Current.Dispatcher.Invoke(() =>
                 {
                     return Application.LoadComponent(uri);
                 });

             });

            //var content = Application.Current.Dispatcher.Invoke(() =>
            //{
            //    return Application.LoadComponent(uri);
            //});

            ActionResult result = new ActionResult();

            result.Uri = uri;

            result.View = content as ContentControl;

            //  Message：反射获取ViewModel
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<MvcViewModelBase>(l => l.Name == controlName + "ViewModel");

            if (type != null)
            {
                result.ViewModel = ServiceRegistry.Instance.GetInstance(type);
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                (result.View as FrameworkElement).DataContext = result.ViewModel;

            });

            //  Message：返回页面实体
            return result;
        }

        /// <summary> 异步跳转页面 </summary>

        protected virtual async Task<IActionResult> ViewAsync([CallerMemberName] string name = "")
        {
            return await Task.Run(() => View(name));
        }
    }

    /// <summary> 用于记录带有输出日志的Controller类 </summary>
    public class MvcLogControllerBase<T> : Controller<T> where T : MvcViewModelBase
    {
        IMvcLog _mvcLog = null;

        /// <summary> 传入日志接口 </summary>
        public MvcLogControllerBase(IMvcLog shareViewModel)
        {
            _mvcLog = shareViewModel;
        }

        /// <summary> 运行日志 </summary>
        public void RunLog(string title, string message)
        {
            _mvcLog?.RunLog(title, message);
        }

        /// <summary> 错误日志 </summary>
        public void ErrorLog(string title, string message)
        {
            _mvcLog?.ErrorLog(title, message);
        }

        /// <summary> 输出日志 </summary>
        public void OutPutLog(string title, string message)
        {
            _mvcLog?.OutPutLog(title, message);
        }

        /// <summary> 重写跳转方法，跳转页面记录日志 </summary>
        protected override IActionResult View([CallerMemberName] string name = "")
        {
            this.OutPutLog("跳转页面", $"调用控制器{this.GetType().Name},跳转到页面{name}");

            return base.View(name);
        }
    }

    /// <summary> 带有导航跳转功能的控制器 根据导航特性划分父节点视图 </summary>
    public class MvcNavigationControllerBase<T> : MvcLogControllerBase<T> where T : MvcViewModelBase
    {

        public MvcNavigationControllerBase(IMvcLog imvclog) : base(imvclog)
        {

        }

        /// <summary> 重写跳转页面 用'/' 区分页面层级关系 如：[Route("OverView/Toggle")] 表示 </summary>
        protected override IActionResult View([CallerMemberName] string name = "")
        {
            if (this.GetType().GetMethod(name).GetCustomAttributes(typeof(RouteAttribute), true).FirstOrDefault() is RouteAttribute route)
            {
                var routes = route.Name.Split('/');

                List<ILinkActionBase> result = new List<ILinkActionBase>();

                foreach (var item in routes)
                {
                    ILinkActionBase link = this.ViewModel.GetLinkAction(item);

                    result.Add(link);
                }

                this.ViewModel.Navigation = result.ToObservable();
            }

            return base.View(name);
        }
    }






}
