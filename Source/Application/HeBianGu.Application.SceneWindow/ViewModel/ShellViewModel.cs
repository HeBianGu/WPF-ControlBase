using HeBianGu.Application.SceneWindow.View.Dialog;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
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

namespace HeBianGu.Application.SceneWindow
{
    class ShellViewModel : WindowBaseNotifyPropertyChanged
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

        protected override async void Loaded(string args)
        {
            base.Loaded(args);

            //  Do ：加载场景
            this.Scenes.Clear();

            //this.Scenes.Add(new TestViewModel() { Value = "LicenseService", Value1 = @"E:\Github\HeBianGu.General.LicenseService" });
            //this.Scenes.Add(new TestViewModel() { Value = "Document", Value1 = @"E:\Document" });
            //this.Scenes.Add(new TestViewModel() { Value = "Nuget", Value1 = @"E:\Nuget" });
            //this.Scenes.Add(new TestViewModel() { Value = "WPF-ControlBase-master", Value1 = @"E:\Github\WPF-ControlBase-master" });

            this.Scenes.Add(new TestViewModel() { Value = "Recent", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Recent) });
            this.Scenes.Add(new TestViewModel() { Value = "Music", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic) });
            this.Scenes.Add(new TestViewModel() { Value = "CommonPictures", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures) });
            this.Scenes.Add(new TestViewModel() { Value = "CommonVideos", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos) });
            this.Scenes.Add(new TestViewModel() { Value = "History", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.History) });
            this.Scenes.Add(new TestViewModel() { Value = "Cookies", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) });
            this.Scenes.Add(new TestViewModel() { Value = "PrinterShortcuts", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.PrinterShortcuts) });
            //this.Scenes.Add(new TestViewModel() { Value = "ApplicationData", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) });
            this.Scenes.Add(new TestViewModel() { Value = "Templates", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Templates) });
            this.Scenes.Add(new TestViewModel() { Value = "Fonts", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.Fonts) });
            this.Scenes.Add(new TestViewModel() { Value = "NetworkShortcuts", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.NetworkShortcuts) });
            this.Scenes.Add(new TestViewModel() { Value = "DesktopDirectory", Value1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) });

            this.SelectedScene = this.Scenes?.FirstOrDefault();

            this.CurrentScene = this.Scenes?.FirstOrDefault();

            //  Do ：加载模板

            this.Templates.Clear();

            this.Templates.Add(new TestViewModel() { Value = "所有文件", Value1 = "" });
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

            if (command == "新建场景")
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
                if (string.IsNullOrEmpty(this.Title))
                {
                    Message.Instance.ShowSnackMessageWithNotice("请输入标题信息");
                    return;
                }

                if (this.Historys.FirstOrDefault(l => l.Value == this.Title) != null)
                {
                    Message.Instance.ShowSnackMessageWithNotice("存在重复的标题");
                    return;
                }

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
        }

        async Task RefreshScene()
        {
            if (!Directory.Exists(this.CurrentScene.Value1))
            {
                Message.Instance.ShowSnackMessageWithNotice("场景路径不存在，请检查"); return;
            }

            this.Files.Clear();

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

                 if (files == null || files.Count == 0) return false;

                 l.Message = "加载文件中...";

                 for (int i = 0; i < files.Count; i++)
                 {
                     var item = files[i];

                     TestViewModel model = new TestViewModel();

                     model.Value = item.Name;

                     model.Value1 = item.FullName;

                     model.Value2 = item.Extension;

                     model.Int1 = i;

                     System.Windows.Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                     {
                         this.Files.Add(model);

                         l.Value = double.Parse(model.Int1.ToString()) * 100 / double.Parse((files.Count - 1).ToString());

                         if (model.Int1 == files.Count - 1)
                         {
                             waitHandle.Set();
                         }
                     }));
                 }

                 waitHandle.WaitOne();

                 return true;
             });

            Message.Instance.ShowSnackMessageWithNotice($"场景加载完成 <{this.CurrentScene.Value}>");
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
}
