using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    public partial class ObjectContentDialog : ContentControl
    {

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
            CommandBinding sumit = new CommandBinding(ObjectContentDialog.Sumit, (l,k) =>
              {
                  this.OnSumited();
              });

            this.CommandBindings.Add(sumit);
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
    }
}
