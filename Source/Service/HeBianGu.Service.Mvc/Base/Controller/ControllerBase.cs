// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HeBianGu.Service.Mvc
{
    public abstract class ControllerBase : IController
    {
        protected virtual IActionResult View([CallerMemberName] string actionName = "")
        {
            IEnumerable<ControllerAttribute> route = this.GetType().GetCustomAttributes(typeof(ControllerAttribute), true).Cast<ControllerAttribute>();
            string controlName = route.FirstOrDefault() == null ? this.GetType().Name : route.FirstOrDefault().Name;
            return Mvc.Instance.GetActionResult(controlName, actionName);
        }

        protected virtual async Task<IActionResult> ViewAsync([CallerMemberName] string name = "")
        {
            return await Task.Run(() => View(name));
        }
    }
}
