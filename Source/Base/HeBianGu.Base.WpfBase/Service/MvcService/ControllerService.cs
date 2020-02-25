using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 

namespace HeBianGu.Base.WpfBase
{
    public class ControllerService
    {
        public static Task<IActionResult> CreateActionResult(string controlName, string name, object[] args = null)
        {
            IController control = GetController(controlName, args);

            return GetActionResult(control, name, args) as Task<IActionResult>;
        }

        public static IController GetController(string controlName, object[] args = null)
        {
            //  Do：通过反射获取指定名称的Controller
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<IController>(l => l.Name == controlName + "Controller");

            return ServiceRegistry.Instance.GetInstance(type) as IController;
        }

        public static object GetActionResult(IController controller, string action, object[] args = null)
        {
            Type[] types = args == null ? new Type[] { } : args.Select(l => l.GetType()).ToArray();

            MethodInfo method = controller.GetType().GetMethod(action, types);

            //  Do：通过反射调用指定名称的方法
            return method.Invoke(controller, args);

        }

        public static object GetViewModel(string controlName)
        {
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch(l => l.Name == controlName + "ViewModel");

            return ServiceRegistry.Instance.GetInstance(type);
        }

        public static Task DoActionResult(string control, string action, object[] args = null)
        {
            IController controller = GetController(control);

            MethodInfo method = controller.GetType().GetMethod(action);

            //  Do：通过反射调用指定名称的方法
            return controller.GetType().GetMethod(action).Invoke(controller, args) as Task;

        }
    }
}
