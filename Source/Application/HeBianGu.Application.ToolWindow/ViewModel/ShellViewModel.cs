using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.ToolWindow
{
    [ViewModel("Shell")]
    class ShellViewModel : WindowLinkViewModel
    {


        private ObservableCollection<ClickAction> _clickActions;
        /// <summary> 说明  </summary>
        public ObservableCollection<ClickAction> ClickActions
        {
            get { return _clickActions; }
            set
            {
                _clickActions = value;
                RaisePropertyChanged("ClickActions");
            }
        }


        Random random = new Random();

        protected async override void RelayMethod(object obj)
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

            //  Do：取消
            else if (command == "init")
            {
               
            }
        }
    }

   

}
