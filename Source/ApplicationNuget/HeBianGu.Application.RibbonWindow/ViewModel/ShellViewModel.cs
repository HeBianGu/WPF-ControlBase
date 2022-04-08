﻿using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Imaging;

namespace HeBianGu.Application.RibbonWindow
{
    internal class ShellViewModel : NotifyPropertyChanged
    {


        private ObservableCollection<TreeNodeBase<Student>> _collection = new ObservableCollection<TreeNodeBase<Student>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeBase<Student>> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private RibbonData _ribbonData = new RibbonData();
        /// <summary> 说明  </summary>
        public RibbonData RibbonData
        {
            get { return _ribbonData; }
            set
            {
                _ribbonData = value;
                RaisePropertyChanged();
            }
        }



        protected override void Init()
        {
            for (int i = 0; i < 10; i++)
            {
                TreeNodeBase<Student> n = new TreeNodeBase<Student>(new Student());

                for (int j = 0; j < 5; j++)
                {
                    n.Nodes.Add(new TreeNodeBase<Student>(new Student()));
                }
                Collection.Add(new TreeNodeBase<Student>(new Student()));
            }

            for (int i = 0; i < 5; i++)
            {
                var tab = new TabData
                {
                    Header = "TabData" + i.ToString()
                };
                tab.Label = tab.Header;

                for (int j = 0; j < 2; j++)
                {
                    var group = new GroupData
                    {
                        Label = "GroupData" + j.ToString()
                    };
                    tab.GroupDataCollection.Add(group);

                    for (int k = 0; k < 5; k++)
                    {
                        var button = new ButtonData
                        {
                            Label = "ButtonData" + k.ToString(),
                            LargeImage = new BitmapImage(new Uri("pack://application:,,,/HeBianGu.Application.RibbonWindow;component/Resources/CropImage32.png", UriKind.RelativeOrAbsolute)),
                            SmallImage = new BitmapImage(new Uri("pack://application:,,,/HeBianGu.Application.RibbonWindow;component/Resources/CropImage16.png", UriKind.RelativeOrAbsolute))
                        };
                        group.ControlDataCollection.Add(button);
                    }
                }
                RibbonData.TabDataCollection.Add(tab);
            }
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "1")
            {

            }
            else if (command == "2")
            {

            }

        }
    }


    public class CustomRibbon : Ribbon
    {
        public CustomRibbon()
        {

            Loaded += CustomRibbon_Loaded;

            ContextMenuOpening += ContextMenu_ContextMenuOpening;

        }

        private void ContextMenu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ContextMenu is RibbonContextMenu menu)
            {
                if (RibbonControlService.GetIsInQuickAccessToolBar(this))
                {
                    RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                    qatPlacementItem.Header = "自定义";
                    menu.Items.Add(qatPlacementItem);
                }
            }

        }

        private void CustomRibbon_Loaded(object sender, RoutedEventArgs e)
        {

            ContextMenu.IsVisibleChanged += ContextMenu_IsVisibleChanged;

            ContextMenu.Opened += ContextMenu_Opened;

            if (ContextMenu is RibbonContextMenu menu)
            {
                RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                qatPlacementItem.Header = "cesss";
                menu.Items.Add(qatPlacementItem);
            }
        }

        private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (ContextMenu is RibbonContextMenu menu)
            {
                //if (RibbonControlService.GetIsInQuickAccessToolBar(this))
                //{
                //    RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                //    qatPlacementItem.Header = "自定义";
                //    menu.Items.Add(qatPlacementItem);
                //}

                RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                qatPlacementItem.Header = "自定义";
                menu.Items.Add(qatPlacementItem);
            }
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (ContextMenu is RibbonContextMenu menu)
            {
                if (RibbonControlService.GetIsInQuickAccessToolBar(this))
                {
                    RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                    qatPlacementItem.Header = "自定义";
                    menu.Items.Add(qatPlacementItem);
                }
            }
        }

        protected override void OnContextMenuOpening(ContextMenuEventArgs e)
        {
            base.OnContextMenuOpening(e);

            if (ContextMenu is RibbonContextMenu menu)
            {
                //if (RibbonControlService.GetIsInQuickAccessToolBar(this))
                //{
                //    RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                //    qatPlacementItem.Header = "自定义";
                //    menu.Items.Add(qatPlacementItem);
                //}

                RibbonMenuItem qatPlacementItem = new RibbonMenuItem() { CanAddToQuickAccessToolBarDirectly = false };
                qatPlacementItem.Header = "自定义";
                menu.Items.Add(qatPlacementItem);
            }
        }

    }
}