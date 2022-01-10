using HeBianGu.Application.MapWindow.View.Dialog;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.Link;
using HeBianGu.Window.Notify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace HeBianGu.Application.MapWindow
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

        private ObservableCollection<TestViewModel> _scene = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Scenes
        {
            get { return _scene; }
            set
            {
                _scene = value;
                RaisePropertyChanged("Scenes");
            }
        }

        private TestViewModel _selectedScene;
        /// <summary> 说明  </summary>
        public TestViewModel SelectedScene
        {
            get { return _selectedScene; }
            set
            {
                _selectedScene = value;
                RaisePropertyChanged("SelectedScene");
            }
        }

        private TestViewModel _currentScene;
        /// <summary> 说明  </summary>
        public TestViewModel CurrentScene
        {
            get { return _currentScene; }
            set
            {
                _currentScene = value;
                RaisePropertyChanged("CurrentScene");
            }
        }


        private ObservableCollection<TestViewModel> _files = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Files
        {
            get { return _files; }
            set
            {
                _files = value;
                RaisePropertyChanged("Files");
            }
        }


        private ObservableCollection<TestViewModel> _equipments = new ObservableCollection<TestViewModel>();
        /// <summary> 装备列表  </summary>
        public ObservableCollection<TestViewModel> Equipments
        {
            get { return _equipments; }
            set
            {
                _equipments = value;
                RaisePropertyChanged("Equipments");
            }
        }

        private TestViewModel _selectedEquipment;
        /// <summary> 说明  </summary>
        public TestViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                RaisePropertyChanged("SelectedEquipment");
            }
        }


        private ObservableCollection<ServerViewModel> _servers = new ObservableCollection<ServerViewModel>();
        /// <summary> 位置列表  </summary>
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
        /// <summary> 显示装备属性  </summary>
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
        /// <summary> 显示位置属性  </summary>
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
            //  Do ：测试曲线
            List<Func<double, double>> waves = new List<Func<double, double>>();

            waves.Add(l => 1 * Math.Sin(l));
            waves.Add(l => 2 * Math.Sin(l / 2));
            waves.Add(l => 3 * Math.Sin(l * 2));
            waves.Add(l => 4 * Math.Sin(l / 3));
            waves.Add(l => 5 * Math.Sin(l * 3));
            waves.Add(l => 1 * Math.Sin(l / 4));
            waves.Add(l => 2 * Math.Sin(l * 4));
            waves.Add(l => 3 * Math.Sin(l / 5));
            waves.Add(l => 4 * Math.Sin(l * 5));
            waves.Add(l => 5 * Math.Sin(l / 6));
            waves.Add(l => 1 * Math.Sin(l * 6));
            waves.Add(l => 2 * Math.Sin(l / 7));
            waves.Add(l => 3 * Math.Sin(l * 7));
            waves.Add(l => 4 * Math.Sin(l / 8));
            waves.Add(l => 5 * Math.Sin(l * 8));

            waves.Add(l => 1 * Math.Cos(l));
            waves.Add(l => 2 * Math.Cos(l / 2));
            waves.Add(l => 3 * Math.Cos(l * 2));
            waves.Add(l => 4 * Math.Cos(l / 3));
            waves.Add(l => 5 * Math.Cos(l * 3));
            waves.Add(l => 1 * Math.Cos(l / 4));
            waves.Add(l => 2 * Math.Cos(l * 4));
            waves.Add(l => 3 * Math.Cos(l / 5));
            waves.Add(l => 4 * Math.Cos(l * 5));
            waves.Add(l => 5 * Math.Cos(l / 6));
            waves.Add(l => 1 * Math.Cos(l * 6));
            waves.Add(l => 2 * Math.Cos(l / 7));
            waves.Add(l => 3 * Math.Cos(l * 7));
            waves.Add(l => 4 * Math.Cos(l / 8));
            waves.Add(l => 5 * Math.Cos(l * 8));

            double fz = 150;

            double center = 1800;

            double weight = 90;

            double offsite = 90;


            Task.Run(() =>
            {
                double param = -5;

                while (true)
                {
                    List<double> data = new List<double>();

                    List<double> data1 = new List<double>();

                    List<double> data2 = new List<double>();

                    List<double> axis = new List<double>();

                    for (double i = 0; i <= 360 * 0.2; i = i + 1)
                    {
                        int count = random.Next(3, waves.Count);

                        var find = Enumerable.Range(0, count).Select(l => waves[l])?.ToList();

                        Func<double, double> func = l =>
                        {
                            double result = 0;

                            find.ForEach(k => result = result + k.Invoke(l));

                            return result;
                        };

                        axis.Add(i);

                        double t = i / 360 * Math.PI;

                        double value = func(t);

                        data.Add(func(t));

                        data1.Add(Math.Sin((i * param) / 360 * Math.PI) * 50);

                        double wave = i > center - weight && i < center + weight ? Math.Sin(((i + offsite) * 2) / 360 * Math.PI) * fz : 0;

                        data2.Add(wave + value);
                    }

                    this.WaveyAxis = axis.ToObservable();

                    this.WaveData = data.ToObservable();

                    param = param + 0.01;

                    if (param > 5) param = -5;

                    Thread.Sleep(100);

                }
            });

            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Application.Current.Dispatcher.Invoke(() =>
            //        {
            //            this.LogMessages.Insert(0, new TestViewModel() { Value = "加载成功", Value2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Value3 = "刷新成功，总计360条数据" });

            //            if (this.LogMessages.Count > 100)
            //            {
            //                this.LogMessages.RemoveAt(99);
            //            }

            //        });

            //        Thread.Sleep(5000);
            //    }
            //});
        }

        protected override async void Loaded(string args)
        {
            base.Loaded(args);

            //  Do ：加载任务
            this.Scenes.Clear();

            //this.Scenes.Add(new TestViewModel() { Value = "LicenseService", Value1 = @"E:\Github\HeBianGu.General.LicenseService" });
            //this.Scenes.Add(new TestViewModel() { Value = "Document", Value1 = @"E:\Document" });
            //this.Scenes.Add(new TestViewModel() { Value = "Nuget", Value1 = @"E:\Nuget" });
            //this.Scenes.Add(new TestViewModel() { Value = "WPF-ControlBase-master", Value1 = @"E:\Github\WPF-ControlBase-master" });

            this.Scenes.Add(new TestViewModel() { Value = "任务一", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Recent) });
            this.Scenes.Add(new TestViewModel() { Value = "任务二", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic) });
            this.Scenes.Add(new TestViewModel() { Value = "任务三", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures) });
            this.Scenes.Add(new TestViewModel() { Value = "任务四", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos) });
            this.Scenes.Add(new TestViewModel() { Value = "任务五", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.History) });
            this.Scenes.Add(new TestViewModel() { Value = "任务六", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) });
            this.Scenes.Add(new TestViewModel() { Value = "任务七", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts) });
            //this.Scenes.Add(new TestViewModel() { Value = "ApplicationData", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) });
            this.Scenes.Add(new TestViewModel() { Value = "任务八", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Templates) });
            this.Scenes.Add(new TestViewModel() { Value = "任务九", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) });
            this.Scenes.Add(new TestViewModel() { Value = "任务十", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts) });
            this.Scenes.Add(new TestViewModel() { Value = "任务十一", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) });

            this.SelectedScene = this.Scenes?.FirstOrDefault();

            this.CurrentScene = this.Scenes?.FirstOrDefault();

            //  Do ：加载模板

            this.Templates.Clear();

            this.Templates.Add(new TestViewModel() { Value = "空", Value1 = "" });
            this.Templates.Add(new TestViewModel() { Value = "解决方案", Value1 = ".sln" });
            this.Templates.Add(new TestViewModel() { Value = "视频", Value1 = ".avi .mp4 .wav .wmv" });
            this.Templates.Add(new TestViewModel() { Value = "音频", Value1 = ".mp3 .wma" });
            this.Templates.Add(new TestViewModel() { Value = "图像", Value1 = ".jpg ,png .gif .ico .svg" });
            this.Templates.Add(new TestViewModel() { Value = "办公文件", Value1 = ".xlsx .docx. xls .ppt" });

            this.SelectedTemplate = this.Templates?.FirstOrDefault();

            this.CurrentTempalte = this.Templates?.FirstOrDefault();

            await this.RefreshScene();
        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            if (command == "新建任务")
            {
                SelectTypeControl select = new SelectTypeControl();

                var result = await Message.Instance.ShowCustomDialog<bool>(select);

                if (!result) return;

                ConfigSceneControl config = new ConfigSceneControl();

                config.Model = new TestViewModel();

                config.Model.Value3 = select.Model?.Value;

                config.Model.Value4 = select.Model?.Value1;

                var result1 = await Message.Instance.ShowCustomDialog<bool?>(config);

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
                    Message.Instance.ShowSnackMessageWithNotice("数据不合法"); return;

                }
                this.Scenes.Add(config.Model);

                this.CurrentTempalte = select.Model;

                this.CurrentScene = config.Model;

                await this.RefreshScene();

            }
            else if (command == "ListBox.MouseDoubleClick.SetSelectedScene")
            {
                this.CurrentScene = this.SelectedScene;

                await this.RefreshScene();
            }
            else if (command == "ListBox.MouseDoubleClick.SetSelectedTemplate")
            {
                this.CurrentTempalte = this.SelectedTemplate;

                await this.RefreshScene();
            }
            else if (command == "Button.Click.Commit")
            {
                //if (string.IsNullOrEmpty(this.Title))
                //{
                //    MessageService.ShowSnackMessageWithNotice("请输入标题信息");
                //    return;
                //}

                //if (this.Historys.FirstOrDefault(l => l.Value == this.Title) != null)
                //{
                //    MessageService.ShowSnackMessageWithNotice("存在重复的标题");
                //    return;
                //}

                await Task.Run(() =>
                 {
                     for (int i = 0; i <= 100; i++)
                     {
                         this.Percent = i;

                         Thread.Sleep(50);
                     }
                 });

                Message.Instance.ShowSnackMessageWithNotice("提交成功");

                this.Historys.Add(new TestViewModel() { Value = this.Title });

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
            if (!Directory.Exists(this.CurrentScene.Value1))
            {
                Message.Instance.ShowSnackMessageWithNotice("任务路径不存在，请检查"); return;
            }

            this.Files.Clear();

            this.Equipments.Clear();

            this.Servers.Clear();

            ManualResetEvent waitHandle = new ManualResetEvent(false);

            await NotifyMessageService.ShowWinProgressBarMessage<bool>(l =>
             {
                 l.Message = "读取文件中...";

                 var extends = this.SelectedTemplate.Value1?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                 Predicate<FileInfo> match = m =>
                   {
                       if (extends == null || extends.Count() == 0) return true;

                       return extends.Count(k => k == m.Extension) > 0;

                   };

                 var files = this.GetFiles(this.CurrentScene.Value1, match);

                 if (files == null || files.Count == 0)
                 {
                     waitHandle.Set();

                     return false;

                 }

                 l.Message = "加载文件中...";

                 for (int i = 0; i < files.Count; i++)
                 {
                     var item = files[i];

                     {
                         TestViewModel model = new TestViewModel();

                         model.Value = "装备_" + i.ToString();

                         model.Value1 = i % 5 == 0 ? "坦克" : i % 4 == 0 ? "手持设备" : "飞机";

                         model.Value2 = i % 5 == 0 ? "通信" : i % 4 == 0 ? "雷达" : "敌我识别";

                         model.Int1 = i;

                         System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                         {
                             this.Equipments.Add(model);

                             l.Value = double.Parse(model.Int1.ToString()) * 100 / double.Parse((files.Count - 1).ToString());

                             if (model.Int1 == files.Count - 1)
                             {
                                 waitHandle.Set();
                             }
                         }));
                     }

                     {
                         if (i < 10 || (i > 30 && i < 35))
                         {
                             ServerViewModel model = new ServerViewModel();

                             model.Value = "位置_" + i.ToString();

                             model.Value1 = "192.168.0." + i.ToString();

                             model.Value2 = i % 5 == 0 ? "生成中" : i % 4 == 0 ? "未连接" : i % 3 == 0 ? "故障" : "空闲";

                             model.Int1 = i;

                             System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                             {
                                 int c = random.Next(29, 30);

                                 foreach (var ccc in this.Equipments.Take(c))
                                 {
                                     model.Children.Add(ccc);
                                 }

                                 this.Servers.Add(model);

                             }));
                         }
                     }

                     {
                         TestViewModel model = new TestViewModel();

                         model.Value = item.Name;

                         model.Value1 = item.FullName;

                         model.Value2 = item.Extension;

                         model.Int1 = i;

                         System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                         {
                             this.Files.Add(model);
                         }));
                     }
                 }

                 waitHandle.WaitOne();

                 return true;
             });

            Message.Instance.ShowSnackMessageWithNotice($"任务加载完成 <{this.CurrentScene.Value}>");
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

    }
}
