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


        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() =>
    new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            LinkAction link = new LinkAction();
            link.DisplayName = "总体概览";
            link.Logo = "&#xe69f;";
            link.Controller = "Loyout";
            link.Action = "OverView";
            this.SelectLink = link; 
           
        }


        private bool CanLoaded(string args)
        {
            return true;
        } 

        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：应用
            if (command == "Button.ShowDialogMessage")
            {
                await MessageService.ShowSumitMessge("你为什么要点我？");

            }
            //  Do：取消
            else if (command == "Button.ShowSnackMessage")
            {
                MessageService.ShowSnackMessageWithNotice("干嘛老点我？");
            }
            //  Do：取消
            else if (command == "Button.ShowNotifyIcon")
            {
                //MessageService.ShowSnackMessageWithNotice("干嘛老点我？");
            }
            //  Do：取消
            else if (command == "Button.ShowNotifyMessage")
            {
                MessageService.ShowNotifyMessage("我是消息气泡");
            }
        }
    }
}
