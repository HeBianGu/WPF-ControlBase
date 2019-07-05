using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public abstract class Controller
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
                controlName = route.FirstOrDefault().Path;
            }

            var ass = Assembly.GetEntryAssembly().GetName();

            string path = $"/{ass.Name};component/View/{controlName}/{name}Control.xaml";
            string space = $"{ass.Name}.ViewModel.{controlName}ViewModel";

            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var content = Application.LoadComponent(uri);

            ActionResult result = new ActionResult();

            result.Uri = uri;
            result.View = content as ContentControl;

            var obj = Assembly.GetEntryAssembly().CreateInstance(space);

            result.ViewModel = Assembly.GetEntryAssembly().CreateInstance(space);
            result.View.DataContext = result.ViewModel;
            return result;
        }
    }

    public sealed class RouteAttribute : Attribute
    {
        public string Path { get; set; }
        public RouteAttribute(string path)
        {
            this.Path = path;
        }

    }

    public interface IActionResult
    {
        ContentControl View { get; set; }

        Uri Uri { get; set; }
        object ViewModel { get; set; }
    }

    public class ActionResult : IActionResult
    {
        public ContentControl View { get; set; }

        public Uri Uri { get; set; }

        public object ViewModel { get; set; }
    }
}
