using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public static class LinkActionExtention
    {
        public static Task<IActionResult> CreateActionResult(this ILinkActionBase linkAction)
        {
            string controlName = linkAction.Controller;

            string name = linkAction.Action;

            object[] args = linkAction.Parameter;

            IController control = GetController(linkAction);

            if (control == null)
            {
                throw new ArgumentException($"没有找到控制器{controlName}，请添加控制器并使用services.UseMvc()");
            }

            return GetActionResult(control, name, args) as Task<IActionResult>;
        }

        public static IController GetController(this ILinkActionBase linkAction)
        {
            //  Do：通过反射获取指定名称的Controller
            //Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<IController>(l => l.Name == controlName + "Controller");

            string controlName = linkAction.Controller;

            if (string.IsNullOrEmpty(linkAction.FullName))
            {
                Type type = MvcService.GetOfType(l => typeof(IController).IsAssignableFrom(l) && l.Name == controlName + "Controller");

                return ServiceRegistry.Instance.GetInstance(type) as IController;
            }
            else
            {
                //  Do ：如果有命名空间参数匹配命名空间
                Type type = MvcService.GetOfType(l => typeof(IController).IsAssignableFrom(l) && l.Name == controlName + "Controller" && l.FullName == linkAction.FullName);

                return ServiceRegistry.Instance.GetInstance(type) as IController;
            }
        }

        public static object GetActionResult(IController controller, string action, object[] args = null)
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

        public static object GetViewModel(string controlName)
        {
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch(l => l.Name == controlName + "ViewModel");

            return ServiceRegistry.Instance.GetInstance(type);
        }

        public static Task DoActionResult(this LinkActionBase linkAction)
        {
            IController controller = GetController(linkAction);

            MethodInfo method = controller.GetType().GetMethod(linkAction.Action);

            //  Do：通过反射调用指定名称的方法
            return controller.GetType().GetMethod(linkAction.Action).Invoke(controller, linkAction.Parameter) as Task;

        }
    }
}
