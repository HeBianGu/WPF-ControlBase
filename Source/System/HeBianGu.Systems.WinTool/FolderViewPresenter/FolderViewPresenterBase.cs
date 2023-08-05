using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace HeBianGu.Systems.WinTool
{
    public abstract class FolderViewPresenterBase<Setting, Interface> : ServiceMvpSettingBase<Setting, Interface> where Setting : class, Interface, new()
        where Interface : IInvokePresenter
    {
        public FolderViewPresenterBase()
        {
            this.Collection = this.CreateFiles().ToObservable();
        }

        public Predicate<string> FileFilter { get; set; }

        protected virtual IEnumerable<string> CreateFiles()
        {
            this.Folders = this.CreateFolders()?.ToObservable();
            this.Extension = this.CreateExtensions();
            var extensions = this.Extension?.Split(',');
            foreach (var item in this.Folders)
            {
                if (!Directory.Exists(item)) continue;

                var files = Directory.GetFiles(item);
                if (extensions != null)
                    files = files.Where(x => extensions.Any(e => Path.GetExtension(x).ToLower().EndsWith(e))).ToArray();
                foreach (var file in files)
                {
                    if (this.FileFilter?.Invoke(file) != false)
                        yield return file;
                }
            }
        }

        private Dock _dock;
        [DefaultValue(Dock.Right)]
        [Display(Name = "停靠方向")]
        public Dock Dock
        {
            get { return _dock; }
            set
            {
                _dock = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _folders = new ObservableCollection<string>();
        [Browsable(false)]
        public ObservableCollection<string> Folders
        {
            get { return _folders; }
            set
            {
                _folders = value;
                RaisePropertyChanged("Folders");
            }
        }

        private string _extensions;
        [DefaultValue("exe,bat")]
        [Display(Name = "扩展类型")]
        public string Extension
        {
            get { return _extensions; }
            set
            {
                _extensions = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedItem;
        [XmlIgnore]
        [Browsable(false)]
        public string SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _collection = new ObservableCollection<string>();
        [Browsable(false)]
        public ObservableCollection<string> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        protected virtual IEnumerable<string> CreateFolders()
        {
            return new ObservableCollection<string>();
        }

        protected virtual string CreateExtensions()
        {
            return this.Extension;
        }

        public RelayCommand OpenCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {
                //Process.Start(project);
                Process.Start(new ProcessStartInfo(project) { UseShellExecute = true });

            }
        });

        public RelayCommand DeleteCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {
                this.Collection.Remove(project);
            }
        });

        public RelayCommand AddFileCommand => new RelayCommand((s, e) =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //设置初始路径
            openFileDialog.Filter = "Excel文件(*.xls)|*.xls|Csv文件(*.csv)|*.csv|所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true) return;
            this.Collection.Add(openFileDialog.FileName);
        });

        public override bool Invoke(out string message)
        {
            message = null;
            //Process.Start(this.SelectedItem);
            return true;
        }


    }
}
