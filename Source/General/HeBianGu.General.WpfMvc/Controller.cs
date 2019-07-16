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

namespace HeBianGu.General.WpfMvc
{
    public abstract class Controller<T> : Controller
    {
        public T ViewModel { get; set; } = ServiceRegistry.Instance.GetInstance<T>();
    }

    public abstract class Controller : ControllerBase
    {

    }

    public abstract class ControllerBase: IController
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



            var content=  Application.Current.Dispatcher.Invoke(() =>
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
                result.View.DataContext = result.ViewModel;

            }); 

            return result;
        }

    }


  


}
