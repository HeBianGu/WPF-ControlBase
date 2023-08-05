// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Explorer
{
    /// <summary> 导航树 </summary>
    public class ExplorerTree : TreeView
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ExplorerTree), "S.ExplorerTree.Default");
        public static ComponentResourceKey QuickKey => new ComponentResourceKey(typeof(ExplorerTree), "S.ExplorerTree.Quick.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(ExplorerTree), "S.ExplorerTree.Single");

        static ExplorerTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExplorerTree), new FrameworkPropertyMetadata(typeof(ExplorerTree)));

            StyleLoader.Instance?.LoadDefault(typeof(ExplorerTree));
        }

        public ExplorerTree()
        {
            this.Loaded += ExplorerTree_Loaded;
        }

        private void ExplorerTree_Loaded(object sender, RoutedEventArgs e)
        {
            this.Init();
        }

        private void Init()
        {
            List<RootModel> roots = new List<RootModel>();

            RootModel root = new RootModel() { DisplayName = "此电脑", IsExpanded = true, Icon = "\xe67b" };

            List<SystemInfoModel> from = new List<SystemInfoModel>();

            //DriveInfo[] drives = DriveInfo.GetDrives();

            var drives = ExplorerProxy.Instance.GetDrives();

            foreach (var item in drives)
            {
                DirectoryModel directory = new DirectoryModel(item);
                directory.FileInfoVisible = this.FileInfoVisible;
                directory.RefreshChildren();
                from.Add(directory);
            }
            root.Collection = from.ToObservable();
            roots.Add(root);
            roots.AddRange(this.Customs);
            foreach (RootModel custom in this.Customs)
            {
                foreach (SystemInfoModel c in custom.Collection)
                {
                    if (c is DirectoryModel model)
                    {
                        model.RefreshChildren();
                    }
                }
            }

            this.ItemsSource = roots;
        }


        public bool FileInfoVisible
        {
            get { return (bool)GetValue(FileInfoVisibleProperty); }
            set { SetValue(FileInfoVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileInfoVisibleProperty =
            DependencyProperty.Register("FileInfoVisible", typeof(bool), typeof(ExplorerTree), new PropertyMetadata(default(bool), (d, e) =>
             {
                 ExplorerTree control = d as ExplorerTree;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        /// <summary> 自定义初始化访问位置 </summary>
        public RootModelCollection Customs
        {
            get { return (RootModelCollection)GetValue(CustomsProperty); }
            set { SetValue(CustomsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomsProperty =
            DependencyProperty.Register("Customs", typeof(RootModelCollection), typeof(ExplorerTree), new PropertyMetadata(new RootModelCollection(), (d, e) =>
             {
                 ExplorerTree control = d as ExplorerTree;

                 if (control == null) return;

                 RootModelCollection config = e.NewValue as RootModelCollection;

             }));
    }
}
