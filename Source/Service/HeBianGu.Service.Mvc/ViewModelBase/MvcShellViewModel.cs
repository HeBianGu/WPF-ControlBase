// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.Service.Mvc
{
    /// <summary> 说明</summary>
    public class MvcShellViewModel : NotifyPropertyChanged
    {
        #region - 属性 - 

        private ObservableCollection<IAction> _linkActions = new ObservableCollection<IAction>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IAction> LinkActions
        {
            get { return _linkActions; }
            set
            {
                _linkActions = value;
                RaisePropertyChanged();
            }
        }


        private IAction _currentLink;
        /// <summary> 说明  </summary>
        public IAction CurrentLink
        {
            get { return _currentLink; }
            set
            {
                _currentLink = value;
                RaisePropertyChanged();
            }
        }


        private LinkActionGroups _linkActionGroups = new LinkActionGroups(true);
        /// <summary> 说明  </summary>
        public LinkActionGroups LinkActionGroups
        {
            get { return _linkActionGroups; }
            set
            {
                _linkActionGroups = value;
                RaisePropertyChanged("LinkActionGroups");
            }
        }


        protected override void Init()
        {
            base.Init();

            this.LinkActions = Mvc.Instance.GetLinkActions()?.ToObservable();

            if (this.LinkActions.Count > 0)
            {
                this.CurrentLink = this.LinkActions?.FirstOrDefault();
                return;
            }

            this.LinkActionGroups = Mvc.Instance.GetLinkActionGroups();

            if (this.LinkActionGroups.Count > 0)
            {
                this.CurrentLink = this.LinkActionGroups?.FirstOrDefault()?.Links?.FirstOrDefault();
                return;
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion
    }

}
