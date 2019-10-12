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

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [ViewModel("Loyout")]
    class LoyoutViewModel : MvcViewModelBase
    {

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


        private ObservableSource<TreeNodeEntity> _observableSource = new ObservableSource<TreeNodeEntity>();
        /// <summary> 说明  </summary>
        public ObservableSource<TreeNodeEntity> ObservableSource
        {
            get { return _observableSource; }
            set
            {
                _observableSource = value;
                RaisePropertyChanged("ObservableSource");
            }
        }

        private ObservableCollection<MessageBase> _messageSource = new ObservableCollection<MessageBase>();
        /// <summary> 说明  </summary>
        public ObservableCollection<MessageBase> MessageSource
        {
            get { return _messageSource; }
            set
            {
                _messageSource = value;
                RaisePropertyChanged("MessageSource");
            }
        }

        

        protected override void Init()
        {

            this.ObservableSource.PageCount = 6;

            this.ObservableSource.Clear();

            var data = AssemblyDomain.Instance.GetTreeListData();

            this.ObservableSource.Add(data.ToArray());

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
                var result = await MessageService.ShowResultMessge("确认要退出系统?");

                if (result)
                {
                    MessageService.ShowSnackMessageWithNotice("你点击了取消");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("你点击了确定");
                }
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
            else if (command == "Button.ShowIdentifyNotifyMessage")
            {
                MessageService.ShowNotifyDialogMessage("自定义气泡消息" + DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"),"友情提示",5);
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
                      l.CloseAnimation(l);

                      l.Result = true;

                      MessageService.ShowSnackMessageWithNotice("你点到我了！");
                  };

                acts.Add(Tuple.Create("按钮一", action));
                acts.Add(Tuple.Create("按钮二", action));
                acts.Add(Tuple.Create("按钮三", action));

                MessageWindow.ShowDialogWith("这是自定义按钮提示消息", "好心提醒", acts.ToArray());
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

                //UpgradeWindow.BeginUpgrade("发现新版本：V3.0.1", @"http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4",
                //   message.ToArray());
            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Error"))
            {
                ErrorMessage message = new ErrorMessage();

                message.Message = "错误信息！" + DateTime.Now.ToString();

                this.AddMessage(message, command);
            }
            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Info"))
            {
                InfoMessage message = new InfoMessage();

                message.Message = "提示信息！" + DateTime.Now.ToString();

                this.AddMessage(message, command);

            }
            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Success"))
            {
                SuccessMessage message = new SuccessMessage();

                message.Message = "保存成功！" + DateTime.Now.ToString();

                this.AddMessage(message, command);

            }
            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Fatal"))
            {
                FatalMessage message = new FatalMessage();

                message.Message = "问题很严重！" + DateTime.Now.ToString();

                this.AddMessage(message, command);


            }
            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Warn"))
            {
                WarnMessage message = new WarnMessage();

                message.Message = "警告信息！" + DateTime.Now.ToString();

                this.AddMessage(message, command);

            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Dailog"))
            {
                DailogMessage message = new DailogMessage();

                message.Message = "可以保存了么?" + DateTime.Now.ToString();

                this.AddMessage(message, command);
            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.ShowCoverMessge"))
            {
                SettingControl setting = new SettingControl();

                MessageService.ShowWithLayer(setting);

            }

           
        }

        void AddMessage(MessageBase message,string command)
        {
            if(command.EndsWith("System"))
            {
                MessageService.ShowSystemNotifyMessage(message);
            }
            else if(command.EndsWith("Window"))
            {

                MessageService.ShowWindowNotifyMessage(message);
            }
            else
            {
                this.MessageSource.Add(message);
            }
        }


    }
}
