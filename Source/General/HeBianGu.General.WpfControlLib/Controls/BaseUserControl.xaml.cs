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
    /// BaseUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUserControl : UserControl
    {
        //public BaseUserControl()
        //{
        //    InitializeComponent();
        //}

        static BaseUserControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseUserControl), new FrameworkPropertyMetadata(typeof(BaseUserControl)));
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ShowedRoutedEvent =
            EventManager.RegisterRoutedEvent("Showed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(BaseUserControl));
        //CLR事件包装
        public event RoutedEventHandler Showed
        {
            add { this.AddHandler(ShowedRoutedEvent, value); }
            remove { this.RemoveHandler(ShowedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnShowed()
        {
            RoutedEventArgs args = new RoutedEventArgs(ShowedRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent HiddenRoutedEvent =
            EventManager.RegisterRoutedEvent("Hidden", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(BaseUserControl));
        //CLR事件包装
        public event RoutedEventHandler Hidden
        {
            add { this.AddHandler(HiddenRoutedEvent, value); }
            remove { this.RemoveHandler(HiddenRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnHidden()
        {
            RoutedEventArgs args = new RoutedEventArgs(HiddenRoutedEvent, this);
            this.RaiseEvent(args);
        }



        public bool IsShow
        {
            get { return (bool)GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowProperty =
            DependencyProperty.Register("IsShow", typeof(bool), typeof(BaseUserControl), new PropertyMetadata(default(bool), (d, e) =>
             {
                 BaseUserControl control = d as BaseUserControl;

                 if (control == null) return;

                 bool config = (bool)e.NewValue;

                 if(config)
                 {
                     control.OnShowed();
                 }
                 else
                 {
                     control.OnHidden();
                 }

             }));

    }
}
