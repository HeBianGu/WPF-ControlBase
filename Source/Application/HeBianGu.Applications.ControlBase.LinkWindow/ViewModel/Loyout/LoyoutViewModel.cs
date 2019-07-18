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

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    { 
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


            this.Collection.Clear();

            for (int i = 0; i < 100; i++)
            {
                UpLoadItem item = new UpLoadItem();

                item.Index = (i + 1).ToString();
                item.Name = "吉林大学";
                item.Path = "吉林省长春市";

                this.Collection.Add(item);
            }

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





        private ObservableCollection<UpLoadItem> _collection = new ObservableCollection<UpLoadItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<UpLoadItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }



        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.ShowDialogMessage")
            {
                await MessageService.ShowSumitMessge("这是消息对话框？");

            }
            //  Do：等待消息
            else if (command == "Button.ShowWaittingMessge")
            {

                await MessageService.ShowWaittingMessge(() => Thread.Sleep(2000));

            }
            //  Do：百分比进度对话框
            else if (command == "Button.ShowPercentProgress")
            {
                Action<IPercentProgress> action = l =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            l.Value = i;

                            Thread.Sleep(50);
                        }

                        Thread.Sleep(1000);

                        MessageService.ShowSnackMessageWithNotice("加载完成！");
                    };
                await MessageService.ShowPercentProgress(action);

            }
            //  Do：文本进度对话框
            else if (command == "Button.ShowStringProgress")
            {
                Action<IStringProgress> action = l =>
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        l.MessageStr = $"正在提交当前页第{i}份数据,共100份";

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(1000);

                    MessageService.ShowSnackMessageWithNotice("提交完成：成功100条，失败0条！");
                };

                await MessageService.ShowStringProgress(action);

            }
            //  Do：确认取消对话框
            else if (command == "Button.ShowResultMessge")
            {
                Action<object, DialogClosingEventArgs> action = (l, k) =>
                {
                    if ((bool)k.Parameter)
                    {
                        MessageService.ShowSnackMessageWithNotice("你点击了取消");
                    }
                    else
                    {
                        MessageService.ShowSnackMessageWithNotice("你点击了确定");
                    }
                };

                MessageService.ShowResultMessge("确认要退出系统?", action);


            }
            //  Do：提示消息
            else if (command == "Button.ShowSnackMessage")
            {
                MessageService.ShowSnackMessageWithNotice("这是提示消息？");
            } 
            //  Do：气泡消息
            else if (command == "Button.ShowNotifyMessage")
            {
                MessageService.ShowNotifyMessage("你有一条报警信息需要处理，请检查", "Notify By HeBianGu");
            }
        }
    }
}
