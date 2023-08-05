// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Panel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.Message
{

    public partial class ObjectContentDialog : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ObjectContentDialog), "S.ObjectContentDialog.Default");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(ObjectContentDialog), "S.ObjectContentDialog.Clear");
        public static ComponentResourceKey CloseKey => new ComponentResourceKey(typeof(ObjectContentDialog), "S.ObjectContentDialog.Close");

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        static ObjectContentDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectContentDialog), new FrameworkPropertyMetadata(typeof(ObjectContentDialog)));
        }

        public ObjectContentDialog()
        {
            //  Do ：设置绑定命令
            CommandBinding sumit = new CommandBinding(ObjectContentDialog.Sumit, (l, k) =>
              {
                  this.OnSumited();
              });

            this.CommandBindings.Add(sumit);

            //  Do ：设置绑定命令
            CommandBinding close = new CommandBinding(ObjectContentDialog.Close, (l, k) =>
            {
                this.OnClosed();
            });

            this.CommandBindings.Add(close);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ClosedRoutedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ObjectContentDialog));
        //CLR事件包装
        public event RoutedEventHandler Closed
        {
            add { this.AddHandler(ClosedRoutedEvent, value); }
            remove { this.RemoveHandler(ClosedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnClosed()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClosedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitedRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumited", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(ObjectContentDialog));
        //CLR事件包装
        public event RoutedEventHandler Sumited
        {
            add { this.AddHandler(SumitedRoutedEvent, value); }
            remove { this.RemoveHandler(SumitedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumited()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitedRoutedEvent, this);
            this.RaiseEvent(args);
        }

        /// <summary> 标题 </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ObjectContentDialog), new PropertyMetadata(default(string), (d, e) =>
             {
                 ObjectContentDialog control = d as ObjectContentDialog;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public static RoutedUICommand Sumit = new RoutedUICommand() { Text = "确定" };

        public static RoutedUICommand Close = new RoutedUICommand() { Text = "关闭" };

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.OriginalSource is Grid grid)
            {
                if (grid.Name == "grid" && ContainPanelSetting.Instance.UseClickClose)
                {
                    this.OnClosed();
                }
            }
        }
    }
}
