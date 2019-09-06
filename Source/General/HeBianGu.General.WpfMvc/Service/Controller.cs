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

    public abstract class Controller<M> : Controller where M : MvcViewModelBase
    {
        public M ViewModel { get; set; } = ServiceRegistry.Instance.GetInstance<M>();

        public bool ModelState(object obj, out string message)
        {
            var results = ObjectPropertyFactory.ModelState(obj);

            message = results.FirstOrDefault();

            return results.Count == 0;
        }

        public bool ModelState(object obj)
        {
            string message;

            return this.ModelState(obj, out message);
        }
    }

    public abstract class Controller : ControllerBase
    {
        public Dispatcher Dispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        public T Invoke<T>(Func<T> func)
        {
            return this.Dispatcher.Invoke(func);
        }

        public void Invoke(Action action)
        {
            this.Dispatcher.Invoke(action);
        }
    }

    public abstract class ControllerBase : IController
    {
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

            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);

            var content = Application.Current.Dispatcher.Invoke(() =>
            {
                return Application.LoadComponent(uri);
            });

            ActionResult result = new ActionResult();

            result.Uri = uri;
            result.View = content as ContentControl;

            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<NotifyPropertyChanged>(l => l.Name == controlName + "ViewModel");

            result.ViewModel = ServiceRegistry.Instance.GetInstance(type);

            Application.Current.Dispatcher.Invoke(() =>
            {
                (result.View as FrameworkElement).DataContext = result.ViewModel;

            });

            return result;
        }


        protected virtual async Task<IActionResult> ViewAsync([CallerMemberName] string name = "")
        {
            return await Task.Run(() => View(name));
        }

        //protected virtual IActionResult LinkAction([CallerMemberName] string name = "")
        //{
        //    var route = this.GetType().GetCustomAttributes(typeof(RouteAttribute), true).Cast<RouteAttribute>();

        //    string controlName = null;

        //    if (route.FirstOrDefault() == null)
        //    {
        //        controlName = this.GetType().Name;
        //    }
        //    else
        //    {
        //        controlName = route.FirstOrDefault().Name;
        //    }

        //    var ass = Assembly.GetEntryAssembly().GetName();

        //    string path = $"/{ass.Name};component/View/{controlName}/{name}Control.xaml";

        //    Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);

        //    var content = Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        return Application.LoadComponent(uri);
        //    });

        //    ActionResult result = new ActionResult();

        //    result.Uri = uri;
        //    result.View = content;

        //    Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<NotifyPropertyChanged>(l => l.Name == controlName + "ViewModel");

        //    result.ViewModel = ServiceRegistry.Instance.GetInstance(type);

        //    Application.Current.Dispatcher.Invoke(() =>
        //    {
        //        (result.View as FrameworkElement).DataContext = result.ViewModel;
        //    });

        //    return result;
        //}

    }

    /// <summary> 用于记录带有输出日志的Controller类 </summary>
    public class MvcLogControllerBase<T> : Controller<T> where T : MvcViewModelBase
    {
        IMvcLog _mvcLog = null;

        public MvcLogControllerBase(IMvcLog shareViewModel)
        {
            _mvcLog = shareViewModel;
        }

        public void RunLog(string title, string message)
        {
            _mvcLog?.RunLog(title, message);
        }

        public void ErrorLog(string title, string message)
        {
            _mvcLog?.ErrorLog(title, message);
        }

        public void OutPutLog(string title, string message)
        {
            _mvcLog?.OutPutLog(title, message);
        }

        protected override IActionResult View([CallerMemberName] string name = "")
        {
            this.OutPutLog("跳转页面", $"调用控制器{this.GetType().Name},跳转到页面{name}");

            return base.View(name);
        }
    }

    public class MvcNavigationControllerBase<T> : MvcLogControllerBase<T> where T : MvcViewModelBase
    {

        public MvcNavigationControllerBase(IMvcLog imvclog) : base(imvclog)
        {

        }

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
