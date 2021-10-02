using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 页面绑定基类接口 </summary>
    public interface ILinkActionBase
    {
        /// <summary> 跳转页面的名称 </summary>
        string Action { get; set; }

        /// <summary> 跳转控制器名称 </summary>
        string Controller { get; set; }

        /// <summary> 页面显示名称 </summary>
        string DisplayName { get; set; }

        /// <summary> 页面显示图标 </summary>
        string Logo { get; set; }

        /// <summary>
        /// 要传递的参数
        /// </summary>
        object[] Parameter { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        string FullName { get; set; }
    }
}