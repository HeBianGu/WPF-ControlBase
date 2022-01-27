using HeBianGu.Application.VsWindow.View.Dialog;
using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Link;
using HeBianGu.Window.Notify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shell;
using System.Windows.Threading;

namespace HeBianGu.Application.VsWindow
{
    class ShellViewModel : WindowLinkViewModel
    {

        IAssemblyDomain _domain;

        public ShellViewModel(IAssemblyDomain domain)
        {
            _domain = domain;

            //  Do：加载记住账号和密码
            var from = _domain.GetAccountConfig();

            if (from != null)
            {
                if (from.Remenber)
                {
                    this.LoginPassWord = from.LoginPassWord;
                    this.Remenber = true;
                }
                this.LoginUseName = from.LoginUseName;

            }
        }


        private ObservableCollection<TaskViewModel> _tasks = new ObservableCollection<TaskViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TaskViewModel> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }


        private TaskViewModel _selectedTask;
        /// <summary> 说明  </summary>
        public TaskViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                RaisePropertyChanged("SelectedTask");
            }
        }



        private TestViewModel _selectedNode;
        /// <summary> 说明  </summary>
        public TestViewModel SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                RaisePropertyChanged("SelectedNode");
            }
        }

        private ObservableCollection<EquipmentViewModel> _equipmentTemplates = new ObservableCollection<EquipmentViewModel>();
        /// <summary> 设备列表  </summary>
        public ObservableCollection<EquipmentViewModel> EquipmentTemplates

        {
            get { return _equipmentTemplates; }
            set
            {
                _equipmentTemplates = value;
                RaisePropertyChanged("Equipments");
            }
        }

        private EquipmentViewModel _selectedEquipment;
        /// <summary> 说明  </summary>
        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                RaisePropertyChanged("SelectedEquipment");
            }
        }


        private ObservableCollection<ServerViewModel> _servers = new ObservableCollection<ServerViewModel>();
        /// <summary> 发生器列表  </summary>
        public ObservableCollection<ServerViewModel> Servers
        {
            get { return _servers; }
            set
            {
                _servers = value;
                RaisePropertyChanged("Servers");
            }
        }


        private ServerViewModel _selectedServer;
        /// <summary> 说明  </summary>
        public ServerViewModel SelectedServer
        {
            get { return _selectedServer; }
            set
            {
                _selectedServer = value;
                RaisePropertyChanged("SelectedServer");
            }
        }



        private ObservableCollection<TestViewModel> _tskTempaltes = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> TaskTempaltes
        {
            get { return _tskTempaltes; }
            set
            {
                _tskTempaltes = value;
                RaisePropertyChanged("TaskTempaltes");
            }
        }



        private TestViewModel _selectedFile;
        /// <summary> 说明  </summary>
        public TestViewModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                RaisePropertyChanged("SelectedFile");
            }
        }


        private ObservableCollection<TestViewModel> _templates = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Templates
        {
            get { return _templates; }
            set
            {
                _templates = value;
                RaisePropertyChanged("Templates");
            }
        }


        private TestViewModel _selectedTemplate;
        /// <summary> 说明  </summary>
        public TestViewModel SelectedTemplate
        {
            get { return _selectedTemplate; }
            set
            {
                _selectedTemplate = value;
                RaisePropertyChanged("SelectedTemplate");
            }
        }


        private TestViewModel _currentTemplate;
        /// <summary> 说明  </summary>
        public TestViewModel CurrentTempalte
        {
            get { return _currentTemplate; }
            set
            {
                _currentTemplate = value;
                RaisePropertyChanged("CurrentTempalte");
            }
        }


        private ObservableCollection<TestViewModel> _historys = new ObservableCollection<TestViewModel>();
        /// <summary> 历史提交记录  </summary>
        public ObservableCollection<TestViewModel> Historys
        {
            get { return _historys; }
            set
            {
                _historys = value;
                RaisePropertyChanged("Historys");
            }
        }


        private TestViewModel _selectedHistory;
        /// <summary> 选中的历史记录  </summary>
        public TestViewModel SelectedHistory
        {
            get { return _selectedHistory; }
            set
            {
                _selectedHistory = value;
                RaisePropertyChanged("SelectedHistory");
            }
        }


        private string _title;
        /// <summary> 标题  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }


        private double _percent;
        /// <summary> 说明  </summary>
        public double Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                RaisePropertyChanged("Percent");
            }
        }



        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;

                RaisePropertyChanged("Message");
            }
        }



        private bool _isRunEnbel = true;
        /// <summary> 开始按钮是否可用  </summary>
        public bool IsRunEnbel
        {
            get { return _isRunEnbel; }
            set
            {
                _isRunEnbel = value;
                RaisePropertyChanged("IsRunEnbel");
            }
        }

        private bool _isStopEnbel;
        /// <summary> 停止按钮是否可用  </summary>
        public bool IsStopEnbel
        {
            get { return _isStopEnbel; }
            set
            {
                _isStopEnbel = value;
                RaisePropertyChanged("IsStopEnbel");
            }
        }



        private bool _stateVisible;
        /// <summary> 设备栏是否可用  </summary>
        public bool StateVisble
        {
            get { return _stateVisible; }
            set
            {
                _stateVisible = value;
                RaisePropertyChanged("StateVisble");
            }
        }


        private ObservableCollection<double> _waveyAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> WaveyAxis
        {
            get { return _waveyAxis; }
            set
            {
                _waveyAxis = value;
                RaisePropertyChanged("WaveyAxis");
            }
        }

        private ObservableCollection<double> _waveData = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> WaveData
        {
            get { return _waveData; }
            set
            {
                _waveData = value;
                RaisePropertyChanged("WaveData");
            }
        }


        private bool _isExpanedProperty;
        /// <summary> 显示设备属性  </summary>
        public bool IsExpanedProperty
        {
            get { return _isExpanedProperty; }
            set
            {
                _isExpanedProperty = value;
                RaisePropertyChanged("IsExpanedProperty");
            }
        }


        private bool _isExpandedServerProperty;
        /// <summary> 显示发生器属性  </summary>
        public bool IsExpandedServerProperty
        {
            get { return _isExpandedServerProperty; }
            set
            {
                _isExpandedServerProperty = value;
                RaisePropertyChanged("IsExpandedServerProperty");
            }
        }



        Random random = new Random();

        protected override void Init()
        {
        }

        protected override async void Loaded(string args)
        {
            base.Loaded(args);

            this.Message = "正在加载...";

            this.StateVisble = true;

            this.SelectedTask = this.Tasks?.FirstOrDefault();

            //  Do ：加载任务
            this.Tasks.Clear();

            this.Tasks.Add(new TaskViewModel() { Value = "任务一", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Recent) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务二", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务三", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务四", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务五", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.History) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务六", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务七", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts) });
            //this.Scenes.Add(new TestViewModel() { Value = "ApplicationData", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务八", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Templates) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务九", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务十", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts) });
            this.Tasks.Add(new TaskViewModel() { Value = "任务十一", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) });

            this.SelectedTask = this.Tasks?.FirstOrDefault();

            await Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    this.Percent = i;

                    Thread.Sleep(20);
                }
            });

            int r = random.Next(5, 20);

            await Task.Run(() =>
            {
                for (int i = 0; i <= r; i++)
                {
                    EquipmentViewModel cc = new EquipmentViewModel();

                    cc.Value = "设备模板_" + i.ToString();

                    cc.Value1 = i % 5 == 0 ? "电台" : i % 4 == 0 ? "雷达" : "电台";

                    cc.Value2 = i % 5 == 0 ? "通信" : i % 4 == 0 ? "雷达" : "干扰";

                    cc.Int1 = i;

                    ServerViewModel temp = new ServerViewModel();

                    temp.Value = "发生器_" + i.ToString();

                    temp.Value1 = "192.168.0." + i.ToString();

                    //temp.Value2 = i % 5 == 0 ? "生成中" : i % 4 == 0 ? "未连接" : i % 3 == 0 ? "故障" : "空闲";

                    temp.Value3 = i % 5 == 0 ? "类型一" : i % 4 == 0 ? "类型二" : i % 3 == 0 ? "类型二" : "类型二";

                    temp.Int1 = i;

                    temp.Double1 = random.Next(45, 90);

                    temp.Double2 = random.Next(90, 180);

                    temp.Double3 = random.Next(0, 360);

                    temp.Int2 = random.Next(100, 300);

                    System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                      {
                          if (this.EquipmentTemplates.Count == 0)
                          {
                              this.EquipmentTemplates.Add(new EquipmentViewModel() { Value = "空模板", Value1 = "默认" });

                              this.Servers.Add(new ServerViewModel() { Value = "空模板", Value3 = "默认" });
                          }

                          this.EquipmentTemplates.Add(cc);

                          this.Servers.Add(temp);
                      }));

                }
            });




            await this.RefreshScene();

            this.Message = "加载完成...";

            await Task.Delay(2000).ContinueWith(l =>
             {
                 this.StateVisble = false;
             });

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {

                {
                    //Do ：分组
                    ICollectionView vw = CollectionViewSource.GetDefaultView(this.EquipmentTemplates);
                    vw.GroupDescriptions.Clear();
                    vw.GroupDescriptions.Add(new PropertyGroupDescription("Value1"));
                }

                {
                    //Do ：分组
                    ICollectionView vw = CollectionViewSource.GetDefaultView(this.Servers);
                    vw.GroupDescriptions.Clear();
                    vw.GroupDescriptions.Add(new PropertyGroupDescription("Value3"));
                }
            });

        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)
        {
            //  Do ：属性显示
            if (obj is RoutedPropertyChangedEventArgs<object> arg)
            {
                this.SelectedNode = arg.NewValue as TestViewModel;
            }

            //  Do ：拖拽到扇形区域显示
            if (obj is DragEventArgs dragEventArgs)
            {
                if (dragEventArgs.Data.GetDataPresent("Equipment"))
                {
                    var data = dragEventArgs.Data.GetData("Equipment") as EquipmentViewModel;

                    if (data == null) return;

                    FrameworkElement element = dragEventArgs.OriginalSource as FrameworkElement;

                    ServerViewModel server = element.Tag as ServerViewModel;

                    if (server == null) return;

                    server.Children.Add(data);

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("添加成功");
                }
            }

            string command = obj?.ToString();

            if (command == "新建任务")
            {
                SelectTypeControl select = new SelectTypeControl();

                var result = await HeBianGu.General.WpfControlLib.Message.Instance.ShowCustomDialog<bool>(select);

                if (!result) return;

                ConfigSceneControl config = new ConfigSceneControl();

                var model = new TaskViewModel();

                config.Model = model;

                config.Model.Value3 = select.Model?.Value;

                config.Model.Value4 = select.Model?.Value1;

                var result1 = await HeBianGu.General.WpfControlLib.Message.Instance.ShowCustomDialog<bool?>(config);

                //  Do ：取消
                if (result1 == false) return;

                //  Do ：上一步
                if (result1 == null)
                {
                    this.RelayMethod(command); return;
                }

                //  Do ：创建
                if (string.IsNullOrEmpty(config.Model.Value) || string.IsNullOrEmpty(config.Model.Value1))
                {
                    HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("数据不合法"); return;

                }
                this.Tasks.Add(model);

                this.CurrentTempalte = select.Model;

                this.SelectedTask = model;

                await this.RefreshScene();

            }

            else if (command == "打开任务")
            {
                TasksControl select = new TasksControl();

                var result = await HeBianGu.General.WpfControlLib.Message.Instance.ShowCustomDialog<bool>(select);

                if (!result) return;

                this.SelectedTask = select.Model;

                await this.RefreshScene();

            }

            else if (command == "用户管理")
            {
                UserManagerControl select = new UserManagerControl();

                HeBianGu.General.WpfControlLib.Message.Instance.ShowLayer(select);

            }
            else if (command == "地图管理")
            {
                MapManagerControl select = new MapManagerControl();

                HeBianGu.General.WpfControlLib.Message.Instance.ShowLayer(select);

            }
            else if (command == "主机管理")
            {
                ServerManagerControl select = new ServerManagerControl();

                HeBianGu.General.WpfControlLib.Message.Instance.ShowLayer(select);

            }
            else if (command == "设备模板管理")
            {
                EquipManagerControl select = new EquipManagerControl();

                HeBianGu.General.WpfControlLib.Message.Instance.ShowLayer(select);

            }
            else if (command == "ListBox.MouseDoubleClick.SetSelectedScene")
            {
                //this.SelectedTask = this.SelectedScene;

                await this.RefreshScene();
            }
            else if (command == "ListBox.MouseDoubleClick.SetSelectedTemplate")
            {
                this.CurrentTempalte = this.SelectedTemplate;

                await this.RefreshScene();
            }
            else if (command == "Button.Click.Commit")
            {
                MapperControl select = new MapperControl();

                var result = await HeBianGu.General.WpfControlLib.Message.Instance.ShowCustomDialog<bool>(select);

                if (!result) return;

                this.Message = "正在检查参数";

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("开始运行");

                HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbar(l => l.ProgressState = TaskbarItemProgressState.Indeterminate);


                this.StateVisble = true;

                this.IsRunEnbel = false;

                this.IsStopEnbel = true;

                await NotifyMessageService.ShowWinProgressStrMessage(l =>
                 {
                     for (int i = 0; i <= 100; i++)
                     {
                         this.Percent = i;

                         l.Message = $"正在检查信号参数，第{i}份数据,共100份";

                         Thread.Sleep(50);
                     }

                     Thread.Sleep(500);

                     return true;
                 });

                if (random.Next(1, 5) == 1)
                {
                    NotifyMessageService.ShowWinErrorMessage("检查信号错误，请检查信号[信号_2]");

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("检查信号错误，详情见日志");

                    this.IsRunEnbel = true;

                    this.IsStopEnbel = false;

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbar(l => l.ProgressState = TaskbarItemProgressState.None);

                    return;
                }

                NotifyMessageService.ShowWinSuccessMessage("检查信号成功");

                HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbarNormal(l => l.ProgressValue = 0.3);

                this.Message = "正在生成信号";

                //this.IsRunEnbel = false;

                //this.IsStopEnbel = true;

                await NotifyMessageService.ShowWinProgressStrMessage(l =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        this.Percent = i;

                        l.Message = $"正在生成信号第{i}份数据,共100份";

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(500);

                    return true;
                });

                if (random.Next(1, 5) == 1)
                {
                    NotifyMessageService.ShowWinErrorMessage("生成信号错误，详情见日志");

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("生成信号错误，详情见日志");

                    this.IsRunEnbel = true;

                    this.IsStopEnbel = false;

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbar(l => l.ProgressState = TaskbarItemProgressState.None);

                    return;
                }

                HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbarNormal(l => l.ProgressValue = 0.6);

                NotifyMessageService.ShowWinSuccessMessage("生成信号成功");

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("生成信号成功");

                this.Historys.Add(new TestViewModel() { Value = this.Title });

                this.Message = "正在合成信号";

                //this.IsStopEnbel = true;

                await NotifyMessageService.ShowWinProgressStrMessage(l =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        this.Percent = i;

                        l.Message = $"正在合成信号第{i}份数据,共100份";

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(500);

                    return true;
                });

                if (random.Next(1, 5) == 1)
                {
                    NotifyMessageService.ShowWinErrorMessage("合成信号错误，详情见日志");

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("合成信号错误，详情见日志");

                    this.IsRunEnbel = true;

                    this.IsStopEnbel = false;

                    HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbar(l => l.ProgressState = TaskbarItemProgressState.None);

                    return;
                }

                NotifyMessageService.ShowWinSuccessMessage("合成信号成功");

                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("运行完成");

                HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbarNormal(l => l.ProgressValue = 1);

                this.Historys.Add(new TestViewModel() { Value = this.Title });

                this.Message = "运行完成";

                this.IsRunEnbel = true;

                this.IsStopEnbel = false;

                await Task.Delay(3000).ContinueWith(l =>
                {
                    this.StateVisble = false;
                });

                HeBianGu.General.WpfControlLib.Message.Instance.ShowTaskbar(l => l.ProgressState = TaskbarItemProgressState.None);
            }
            //  Do ：登录
            else if (command == "Sumit")
            {
                this.IsBuzy = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(500);

                    string err;

                    var result = _domain.Login(this.LoginUseName, this.LoginPassWord, this.Remenber, out err);

                    if (result)
                    {
                        this.LoginMessage = "登录成功";
                        this.IsBuzy = false;

                        Task.Delay(500).ContinueWith(l =>
                        {
                            this.IsLogined = true;

                        });
                    }
                    else
                    {
                        this.IsBuzy = false;
                        this.IsEnbled = false;

                        //this.LoginMessage = "网络错误，登录失败";
                        this.LoginMessage = err;

                        Task.Delay(1000).ContinueWith(l =>
                        {
                            this.LoginMessage = "登录";
                            this.IsEnbled = true;
                        });
                    }
                    //});

                });

            }

            else if (command == "SelectionChanged.ShowProperty")
            {
                this.IsExpanedProperty = true;
                this.IsExpandedServerProperty = false;
            }

            else if (command == "SelectionChanged.Server.ShowProperty")
            {
                this.IsExpanedProperty = false;
                this.IsExpandedServerProperty = true;
            }
        }
        async Task RefreshScene()
        {
            if (!Directory.Exists(this.SelectedTask.Value1))
            {
                HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice("任务路径不存在，请检查"); return;
            }

            //this.Files.Clear();

            //this.EquipmentTemplates.Clear();

            //this.Servers.Clear();

            this.SelectedTask.Children.Clear();


            await Task.Run(() =>
             {
                 int ts = random.Next(3, 4);

                 for (int i = 0; i < ts; i++)
                 {
                     ServerViewModel temp = new ServerViewModel();

                     temp.Value = "发生器_" + i.ToString();

                     temp.Value1 = "192.168.0." + i.ToString();

                     temp.Value2 = i % 5 == 0 ? "生成中" : i % 4 == 0 ? "未连接" : i % 3 == 0 ? "故障" : "空闲";

                     temp.Int1 = i;

                     //  Do ：Start
                     temp.Double1 = random.Next(0, 20);

                     //  Do ：End
                     temp.Double2 = random.Next(90, 180);

                     //  Do ：Offset
                     temp.Double3 = random.Next(0, 360);

                     //  Do ：Len
                     temp.Int2 = random.Next(200, 400);


                     System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                     {
                         int c = random.Next(29, 30);

                         for (int j = 0; j < c; j++)
                         {
                             EquipmentViewModel cc = new EquipmentViewModel();

                             cc.Value = "设备_" + j.ToString();

                             cc.Value1 = j % 5 == 0 ? "坦克" : j % 4 == 0 ? "手持设备" : "飞机";

                             cc.Value2 = j % 5 == 0 ? "通信" : j % 4 == 0 ? "雷达" : "敌我识别";

                             cc.Int1 = j;

                             temp.Children.Add(cc);
                         }

                         //this.Servers.Add(temp);

                         this.SelectedTask.Children.Add(temp);

                     }));
                 }
             });

            HeBianGu.General.WpfControlLib.Message.Instance.ShowSnackMessageWithNotice($"任务加载完成 <{this.SelectedTask.Value}>");
        }


        public List<FileInfo> GetFiles(string folder, Predicate<FileInfo> match)
        {
            List<FileInfo> result = new List<FileInfo>();

            DirectoryInfo directory = new DirectoryInfo(folder);

            var sys = directory.GetFileSystemInfos();

            if (sys == null) return null;

            foreach (var item in sys)
            {
                if (item is FileInfo file)
                {
                    if (match(file))
                        result.Add(file);
                }
                else if (item is DirectoryInfo dir)
                {
                    var find = this.GetFiles(dir.FullName, match);

                    if (find == null) continue;

                    result.AddRange(find);
                }
                else
                {
                    continue;
                }
            }

            return result;
        }
    }


    class ServerViewModel : TestViewModel
    {
        //public ServerViewModel()
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        this.Children.Add(new EquipmentViewModel() { Value = "设备_" + i });
        //    }
        //}

        public ServerViewModel()
        {
            this.Value9 = "2";
        }
    }

    public class TaskViewModel : TestViewModel
    {
        public TaskViewModel()
        {
            //this.Children.Add(new ServerGroupViewModel() { Value = "发生器列表" });
            //this.Children.Add(new EquipmentGroupViewModel() { Value = "设备列表" });

            //for (int i = 0; i < 4; i++)
            //{
            //    this.Children.Add(new ServerViewModel() { Value = "发生器_" + i });
            //}
        }
    }

    class EquipmentViewModel : TestViewModel
    {

    }
    class EquipmentGroupViewModel : TestViewModel
    {
        public EquipmentGroupViewModel()
        {
            for (int i = 0; i < 30; i++)
            {
                this.Children.Add(new EquipmentViewModel() { Value = "设备_" + i });
            }
        }
    }

    class ServerGroupViewModel : TestViewModel
    {
        public ServerGroupViewModel()
        {
            for (int i = 0; i < 4; i++)
            {
                this.Children.Add(new ServerViewModel() { Value = "发生器_" + i });
            }
        }
    }
}
