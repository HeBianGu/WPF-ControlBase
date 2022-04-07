// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Threading.Tasks;

namespace HeBianGu.Service.Mvc
{
    public class LinkAction : ActionBase, ILinkAction
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

        private string _fullName;
        /// <summary> 用于区分命名控件 为空表示不区分  </summary>
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisePropertyChanged("FullName");
            }
        }

        public override async Task<IActionResult> GetActionResult()
        {
            return await Mvc.Instance.CreateActionResult(this);
        }


        public ITransitionWipe TransitionWipe { get; set; }

    }
}
