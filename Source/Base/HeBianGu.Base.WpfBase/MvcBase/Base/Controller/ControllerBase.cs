using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 控制器基类 包含跳转页面的加载方式 </summary>
    public abstract class ControllerBase : IController
    {
        /// <summary> 跳转到指定页面 </summary>
        protected virtual IActionResult View([CallerMemberName] string name = "")
        {
            var route = this.GetType().GetCustomAttributes(typeof(ControllerAttribute), true).Cast<ControllerAttribute>();

            string controlName = null;

            if (route.FirstOrDefault() == null)
            {
                controlName = this.GetType().Name;
            }
            else
            {
                controlName = route.FirstOrDefault().Name;
            }

            //var ass = Assembly.GetEntryAssembly().GetName();

            var ass = this.GetType().Assembly;

            string path = $"/{ass.GetName().Name};component/View/{controlName}/{name}Control.xaml";

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
            Type type = ass.GetTypeOfMatch<MvcViewModelBase>(l => l.Name == controlName + "ViewModel");

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



}
