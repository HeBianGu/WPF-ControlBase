// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Service.Mvc;

namespace HeBianGu.App.Music.ViewModel.Custom
{
    /// <summary> 说明</summary>
    internal class CustomViewModel : MvcViewModelBase
    {
        #region - 属性 -

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
