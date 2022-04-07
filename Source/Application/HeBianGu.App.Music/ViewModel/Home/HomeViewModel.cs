// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;

namespace HeBianGu.App.Music.ViewModel.Home
{
    [ViewModel("Home")]
    internal class HomeViewModel : MvcViewModelBase
    {
        #region - 属性 -

        private IAction _selectedAction;
        /// <summary> 说明  </summary>
        public IAction SelectedAction
        {
            get { return _selectedAction; }
            set
            {
                _selectedAction = value;
                RaisePropertyChanged();
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
