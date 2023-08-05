// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.ScrollVewerLocator
{
    public class MaskGrid : Border
    {

        //声明和注册路由事件
        public static readonly RoutedEvent LocationChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("LocationChanged", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(MaskGrid));
        //CLR事件包装
        public event RoutedEventHandler LocationChanged
        {
            add { this.AddHandler(LocationChangedRoutedEvent, value); }
            remove { this.RemoveHandler(LocationChangedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        internal void OnLocationChanged()
        {
            RoutedEventArgs args = new RoutedEventArgs(LocationChangedRoutedEvent, this);
            this.RaiseEvent(args);
        }

    }
}
