using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow.ViewModel
{
    [Route("Loyout")]
    class LoyoutViewModel : NotifyPropertyChanged
    {

        private LinkAction _selectLink;
        /// <summary> 说明  </summary>
        public LinkAction SelectLink
        {
            get { return _selectLink; }
            set
            {
                _selectLink = value;

                RaisePropertyChanged("SelectLink");

            }
        }

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
    }
}
