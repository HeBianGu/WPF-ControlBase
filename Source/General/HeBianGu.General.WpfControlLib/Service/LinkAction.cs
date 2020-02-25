using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfControlLib
{
    public class LinkAction : NotifyPropertyChanged, ILinkActionBase
    {
        private string _controller;
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
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

        public Task<IActionResult> ActionResult()
        {
            return ControllerService.CreateActionResult(this.Controller, this.Action, this.Parameter);

        }

        private string _logo;
        /// <summary> 说明  </summary>
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged("Logo");
            }
        }

        public ITransitionWipe TransitionWipe { get; set; }

    }


    /// <summary>
    /// Represents a named group of links.
    /// </summary>
    public class LinkActionGroup : NotifyPropertyChanged
    {

        private string displayName;
        /// <summary> 显示名称 </summary>
        public string DisplayName
        {
            get { return this.displayName; }
            set
            {
                if (this.displayName != value)
                {
                    this.displayName = value;
                    RaisePropertyChanged("DisplayName");
                }
            }
        }

        private ILinkActionBase selectedLink;
        public ILinkActionBase SelectedLink
        {
            get { return this.selectedLink; }
            set
            {
                if (this.selectedLink != value)
                {
                    this.selectedLink = value;
                    RaisePropertyChanged("SelectedLink");
                }
            }
        }

        private ObservableCollection<ILinkActionBase> links = new ObservableCollection<ILinkActionBase>();
        public ObservableCollection<ILinkActionBase> Links
        {
            get { return this.links; }
            set { this.links = value; }
        }

        private string _logo;
        /// <summary> 说明  </summary>
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged("Logo");
            }
        }
    }
}
