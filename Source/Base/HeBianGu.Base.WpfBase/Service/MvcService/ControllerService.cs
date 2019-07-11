using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Base.WpfBase
{
    public class ControllerService
    {
        public static async Task<IActionResult> CreateActionResult(string controlName, string name)
        {
            IController control = GetController(controlName);

            return await GetActionResult(control, name);
        }

        public static IController GetController(string controlName)
        {
            //  Do：通过反射获取指定名称的Controller
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<IController>(l => l.Name == controlName + "Controller");

            return ServiceRegistry.Instance.GetInstance(type) as IController;
        }

        public static async Task<IActionResult> GetActionResult(IController controller, string action, object[] args = null)
        {
            //  Do：通过反射调用指定名称的方法
            var from = controller.GetType().GetMethod(action).Invoke(controller, args) as Task<IActionResult>;

            return await from;

            //if (from is Task<IActionResult>)
            //{
            //    Task<IActionResult> task = from as Task<IActionResult>;
            //    return await task;
            //}
            //else if (from is IActionResult)
            //{ 
            //    return from as IActionResult;
            //} 

            //return null;
        }

        public static object GetViewModel(string controlName)
        {
            Type type = Assembly.GetEntryAssembly().GetTypeOfMatch<NotifyPropertyChanged>(l => l.Name == controlName + "ViewModel");

            return ServiceRegistry.Instance.GetInstance(type);
        }

    }
}
