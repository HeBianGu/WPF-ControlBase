using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            this.OnClosed();

            IsShowed = false;
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ClosedRoutedEvent =
            EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(AwaitControl));
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


    }
}
