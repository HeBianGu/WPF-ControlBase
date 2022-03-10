using HeBianGu.App.Media.View.Layout.Dialog;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.App.Media.ViewModel.Layout
{
    [ViewModel("Layout")]
    internal class LayoutViewModel : MvcViewModelBase
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

            if(obj is Student )
            {
                EditDialog editDialog = Application.Current.Dispatcher.Invoke(() =>
                {
                    return new EditDialog();
                });

                Message.Instance.ShowLayer(editDialog);
            }
           

        }

        #endregion
    }

}
