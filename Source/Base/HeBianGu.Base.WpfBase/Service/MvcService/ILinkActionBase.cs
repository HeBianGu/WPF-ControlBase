using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 页面绑定基类接口 </summary>
    public interface ILinkActionBase
    {
        /// <summary> 跳转页面的名称 </summary>
        string Action { get; set; }

        /// <summary> 异步获取跳转的页面实体 </summary>
        Task<IActionResult> ActionResult();

        /// <summary> 跳转控制器名称 </summary>
        string Controller { get; set; }

        /// <summary> 页面显示名称 </summary>
        string DisplayName { get; set; }

        /// <summary> 页面显示图标 </summary>
        string Logo { get; set; }
    }
}