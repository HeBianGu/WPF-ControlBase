using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 用于记录带有输出日志的Controller类 </summary>
    public class MvcLogControllerBase<T> : Controller<T> where T : MvcViewModelBase
    {
        IMvcLog _mvcLog = null;

        /// <summary> 传入日志接口 </summary>
        public MvcLogControllerBase(IMvcLog shareViewModel)
        {
            _mvcLog = shareViewModel;
        }

        /// <summary> 运行日志 </summary>
        public void RunLog(string title, string message)
        {
            _mvcLog?.RunLog(title, message);
        }

        /// <summary> 错误日志 </summary>
        public void ErrorLog(string title, string message)
        {
            _mvcLog?.ErrorLog(title, message);
        }

        /// <summary> 输出日志 </summary>
        public void OutPutLog(string title, string message)
        {
            _mvcLog?.OutPutLog(title, message);
        }

        /// <summary> 重写跳转方法，跳转页面记录日志 </summary>
        protected override IActionResult View([CallerMemberName] string name = "")
        {
            this.OutPutLog("跳转页面", $"调用控制器{this.GetType().Name},跳转到页面{name}");

            return base.View(name);
        }
    }
}
