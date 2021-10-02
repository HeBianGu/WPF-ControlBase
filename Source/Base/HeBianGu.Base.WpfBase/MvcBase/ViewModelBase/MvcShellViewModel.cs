using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 说明</summary>
    public class MvcShellViewModel : NotifyPropertyChanged
    {
        #region - 属性 -

        private ObservableCollection<ILinkActionBase> _linkActions = new ObservableCollection<ILinkActionBase>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ILinkActionBase> LinkActions
        {
            get { return _linkActions; }
            set
            {
                _linkActions = value;
                RaisePropertyChanged("LinkActions");
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

            this.LinkActions = MvcService.GetLinkActions()?.ToObservable();

            this.LinkActionGroups = MvcService.GetLinkActionGroups();
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
