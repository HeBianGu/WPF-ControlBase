using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class Explorer : PagedDataGrid
    {
        static Explorer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Explorer), new FrameworkPropertyMetadata(typeof(Explorer)));
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
        bool _isHistoryRefresh = false;

        public Explorer()
        {
            {
                CommandBinding binding = new CommandBinding(Next, (l, k) =>
                {
                    int index = this.History.IndexOf(HistorySelectedItem);

                    //this.RefreshPath(this.History[index + 1]?.Model.FullName);

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

                    //this.RefreshPath(this.History[index - 1]?.Model.FullName);

                    this.HistorySelectedItem = this.History[index - 1];

                }, (l, k) =>
                {
                    int index = this.History.IndexOf(HistorySelectedItem);

                    k.CanExecute = index > 0;
                });

                this.CommandBindings.Add(binding);
            }
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

                 if (control == null) return;

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

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

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

                 if(!control._isHistoryRefresh)
                 {
                     control.RefreshHistory(config);
                 }

                 //  Do ：回退状态
                 control._isHistoryRefresh = false;

             }));

        void RefreshPath(string path)
        {
            List<SystemInfoModel> from = new List<SystemInfoModel>();

            if (!Directory.Exists(path))
            {
                var drives = DriveInfo.GetDrives();

                from.AddRange(drives.Select(l => new DirectoryModel(l.RootDirectory)));
            }
            else
            {
                var dir = new DirectoryInfo(path);

                var all = dir.GetFileSystemInfos().Where(l => !l.Attributes.HasFlag(FileAttributes.System));

                from.AddRange(all.OfType<DirectoryInfo>().Select(l => new DirectoryModel(l)));

                from.AddRange(all.OfType<FileInfo>().Select(l => new FileModel(l)));
                
            }

            this.DataSource = from.ToObservable();
        
        }

        void RefreshHistory(string path)
        {
            if (!Directory.Exists(path)) return;

            var dir = new DirectoryInfo(path);

            //  Do ：加载历史记录，获取后十个
            this.History.Insert(0,new DirectoryModel(dir));

            this.History = this.History.Take(10).ToObservable();

            this.HistorySelectedItem = this.History?.FirstOrDefault();
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (this.SelectedItem is DirectoryModel model)
            {
                this.CurrentPath = model.Model?.FullName;
            }
        }
    }


    public class NavigationBar : ListBox
    {
        public string CurrentPath
        {
            get { return (string)GetValue(CurrentPathProperty); }
            set { SetValue(CurrentPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPathProperty =
            DependencyProperty.Register("CurrentPath", typeof(string), typeof(NavigationBar), new PropertyMetadata(default(string), (d, e) =>
             {
                 NavigationBar control = d as NavigationBar;

                 if (control == null) return;

                 string config = e.NewValue as string;


                 control.RefreshData(config);

             }));


        void RefreshData(string path)
        {
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();

            if (!Directory.Exists(path))
            {
                dirs.Add(new DirectoryInfo("我的电脑"));
            }
            else
            {
                var dir = new DirectoryInfo(path);

                Action<DirectoryInfo> action = null;

                action = l =>
                {
                    if (l.Parent == null) return;

                    dirs.Add(l.Parent);

                    action(l.Parent);
                };


                dirs.Add(dir);

                action.Invoke(dir);

                dirs.Add(new DirectoryInfo("我的电脑"));

                dirs.Reverse();
            }


            this.ItemsSource = dirs.Select(l => new DirectoryModel(l));
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (this.SelectedItem is DirectoryModel dir)
            {
                this.CurrentPath = dir.Model.FullName;
            }
        }
    }

    /// <summary> 说明</summary>
    public class SystemInfoModel : NotifyPropertyChanged
    {
        #region - 属性 -

        private string _ficon;
        /// <summary> 说明  </summary>
        public string FIcon
        {
            get { return _ficon; }
            set
            {
                _ficon = value;
                RaisePropertyChanged("FIcon");
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
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
        }

        #endregion
    }

    /// <summary> 说明</summary>
    public class SystemInfoModel<T> : SystemInfoModel where T : FileSystemInfo
    {
        #region - 属性 -


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
        }


        /// <summary> 图片路径 </summary>
        public Icon Logo
        {
            get
            {
                return IconHelper.Instance.GetSystemInfoIcon(this.Model?.FullName);
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
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
        }

        #endregion
    }


    /// <summary> 说明</summary>
    public class FileModel : SystemInfoModel<FileInfo>
    {
        #region - 属性 -

        public FileModel(FileInfo model) : base(model)
        {
            this.FIcon = "\xe7fe";
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
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
        }

        #endregion
    }


    /// <summary> 说明</summary>
    public class DirectoryModel : SystemInfoModel<DirectoryInfo>
    {
        public DirectoryModel(DirectoryInfo model) : base(model)
        {
            this.FIcon = "\xe7fe";
        }

        #region - 属性 -

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
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
        }

        #endregion
    }


}
