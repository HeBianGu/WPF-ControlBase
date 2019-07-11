using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {

        //private LinkAction _selectLink;
        ///// <summary> 说明  </summary>
        //public LinkAction SelectLink
        //{
        //    get { return _selectLink; }
        //    set
        //    {
        //        _selectLink = value;

        //        RaisePropertyChanged("SelectLink");

        //    }
        //}


        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() =>
    new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            ILinkActionBase link = new LinkActionBase();
            link.DisplayName = "总体概览";
            link.Logo = "&#xe69f;";
            link.Controller = "Loyout";
            link.Action = "OverView";
            this.SelectLink = link; 
           
        }


        private string _buttonContentText;
        /// <summary> 说明  </summary>
        public string ButtonContentText
        {
            get { return _buttonContentText; }
            set
            {
                _buttonContentText = value;
                RaisePropertyChanged("ButtonContentText");
            }
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
                await MessageService.ShowSumitMessge("这是消息对话框？");

            }
            //  Do：取消
            else if (command == "Button.ShowSnackMessage")
            {
                MessageService.ShowSnackMessageWithNotice("这是提示消息？");
            }
            //  Do：取消
            else if (command == "Button.ShowNotifyIcon")
            {
                //MessageService.ShowSnackMessageWithNotice("干嘛老点我？");
            }
            //  Do：取消
            else if (command == "Button.ShowNotifyMessage")
            {
                MessageService.ShowNotifyMessage("这是显示的气泡", "Notify By HeBianGu");
            }
        }
    }
}
