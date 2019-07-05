using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
   public static class ControllerFactory
    {

        static Dictionary<string, Object> _objectCache = new Dictionary<string, Object>();

        public static IActionResult CreateActionResult(string controlName, string name)
        {
            var ass = Assembly.GetEntryAssembly().GetName();

            string path = $"/{ass.Name};component/View/{controlName}/{name}Control.xaml";
            string space = $"{ass.Name}.ViewModel.{controlName}ViewModel";

            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var content = Application.LoadComponent(uri);

            ActionResult result = new ActionResult();

            result.Uri = uri;
            result.View = content as ContentControl;

           if( !_objectCache.Keys.Contains(space))
            {
                var obj = Assembly.GetEntryAssembly().CreateInstance(space);
                _objectCache.Add(space, obj);
            }

            result.ViewModel = _objectCache[space];


            result.View.DataContext = result.ViewModel;
            return result;
        }
    }
}
