using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.App.Touch.View.Share
{
    /// <summary>
    /// AwaitControl.xaml 的交互逻辑
    /// </summary>
    public partial class AwaitControl : UserControl
    {
        /// <summary> 用来标识当前窗口是否显示 </summary>
        public static bool IsShowed;

        public static AwaitControl Create()
        {
            IsShowed = true;

            return new AwaitControl();
        }

        private AwaitControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnClosed();

            IsShowed = false;
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ClosedRoutedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(AwaitControl));
        //CLR事件包装
        public event RoutedEventHandler Closed
        {
            add { AddHandler(ClosedRoutedEvent, value); }
            remove { RemoveHandler(ClosedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnClosed()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClosedRoutedEvent, this);
            RaiseEvent(args);
        }


    }
}
