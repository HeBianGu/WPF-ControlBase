// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Windows.Shapes;

namespace HeBianGu.Control.Explorer
{
    public class Explorer : DataGrid
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Explorer), "S.Explorer.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(Explorer), "S.Explorer.Single");

        static Explorer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Explorer), new FrameworkPropertyMetadata(typeof(Explorer)));

            StyleLoader.Instance?.LoadDefault(typeof(Explorer));
        }

        public static RoutedUICommand Next = new RoutedUICommand() { Text = "下一项" };

        public static RoutedUICommand Previous = new RoutedUICommand() { Text = "上一项" };

        public DirectoryModel HistorySelectedItem
        {
            get { return (DirectoryModel)GetValue(HistorySelectedItemProperty); }
            set { SetValue(HistorySelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HistorySelectedItemProperty =
            DependencyProperty.Register("HistorySelectedItem", typeof(DirectoryModel), typeof(Explorer), new PropertyMetadata(default(DirectoryModel), (d, e) =>
             {
                 Explorer control = d as Explorer;

                 if (control == null) return;

                 DirectoryModel config = e.NewValue as DirectoryModel;

                 if (control.CurrentPath != config?.Model.FullName)
                 {
                     control._isHistoryRefresh = true;

                     control.CurrentPath = config?.Model.FullName;

                 }

             }));

        //  Do ：勾选的历史记录不再添加历史记录
        private bool _isHistoryRefresh = false;

        public Explorer()
        {
            {
                CommandBinding binding = new CommandBinding(Next, (l, k) =>
                {
                    int index = this.History.IndexOf(HistorySelectedItem);
                    this.HistorySelectedItem = this.History[index + 1];

                }, (l, k) =>
                 {
                     int index = this.History.IndexOf(HistorySelectedItem);
                     k.CanExecute = index < this.History.Count - 1;
                 });
                this.CommandBindings.Add(binding);
            }
            {
                CommandBinding binding = new CommandBinding(Previous, (l, k) =>
                {
                    int index = this.History.IndexOf(HistorySelectedItem);
                    this.HistorySelectedItem = this.History[index - 1];
                }, (l, k) =>
                {
                    int index = this.History.IndexOf(HistorySelectedItem);
                    k.CanExecute = index > 0;
                });
                this.CommandBindings.Add(binding);
            }
            this.RefreshPath(null);
        }

        public ObservableCollection<DirectoryModel> History
        {
            get { return (ObservableCollection<DirectoryModel>)GetValue(HistoryProperty); }
            set { SetValue(HistoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HistoryProperty =
            DependencyProperty.Register("History", typeof(ObservableCollection<DirectoryModel>), typeof(Explorer), new PropertyMetadata(new ObservableCollection<DirectoryModel>(), (d, e) =>
             {
                 Explorer control = d as Explorer;
                 if (control == null)
                     return;
                 ObservableCollection<DirectoryModel> config = e.NewValue as ObservableCollection<DirectoryModel>;
             }));

        public bool NavigationBarEnbled
        {
            get { return (bool)GetValue(NavigationBarEnbledProperty); }
            set { SetValue(NavigationBarEnbledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationBarEnbledProperty =
            DependencyProperty.Register("NavigationBarEnbled", typeof(bool), typeof(Explorer), new PropertyMetadata(default(bool), (d, e) =>
             {
                 Explorer control = d as Explorer;
                 if (control == null)
                     return;
                 //bool config = e.NewValue as bool;

             }));

        public Visibility HeaderVisibility
        {
            get { return (Visibility)GetValue(HeaderVisibilityProperty); }
            set { SetValue(HeaderVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderVisibilityProperty =
            DependencyProperty.Register("HeaderVisibility", typeof(Visibility), typeof(Explorer), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Explorer control = d as Explorer;

                 if (control == null) return;

                 if (e.OldValue is Visibility o)
                 {

                 }

                 if (e.NewValue is Visibility n)
                 {

                 }

             }));



        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(Explorer), new PropertyMetadata(default(string), (d, e) =>
             {
                 Explorer control = d as Explorer;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 control.RefreshPath(config);

                 if (!control._isHistoryRefresh)
                 {
                     control.RefreshHistory(config);
                 }

                 //  Do ：回退状态
                 control._isHistoryRefresh = false;

             }));

        /// <summary>
        /// 搜索通配符
        /// </summary>
        public string AllSearchPattern
        {
            get { return (string)GetValue(AllSearchPatternProperty); }
            set { SetValue(AllSearchPatternProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllSearchPatternProperty =
            DependencyProperty.Register("AllSearchPattern", typeof(string), typeof(Explorer), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Explorer control = d as Explorer;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }



                 control.RefreshPath(control.CurrentPath);

             }));


        /// <summary>
        /// 文件通配符
        /// </summary>
        public string SearchPattern
        {
            get { return (string)GetValue(SearchPatternProperty); }
            set { SetValue(SearchPatternProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchPatternProperty =
            DependencyProperty.Register("SearchPattern", typeof(string), typeof(Explorer), new FrameworkPropertyMetadata("*", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Explorer control = d as Explorer;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }
             }));


        public Visibility SearchBoxVisibility
        {
            get { return (Visibility)GetValue(SearchBoxVisibilityProperty); }
            set { SetValue(SearchBoxVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchBoxVisibilityProperty =
            DependencyProperty.Register("SearchBoxVisibility", typeof(Visibility), typeof(Explorer), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 Explorer control = d as Explorer;

                 if (control == null) return;

                 if (e.OldValue is Visibility o)
                 {

                 }

                 if (e.NewValue is Visibility n)
                 {

                 }

             }));

        private void RefreshPath(string path)
        {
            List<SystemInfoModel> from = new List<SystemInfoModel>();
            var dir1 = ExplorerProxy.Instance.CreateDirectoryInfo(path);
            if (!ExplorerProxy.Instance.ExistsDirectoryInfo(dir1))
            //if (!Directory.Exists(path))
            {
                //DriveInfo[] drives = DriveInfo.GetDrives();
                //from.AddRange(drives.Select(l => new DirectoryModel(l.RootDirectory)));

                var roots = ExplorerProxy.Instance.GetDrives().Select(x => new DirectoryModel(x));
                from.AddRange(roots);
            }
            else
            {
                //DirectoryInfo dir = new DirectoryInfo(path);
                //if (string.IsNullOrEmpty(this.AllSearchPattern))
                //{
                //IEnumerable<DirectoryInfo> directories = dir.GetDirectories().Where(l => !l.Attributes.HasFlag(FileAttributes.System));
                var dir = ExplorerProxy.Instance.CreateDirectoryInfo(path);
                var directories = ExplorerProxy.Instance.GetDirectories(dir);
                from.AddRange(directories.Select(l => new DirectoryModel(l)));
                //IEnumerable<FileInfo> files = dir.GetFiles(this.SearchPattern ?? "*").Where(l => !l.Attributes.HasFlag(FileAttributes.System));
                var files = ExplorerProxy.Instance.GetFiles(dir);
                from.AddRange(files.Select(l => new FileModel(l)));
                //}
                //else
                //{
                //    IEnumerable<FileSystemInfo> systemInfos = dir.GetFileSystemInfos(this.AllSearchPattern, SearchOption.AllDirectories).Where(l => !l.Attributes.HasFlag(FileAttributes.System));
                //    from.AddRange(systemInfos.OfType<DirectoryInfo>().Select(l => new DirectoryModel(l)));
                //    from.AddRange(systemInfos.OfType<FileInfo>().Select(l => new FileModel(l)));
                //}
            }
            this.ItemsSource = from.ToObservable();
        }

        private void RefreshHistory(string path)
        {
            //if (!Directory.Exists(path))
            //    return;
            var dir = ExplorerProxy.Instance.CreateDirectoryInfo(path);

            if (!ExplorerProxy.Instance.ExistsDirectoryInfo(dir))
                return;
            //DirectoryInfo dir = new DirectoryInfo(path);
            if (this.History.FirstOrDefault()?.Model.FullName == path)
                return;
            //var dir = ExplorerProxy.Instance.CreateDirectoryInfo(path);
            //  Do ：加载历史记录，获取后十个
            this.History.Insert(0, new DirectoryModel(dir));
            this.History = this.History.Take(ExplorerSetting.Instance.HistCapacity).ToObservable();
            this.HistorySelectedItem = this.History?.FirstOrDefault();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (this.SelectedItem is DirectoryModel model)
                this.CurrentPath = model.Model?.FullName;
        }
    }

    public class NavigationBar : ListBox
    {
        static NavigationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavigationBar), new FrameworkPropertyMetadata(typeof(NavigationBar)));
        }

        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(NavigationBar), new PropertyMetadata("我的电脑", (d, e) =>
             {
                 NavigationBar control = d as NavigationBar;
                 if (control == null)
                     return;
                 string config = e.NewValue as string;
                 control.RefreshData(config);

             }));

        private void RefreshData(string path)
        {
            List<IDirectoryInfo> dirs = new List<IDirectoryInfo>();
            var dir = ExplorerProxy.Instance.CreateDirectoryInfo(path);
            if (!ExplorerProxy.Instance.ExistsDirectoryInfo(dir))
            {
                var root = ExplorerProxy.Instance.CreateDirectoryInfo("我的电脑");
                dirs.Add(root);
            }
            else
            {
                //DirectoryInfo dir = new DirectoryInfo(path);
                //var dir = ExplorerProxy.Instance.CreateDirectoryInfo(path);
                Action<IDirectoryInfo> action = null;
                action = l =>
                {
                    var parent = ExplorerProxy.Instance.GetParentDirectory(l);
                    if (parent == null)
                        return;
                    //if (l.Parent == null)
                    //    return;
                    dirs.Add(parent);
                    action(parent);
                };

                dirs.Add(dir);
                action.Invoke(dir);
                dirs.Add(ExplorerProxy.Instance.CreateDirectoryInfo("我的电脑"));
                dirs.Reverse();
            }
            this.ItemsSource = dirs.Select(l => new DirectoryModel(l));

            //List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            //if (!ExplorerProxy.Instance.ExistsDirectoryInfo(path))
            //{
            //    dirs.Add(new DirectoryInfo("我的电脑"));
            //}
            //else
            //{
            //    DirectoryInfo dir = new DirectoryInfo(path);
            //    Action<DirectoryInfo> action = null;
            //    action = l =>
            //    {
            //        if (l.Parent == null)
            //            return;
            //        dirs.Add(l.Parent);
            //        action(l.Parent);
            //    };

            //    dirs.Add(dir);
            //    action.Invoke(dir);
            //    dirs.Add(new DirectoryInfo("我的电脑"));
            //    dirs.Reverse();
            //}
            //this.ItemsSource = dirs.Select(l => new DirectoryModel(l));
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
            if (this.SelectedItem is DirectoryModel dir)
                this.CurrentPath = dir.Model.FullName;
        }
    }

    /// <summary> 说明</summary>
    public class SystemInfoModel : NotifyPropertyChanged
    {
        private string _icon;
        /// <summary> 说明  </summary>
        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged("Icon");
            }
        }


        private string _displayName;
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }

        private bool _isChecked;
        /// <summary> 说明  </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
            }
        }
    }

    /// <summary> 说明</summary>
    public class SystemInfoModel<T> : SystemInfoModel where T : ISystemFileInfo
    {
        private T _model;
        /// <summary> 说明  </summary>
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        public SystemInfoModel(T model)
        {
            this.Model = model;
            this.DisplayName = model.Name;
        }

        public SystemInfoModel()
        {

        }

        /// <summary> 图片路径 </summary>
        public System.Drawing.Icon Logo
        {
            get
            {
                return IconHelper.Instance.GetSystemInfoIcon(this.Model?.FullName);
            }
        }
    }

    /// <summary> 说明</summary>
    public class FileModel : SystemInfoModel<IFileInfo>
    {
        public FileModel(IFileInfo model) : base(model)
        {
            this.Icon = "\xe7fe";
        }
    }

    /// <summary> 说明</summary>
    [TypeConverter(typeof(DirectoryModelTypeConverter))]
    public class DirectoryModel : SystemInfoModel<IDirectoryInfo>
    {
        public DirectoryModel(IDirectoryInfo model) : base(model)
        {
            this.Icon = "\xe7fe";
        }

        public DirectoryModel()
        {

        }

        public DirectoryModel(string path) : base(ExplorerProxy.Instance.CreateDirectoryInfo(path))
        {

        }

        public DirectoryModel(Environment.SpecialFolder special) : base(ExplorerProxy.Instance.CreateDirectoryInfo(Environment.GetFolderPath(special)))
        {

        }

        private ObservableCollection<SystemInfoModel> _collection = new ObservableCollection<SystemInfoModel>();
        /// <summary> 子节点  </summary>
        public ObservableCollection<SystemInfoModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private bool _isExpanded;
        /// <summary> 是否展开  </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;

                if (value == true)
                {
                    this.RefreshGrandson();
                }

                RaisePropertyChanged("IsExpanded");
            }
        }


        private bool _fileInfoVisible = true;
        /// <summary> 是否显示文件  </summary>
        public bool FileInfoVisible
        {
            get { return _fileInfoVisible; }
            set
            {
                _fileInfoVisible = value;
                RaisePropertyChanged("FileInfoVisible");
            }
        }

        private bool _isBuzy;
        /// <summary> 说明  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }
        /// <summary> 刷新子节点 </summary>
        public void RefreshChildren()
        {
            this.Collection.Clear();
            if (this.Model == null)
                return;
            Task.Run(() =>
             {
                 List<SystemInfoModel> from = new List<SystemInfoModel>();
                 //if (!this.Model.Exists)
                 //    return;
                 //IEnumerable<FileSystemInfo> all = this.Model.GetFileSystemInfos().Where(l => !(l.Attributes.HasFlag(FileAttributes.System) || l.Attributes.HasFlag(FileAttributes.Hidden)));
                 //from.AddRange(all.OfType<DirectoryInfo>().Select(l => new DirectoryModel(l) { FileInfoVisible = this.FileInfoVisible }));
                 //if (this.FileInfoVisible)
                 //    from.AddRange(all.OfType<FileInfo>().Select(l => new FileModel(l)));
                 var dir = ExplorerProxy.Instance.CreateDirectoryInfo(this.Model?.FullName);
                 var dirs = ExplorerProxy.Instance.GetDirectories(dir);
                 var files = ExplorerProxy.Instance.GetFiles(dir);
                 from.AddRange(dirs.Select(l => new DirectoryModel(l) { FileInfoVisible = this.FileInfoVisible }));
                 if (this.FileInfoVisible)
                     from.AddRange(files.Select(l => new FileModel(l)));

                 Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                  {
                      this.Collection = from?.ToObservable();
                  }));
             });
        }

        /// <summary> 刷新孙子节点 </summary>
        public void RefreshGrandson()
        {
            IEnumerable<DirectoryModel> dir = this.Collection.OfType<DirectoryModel>();

            foreach (DirectoryModel child in dir)
            {
                child.RefreshChildren();
            }
        }
    }

    public class RootModelCollection : ObservableCollection<RootModel>
    {

    }

    [ContentProperty("Collection")]
    public class RootModel : DirectoryModel
    {

    }

    public class DirectoryModelTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value?.ToString();
            var dir = ExplorerProxy.Instance.CreateDirectoryInfo(str);
            if (ExplorerProxy.Instance.ExistsDirectoryInfo(dir))
            {
                return new DirectoryModel(ExplorerProxy.Instance.CreateDirectoryInfo(str));
            }
            else if (Enum.TryParse(str, out Environment.SpecialFolder special))
            {
                return new DirectoryModel(special);
            }
            return null;
        }
    }


    public partial class IconHelper
    {
        public static IconHelper Instance = new Lazy<IconHelper>().Value;

        /// <summary> 返回系统设置的图标 </summary>  
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>  
        /// <param name="dwFileAttributes">0</param>  
        /// <param name="psfi">结构体</param>  
        /// <param name="cbSizeFileInfo">结构体大小</param>  
        /// <param name="uFlags">枚举类型</param>  
        /// <returns>-1失败</returns>  
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public System.Drawing.Icon GetSystemInfoIcon(string path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            System.Drawing.Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public enum SHGFI
        {
            SHGFI_ICON = 0x100,
            SHGFI_LARGEICON = 0x0,
            SHGFI_USEFILEATTRIBUTES = 0x10
        }
    }


}
