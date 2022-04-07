// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public interface IAction
    {
        /// <summary> 页面显示名称 </summary>
        string DisplayName { get; set; }

        /// <summary> 页面显示图标 </summary>
        string Logo { get; set; }

        Task<IActionResult> GetActionResult();
    }

    public interface IActionResult
    {
        object View { get; set; }

        Uri Uri { get; set; }

        object ViewModel { get; set; }
    }

    public abstract class ActionBase : NotifyPropertyChangedBase, IAction
    {
        private string _displayName;
        /// <summary> 显示名称  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }

        private string _logo;
        /// <summary> 图标  </summary>
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged("Logo");
            }
        }

        private object[] _parameter;
        /// <summary> 传递的参数  </summary>
        public object[] Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                RaisePropertyChanged("Parameter");
            }
        }

        public abstract Task<IActionResult> GetActionResult();
    }

    public class NofoundAction : ActionBase
    {
        public override async Task<IActionResult> GetActionResult()
        {
            ActionResult result = new ActionResult();
            result.View = new TextBlock() { Text = "NotFound" };
            return await Task.FromResult(result);
        }
    }

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
