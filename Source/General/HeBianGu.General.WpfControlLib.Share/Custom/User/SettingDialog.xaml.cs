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

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SettingDialog : UserControl
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.OnClose();
        }

        private void Button_Sumit(object sender, RoutedEventArgs e)
        {
            this.OnSumit();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SettingDialog));
        //CLR事件包装
        public event RoutedEventHandler Sumit
        {
            add { this.AddHandler(SumitRoutedEvent, value); }
            remove { this.RemoveHandler(SumitRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumit()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent CloseRoutedEvent =
            EventManager.RegisterRoutedEvent("Close", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SettingDialog));
        //CLR事件包装
        public event RoutedEventHandler Close
        {
            add { this.AddHandler(CloseRoutedEvent, value); }
            remove { this.RemoveHandler(CloseRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnClose()
        {
            RoutedEventArgs args = new RoutedEventArgs(CloseRoutedEvent, this);
            this.RaiseEvent(args);
        }


    }
}
