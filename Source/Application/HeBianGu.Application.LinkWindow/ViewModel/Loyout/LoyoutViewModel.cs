using HeBianGu.Applications.ControlBase.LinkWindow;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageContainer;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using HeBianGu.Service.Validation;
using HeBianGu.Window.Message;
using HeBianGu.Window.Notify;
using HeBianGu.Window.Version;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Application.LinkWindow
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


        private StoryBoardPlayerViewModel _storyBoardPlayerViewModel = new StoryBoardPlayerViewModel();
        /// <summary> 说明  </summary>
        public StoryBoardPlayerViewModel StoryBoardPlayerViewModel
        {
            get { return _storyBoardPlayerViewModel; }
            set
            {
                _storyBoardPlayerViewModel = value;
                RaisePropertyChanged("StoryBoardPlayerViewModel");
            }
        }

        protected override void Init()
        {

            this.ObservableSource.PageCount = 6;

            this.ObservableSource.Clear();

            var data = AssemblyDomain.Instance.GetTreeListData();

            this.ObservableSource.Add(data.ToArray());

        }


        public ObservableCollection<FComboBoxItemModel> ComboBoxItems
        {
            get { return _comboBoxItems; }
            set { _comboBoxItems = value; RaisePropertyChanged("ComboBoxItems"); }
        }
        private ObservableCollection<FComboBoxItemModel> _comboBoxItems = new ObservableCollection<FComboBoxItemModel>();

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

                            Thread.Sleep(10);
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

                        Thread.Sleep(10);
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
                NotifyMessageService.ShowNotifyDialogMessage("自定义气泡消息" + DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"), "友情提示", 5);
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

                List<Tuple<string, Action<IMessageWindow>>> acts = new List<Tuple<string, Action<IMessageWindow>>>();


                Action<IMessageWindow> action = l =>
                  {
                      l.CloseAnimation(l as WindowBase);

                      l.Result = true;

                      MessageService.ShowSnackMessageWithNotice("你点到我了！");
                  };

                acts.Add(Tuple.Create("按钮一", action));
                acts.Add(Tuple.Create("按钮二", action));
                acts.Add(Tuple.Create("按钮三", action));

                MessageWindow.ShowDialogWith("这是自定义按钮提示消息", "好心提醒",false, acts.ToArray());
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

                message.Message = "错误信息！";

                this.AddMessage(message, command);
            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Info"))
            {
                InfoMessage message = new InfoMessage();

                message.Message = "提示信息！";

                this.AddMessage(message, command);

            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Success"))
            {
                SuccessMessage message = new SuccessMessage();

                message.Message = "保存成功！";

                this.AddMessage(message, command);

            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Fatal"))
            {
                FatalMessage message = new FatalMessage();

                message.Message = "问题很严重！";

                this.AddMessage(message, command);


            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Warn"))
            {
                WarnMessage message = new WarnMessage();

                message.Message = "警告信息！";

                this.AddMessage(message, command);

            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.Message.Dailog"))
            {
                DailogMessage message = new DailogMessage();

                message.Message = "可以保存了么?";

                this.AddMessage(message, command);
            }

            //  Do：气泡消息
            else if (command.StartsWith("Button.ShowCoverMessge"))
            {
                SettingControl setting = new SettingControl();
                MessageService.ShowLayer(setting);
            }

            else if (command == "Button.Add")
            {

                if (this.StoryBoardPlayerViewModel.PlayMode)
                {
                    MessageService.ShowSnackMessageWithNotice("请先停止播放再进行添加！");
                    return;
                }
                this.StoryBoardPlayerViewModel.Create();
            }
            else if (command == "init")
            {
                for (int i = 0; i < 60; i++)
                {
                    ComboBoxItems.Add(new FComboBoxItemModel()
                    {
                        Header = "ComboBoxItem" + (ComboBoxItems.Count + 1),
                        Value = (ComboBoxItems.Count + 1),
                        CanDelete = true
                    });
                }
            }


        }

        void AddMessage(MessageBase message, string command)
        {
            if (command.EndsWith("System"))
            {
                NotifyMessageService.ShowSysMessage(message);
            }
            else if (command.EndsWith("Window"))
            {
                NotifyMessageService.ShowWinMessage(message);
            }
            else
            {
                this.MessageSource.Add(message);
            }
        }


        private TextBoxViewModel _textBoxViewModel = new TextBoxViewModel();
        /// <summary> 说明  </summary>
        public TextBoxViewModel TextBoxViewModel
        {
            get { return _textBoxViewModel; }
            set
            {
                _textBoxViewModel = value;
                RaisePropertyChanged("TextBoxViewModel");
            }
        }



    }


    public partial class StoryBoardPlayerViewModel : NotifyPropertyChanged
    {
        public StoryBoardPlayerViewModel()
        {
            this.IndexChanged += l =>
            {
                //  Do ：触发子项进度
                foreach (var item in this.Collection)
                {
                    item.OnIndexChanged(l);
                }
            };
        }
        private ObservableCollection<StoryBoardItemViewModel> _collection = new ObservableCollection<StoryBoardItemViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<StoryBoardItemViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        public StoryBoardItemViewModel Create()
        {
            StoryBoardItemViewModel item = new StoryBoardItemViewModel(this);

            //  Do ：注册子项进度
            item.IndexChanged += l =>
            {
                //  Do ：触发子项进度外部事件
                ItemIndexChanged?.Invoke(item, l);
            };

            this.Collection.Add(item);

            return item;
        }


        private double _maxValue = 100.0;
        /// <summary> 说明  </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }


        private double _minValue = 0.0;
        /// <summary> 说明  </summary>
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }


        private double _value = 0.0;
        /// <summary> 说明  </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


        private int _speed = 0;
        /// <summary> 说明  </summary>
        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged("Speed");
            }
        }


        System.Timers.Timer timer = new System.Timers.Timer();

        public void Start()
        {
            this.Stop();

            timer = new System.Timers.Timer();

            //timer.Interval = 1000 - Speed;

            timer.Elapsed += (l, k) =>
            {
                timer.Interval = Speed == 0 ? 1000 : 1010 - 125 * Speed;

                if (this.Value < MaxValue)
                {
                    this.Value++;
                }
                else
                {
                    this.Value = 0;
                }

                IndexChanged?.Invoke(this.Value);
            };

            timer.Start();
        }

        public void StartWith(StoryBoardItemViewModel item)
        {
            this.Stop();

            this.Collection.Foreach(l =>
            {
                if (l != item)
                    l.PlayMode = false;

                l.IsEnbled = l != item;
            });


            timer = new System.Timers.Timer();

            this.Value = MaxValue * item.LeftPercent;

            timer.Interval = Speed == 0 ? 1000 : 1010 - 125 * Speed;

            timer.Elapsed += (l, k) =>
            {
                timer.Interval = Speed == 0 ? 1000 : 1010 - 125 * Speed;



                if (this.Value < (int)MaxValue * item.RightPercent && this.Value >= MaxValue * item.LeftPercent)
                {
                    this.Value++;
                }
                else
                {
                    this.Value = MaxValue * item.LeftPercent;
                }


                IndexChanged?.Invoke(this.Value);
            };

            timer.Start();


        }

        public void Stop()
        {
            timer.Stop();
        }


        private bool _playMode;
        /// <summary> 说明  </summary>
        public bool PlayMode
        {
            get { return _playMode; }
            set
            {
                this._playMode = value;
                RaisePropertyChanged("PlayMode");
            }
        }




        protected override async void RelayMethod(object obj)
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
            else if (command == "ToggleButton.Click.Play")
            {
                if (this.Collection.Count == 0)
                {
                    this.PlayMode = false;

                    MessageService.ShowSnackMessageWithNotice("请至少添加一个条目！");

                    return;
                }

                this.Collection.Foreach(l => l.PlayMode = true);

                this.Collection.Foreach(l => l.IsEnbled = false);

                this.Start();


            }

            else if (command == "ToggleButton.Click.Stop")
            {
                this.Collection.Foreach(l => l.PlayMode = false);

                this.Collection.Foreach(l => l.IsEnbled = true);

                this.Stop();
            }
        }

        public event Action<double> IndexChanged;

        public event Action<StoryBoardItemViewModel, double> ItemIndexChanged;

    }

    public partial class StoryBoardItemViewModel : NotifyPropertyChanged
    {
        static int Index = 0;

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public StoryBoardItemViewModel()
        {

        }

        StoryBoardPlayerViewModel _parent;

        public StoryBoardItemViewModel(StoryBoardPlayerViewModel parent)
        {
            _parent = parent;

            this.Name = (Index++).ToString();
        }

        public event Action<double> IndexChanged;

        private bool _isEnbled = false;
        /// <summary> 说明  </summary>
        public bool IsEnbled
        {
            get { return _isEnbled; }
            set
            {
                _isEnbled = value;
                RaisePropertyChanged("IsEnbled");
            }
        }


        private double _leftPercent = 0.0;
        /// <summary> 说明  </summary>
        public double LeftPercent
        {
            get { return _leftPercent; }
            set
            {
                _leftPercent = value;
                RaisePropertyChanged("LeftPercent");
            }
        }


        private double _rightPercent = 1.0;
        /// <summary> 说明  </summary>
        public double RightPercent
        {
            get { return _rightPercent; }
            set
            {
                _rightPercent = value;
                RaisePropertyChanged("RightPercent");
            }
        }

        private bool _playMode;
        /// <summary> 说明  </summary>
        public bool PlayMode
        {
            get { return _playMode; }
            set
            {
                _playMode = value;

                RaisePropertyChanged("PlayMode");
            }
        }


        private double _value = 0;
        /// <summary> 说明  </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


        public void OnIndexChanged(double value)
        {
            if (this.IsEnbled == false && this.PlayMode)
            {
                this.IndexChanged?.Invoke(value);

                this.Value = value;
            }
        }

        public RelayCommand<ToggleButton> ToggleButtonCheckChangedCommand => new Lazy<RelayCommand<ToggleButton>>(() => new RelayCommand<ToggleButton>(async l => await ToggleButtonCheckedChanged(l))).Value;

        internal async Task ToggleButtonCheckedChanged(ToggleButton commandParamer)
        {
            //  Do ：避免通过属性修改触发此事件
            if (!commandParamer.IsFocused) return;

            if (this.PlayMode)
            {
                this._parent.StartWith(this);
            }
            else
            {
                this._parent.Stop();
            }
        }


        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "StoryBoardItem.Button.Delelte")
            {
                if (this._parent.PlayMode)
                {
                    MessageService.ShowSnackMessageWithNotice("请先停止播放再进行此操作！");
                    return;
                }

                var result = await MessageService.ShowResultMessge("确定要删除当前项目？");

                if (!result) return;

                this._parent.Collection.Remove(this);

                this.IndexChanged = null;
            }
        }
    }


    /// <summary> 文本输入验证</summary>
    internal class TextBoxViewModel : ValidationPropertyChanged
    {
        public TextBoxViewModel():base()
        {
           
        }
        private string _name;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                RaisePropertyChanged("Name");
            }
        }


        private string _age;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }


        private string _email;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "邮箱地址不合法！")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }


        private string _phone;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }


        private string _account;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$", ErrorMessage = "字母开头，允许5-16字节，允许字母数字下划线！")]
        public string Accont
        {
            get { return _account; }
            set
            {
                _account = value;
                RaisePropertyChanged("Accont");
            }
        }


        private string _passWord;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "以字母开头，长度在6~18之间，只能包含字母、数字和下划线！！")]
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }


        private string _regin;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "只能5位的数字")]

        public string Regin
        {
            get { return _regin; }
            set
            {
                _regin = value;
                RaisePropertyChanged("Regin");
            }
        }


        private string _limit;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{5,8}$", ErrorMessage = "只能5-8位的数字")]
        public string Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                RaisePropertyChanged("Limit");
            }
        }


        private string _cardID;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{15}|\d{18}$", ErrorMessage = "身份证号码不合法！")]
        public string CardID
        {
            get { return _cardID; }
            set
            {
                _cardID = value;
                RaisePropertyChanged("CardID");
            }
        }

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.CheckDataSumit")
            {
                if (this.IsValid())
                {
                    MessageService.ShowSnackMessageWithNotice("数据校验成功！");
                }
                else
                {
                    MessageService.ShowSnackMessageWithNotice("数据校验错误 - " + this.Error);
                }

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion

    }
}
