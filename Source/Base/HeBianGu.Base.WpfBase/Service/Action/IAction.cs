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

    public abstract class ActionBase : DisplayerViewModelBase, IAction, IAuthority
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged();
            }
        }

        private string _logo;
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged();
            }
        }

        private object[] _parameter;
        public object[] Parameter
        {
            get { return _parameter; }
            set
            {
                _parameter = value;
                RaisePropertyChanged();
            }
        }

        private object _tag;
        public object Tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                RaisePropertyChanged();
            }
        }


        public abstract Task<IActionResult> GetActionResult();
        public bool IsAuthority => Loginner.Instance?.User?.IsValid(this.ID) != false;
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
