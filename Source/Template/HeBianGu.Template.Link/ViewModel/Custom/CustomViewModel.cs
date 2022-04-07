using HeBianGu.Service.Mvc;
using System;

namespace HeBianGu.Template.Link.ViewModel.Custom
{
    [ViewModel("Custom")]
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
