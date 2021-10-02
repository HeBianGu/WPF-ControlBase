using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 带有导航跳转功能的控制器 根据导航特性划分父节点视图 </summary>
    public class MvcNavigationControllerBase<T> : MvcLogControllerBase<T> where T : MvcViewModelBase
    {

        public MvcNavigationControllerBase(IMvcLog imvclog) : base(imvclog)
        {

        }

        /// <summary> 重写跳转页面 用'/' 区分页面层级关系 如：[Controller("OverView/Toggle")] 表示 </summary>
        protected override IActionResult View([CallerMemberName] string name = "")
        {
            if (this.GetType().GetMethod(name).GetCustomAttributes(typeof(ControllerAttribute), true).FirstOrDefault() is ControllerAttribute route)
            {
                var routes = route.Name.Split('/');

                List<ILinkActionBase> result = new List<ILinkActionBase>();

                foreach (var item in routes)
                {
                    ILinkActionBase link = this.ViewModel.GetLinkAction(item);

                    result.Add(link);
                }

                this.ViewModel.Navigation = result.ToObservable();
            }

            return base.View(name);
        }
    }
}
