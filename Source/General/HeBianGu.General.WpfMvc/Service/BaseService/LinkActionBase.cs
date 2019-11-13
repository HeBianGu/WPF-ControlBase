using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfMvc
{
    /// <summary> 页面绑定实体基类 </summary>
    public class LinkActionBase : NotifyPropertyChanged, ILinkActionBase
    {
        private string _controller;
        /// <summary> 控制器名称  </summary>
        public string Controller
        {
            get { return _controller; }
            set
            {
                _controller = value;
                RaisePropertyChanged("Controller");
            }
        }

        private string _action;
        /// <summary> 视图名称  </summary>
        public string Action
        {
            get { return _action; }
            set
            {
                _action = value;
                RaisePropertyChanged("Action");
            }
        }

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

        /// <summary> 异步加载页面 </summary>
        public Task<IActionResult> ActionResult()
        {
            return ControllerService.CreateActionResult(this.Controller, this.Action);

        }
    }



}
