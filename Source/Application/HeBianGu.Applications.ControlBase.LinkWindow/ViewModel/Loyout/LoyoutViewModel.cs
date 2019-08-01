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


            //this.Collection.Clear();

            //for (int i = 0; i < 100; i++)
            //{
            //    UpLoadItem item = new UpLoadItem();

            //    item.Index = (i + 1).ToString();
            //    item.Name = "吉林大学";
            //    item.Path = "吉林省长春市";

            //    this.Collection.Add(item);
            //}

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





        //private ObservableCollection<UpLoadItem> _collection = new ObservableCollection<UpLoadItem>();
        ///// <summary> 说明  </summary>
        //public ObservableCollection<UpLoadItem> Collection
        //{
        //    get { return _collection; }
        //    set
        //    {
        //        _collection = value;
        //        RaisePropertyChanged("Collection");
        //    }
        //}



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
            //  Do：气泡消息
            else if (command == "Button.ShowWindowSumitMessage")
            {
                MessageWindow.ShowSumit("这是窗口提示消息");
            }

            //  Do：气泡消息
            else if (command == "Button.ShowWindowResultMessage")
            {
                MessageWindow.ShowDialog("这是窗口提示消息");
            }
            //  Do：气泡消息
            else if (command == "Button.ShowWindowIndentifyMessage")
            {

                List<Tuple<string, Action<MessageWindow>>> acts = new List<Tuple<string, Action<MessageWindow>>>();


                Action<MessageWindow> action = l =>
                  {
                      l.DialogResult = true;
                      l.Close();

                      MessageService.ShowSnackMessageWithNotice("你点到我了！");
                  };

                acts.Add(Tuple.Create("按钮一", action));
                acts.Add(Tuple.Create("按钮二", action)); 
                acts.Add(Tuple.Create("按钮三", action)); 

                MessageWindow.ShowDialogWith("这是自定义按钮提示消息","好心提醒", acts.ToArray());
            }


            //  Do：气泡消息
            else if (command == "Button.Upgrade")
            {
                UpgradeWindow window = new UpgradeWindow();
                window.TitleMessage = "发现新版本：V3.0.1";
                List<string> message = new List<string>();
                message.Add("1、增加了检验更新和版本下载");
                message.Add("2、增加了Mvc跳转页面方案");
                message.Add("3、修改了一些已知BUG");
                window.Message = message;

                var find = window.ShowDialog();

                if (find.HasValue && find.Value)
                {
                    DownLoadWindow downLoad = new DownLoadWindow();
                    downLoad.TitleMessage = "正在下载新版本：V3.0.1";
                    downLoad.Url = @"http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4";
                    downLoad.Message = message;
                    downLoad.ShowDialog();
                }
            }
        }
    }
}
