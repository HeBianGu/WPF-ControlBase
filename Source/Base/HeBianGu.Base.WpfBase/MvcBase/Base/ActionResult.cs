using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> Mvc中页面跳转应用到的实体 </summary>
    public class ActionResult : IActionResult
    {
        /// <summary> 根据Mvc模式自动加载的视图 </summary>
        public object View { get; set; }

        /// <summary> 根据Mvc模式自动加载的Uri </summary>
        public Uri Uri { get; set; }

        /// <summary> 根据Mvc模式自动加载的ViewModel</summary>
        public object ViewModel { get; set; }
    }
}
