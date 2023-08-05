using HeBianGu.App.Scene.View.Dialog;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.Notify;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.App.Scene
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
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


        protected override async void Init()
        {


        }

        protected override async void Loaded(object args)
        {
            base.Loaded(args);

            //  Do ：加载场景
            Scenes.Clear();

            //this.Scenes.Add(new TestViewModel() { Value = "LicenseService", Value1 = @"E:\Github\HeBianGu.General.LicenseService" });
            //this.Scenes.Add(new TestViewModel() { Value = "Document", Value1 = @"E:\Document" });
            //this.Scenes.Add(new TestViewModel() { Value = "Nuget", Value1 = @"E:\Nuget" });
            //this.Scenes.Add(new TestViewModel() { Value = "WPF-ControlBase-master", Value1 = @"E:\Github\WPF-ControlBase-master" });

            Scenes.Add(new TestViewModel() { Value = "Recent", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Recent) });
            Scenes.Add(new TestViewModel() { Value = "Music", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic) });
            Scenes.Add(new TestViewModel() { Value = "CommonPictures", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures) });
            Scenes.Add(new TestViewModel() { Value = "CommonVideos", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos) });
            Scenes.Add(new TestViewModel() { Value = "History", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.History) });
            Scenes.Add(new TestViewModel() { Value = "Cookies", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) });
            Scenes.Add(new TestViewModel() { Value = "PrinterShortcuts", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts) });
            //this.Scenes.Add(new TestViewModel() { Value = "ApplicationData", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) });
            Scenes.Add(new TestViewModel() { Value = "Templates", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Templates) });
            Scenes.Add(new TestViewModel() { Value = "Fonts", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) });
            Scenes.Add(new TestViewModel() { Value = "NetworkShortcuts", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts) });
            Scenes.Add(new TestViewModel() { Value = "DesktopDirectory", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) });

            SelectedScene = Scenes?.FirstOrDefault();

            CurrentScene = Scenes?.FirstOrDefault();

            //  Do ：加载模板

            Templates.Clear();

            Templates.Add(new TestViewModel() { Value = "所有文件", Value1 = "" });
            Templates.Add(new TestViewModel() { Value = "解决方案", Value1 = ".sln" });
            Templates.Add(new TestViewModel() { Value = "视频", Value1 = ".avi .mp4 .wav .wmv" });
            Templates.Add(new TestViewModel() { Value = "音频", Value1 = ".mp3 .wma" });
            Templates.Add(new TestViewModel() { Value = "图像", Value1 = ".jpg ,png .gif .ico .svg" });
            Templates.Add(new TestViewModel() { Value = "办公文件", Value1 = ".xlsx .docx. xls .ppt" });

            SelectedTemplate = Templates?.FirstOrDefault();

            CurrentTempalte = Templates?.FirstOrDefault();

            await RefreshScene();
        }


        public RelayCommand RelayCommand => new RelayCommand(l =>
        {
            RelayMethod(l);
        });

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            if (command == "新建场景")
            {
                SelectTypeControl select = new SelectTypeControl();

                bool result = await Messager.Instance.ShowDialog<bool>(select);

                if (!result) return;

                ConfigSceneControl config = new ConfigSceneControl
                {
                    Model = new TestViewModel
                    {
                        Value3 = select.Model?.Value,

                        Value4 = select.Model?.Value1
                    }
                };

                bool? result1 = await Messager.Instance.ShowDialog<bool?>(config);

                //  Do ：取消
                if (result1 == false) return;

                //  Do ：上一步
                if (result1 == null)
                {
                    RelayMethod(command); return;
                }

                //  Do ：创建
                if (string.IsNullOrEmpty(config.Model.Value) || string.IsNullOrEmpty(config.Model.Value1))
                {
                    MessageProxy.Snacker.ShowTime("数据不合法"); return;

                }
                Scenes.Add(config.Model);

                CurrentTempalte = select.Model;

                CurrentScene = config.Model;

                await RefreshScene();

            }
            else if (command == "ListBox.MouseDoubleClick.SetSelectedScene")
            {
                CurrentScene = SelectedScene;

                await RefreshScene();
            }
            else if (command == "ListBox.MouseDoubleClick.SetSelectedTemplate")
            {
                CurrentTempalte = SelectedTemplate;

                await RefreshScene();
            }
            else if (command == "Button.Click.Commit")
            {
                if (string.IsNullOrEmpty(Title))
                {
                    MessageProxy.Snacker.ShowTime("请输入标题信息");
                    return;
                }

                if (Historys.FirstOrDefault(l => l.Value == Title) != null)
                {
                    MessageProxy.Snacker.ShowTime("存在重复的标题");
                    return;
                }

                await Task.Run(() =>
                 {
                     for (int i = 0; i <= 100; i++)
                     {
                         Percent = i;

                         Thread.Sleep(50);
                     }
                 });

                MessageProxy.Snacker.ShowTime("提交成功");

                Historys.Add(new TestViewModel() { Value = Title });

            }
        }

        private async Task RefreshScene()
        {
            if (!Directory.Exists(CurrentScene.Value1))
            {
                MessageProxy.Snacker.ShowTime("场景路径不存在，请检查"); return;
            }

            Files.Clear();

            ManualResetEvent waitHandle = new ManualResetEvent(false);

            await MessageProxy.Notify.ShowProgress<bool>(l =>
             {
                 l.Message = "读取文件中...";

                 string[] extends = SelectedTemplate.Value1?.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                 Predicate<FileInfo> match = m =>
                   {
                       if (extends == null || extends.Count() == 0) return true;

                       return extends.Count(k => k == m.Extension) > 0;

                   };

                 List<FileInfo> files = GetFiles(CurrentScene.Value1, match);

                 if (files == null || files.Count == 0) return false;

                 l.Message = "加载文件中...";

                 for (int i = 0; i < files.Count; i++)
                 {
                     FileInfo item = files[i];

                     TestViewModel model = new TestViewModel
                     {
                         Value = item.Name,

                         Value1 = item.FullName,

                         Value2 = item.Extension,

                         Int1 = i
                     };

                     //System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                     //{
                     //    Files.Add(model);

                     //    l.Value = double.Parse(model.Int1.ToString()) * 100 / double.Parse((files.Count - 1).ToString());

                     //    if (model.Int1 == files.Count - 1)
                     //    {
                     //        waitHandle.Set();
                     //    }
                     //}));



                     Application.Current.Dispatcher.Invoke(() =>
                     {
                         Files.Add(model);
                         l.Value = double.Parse(model.Int1.ToString()) * 100 / double.Parse((files.Count - 1).ToString());
                     });


                     //if (model.Int1 == files.Count - 1)
                     //{
                     //    waitHandle.Set();
                     //}
                 }

                 //waitHandle.WaitOne();

                 this.SelectedFile = this.Files?.FirstOrDefault();
                 return true;
             });

            MessageProxy.Snacker.ShowTime($"场景加载完成 <{CurrentScene.Value}>");
        }


        public List<FileInfo> GetFiles(string folder, Predicate<FileInfo> match)
        {
            List<FileInfo> result = new List<FileInfo>();

            DirectoryInfo directory = new DirectoryInfo(folder);

            FileSystemInfo[] sys = directory.GetFileSystemInfos();

            if (sys == null) return null;

            foreach (FileSystemInfo item in sys)
            {
                if (item is FileInfo file)
                {
                    if (match(file))
                        result.Add(file);
                }
                else if (item is DirectoryInfo dir)
                {
                    List<FileInfo> find = GetFiles(dir.FullName, match);

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
}
