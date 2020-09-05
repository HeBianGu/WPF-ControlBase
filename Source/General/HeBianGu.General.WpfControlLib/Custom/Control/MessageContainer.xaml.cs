using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 自定义导航框架 </summary> 
    public class MessageContainer : ItemsControl
    {
        static MessageContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageContainer), new FrameworkPropertyMetadata(typeof(MessageContainer)));
        }

        //public ObservableCollection<MessageBase> Source
        //{
        //    get { return (ObservableCollection<MessageBase>)GetValue(SourceProperty); }
        //    set { SetValue(SourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SourceProperty =
        //    DependencyProperty.Register("Source", typeof(ObservableCollection<MessageBase>), typeof(MessageContainer), new PropertyMetadata(new ObservableCollection<MessageBase>(), (d, e) =>
        //    {
        //        MessageContainer control = d as MessageContainer;

        //        if (control == null) return;

        //        //ProgressBarState config = e.NewValue as ProgressBarState;

        //    }));  

        public void Add(MessageBase message)
        {
            this.Items.Add(message);
        }
    }


    public abstract class MessageBase : ContentControl
    {
        public static RoutedUICommand CloseCommand = new RoutedUICommand() { Text = "关闭" }; 

        public MessageBase()
        {
            {
                CommandBinding binding = new CommandBinding(CloseCommand, (l, k) =>
                {
                    //  Do ：删除消息
                    this.Close();
                });

                this.CommandBindings.Add(binding);
            }
        }
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(MessageBase), new PropertyMetadata(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (d, e) =>
             {
                 MessageBase control = d as MessageBase;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set 
            {
                this.Dispatcher?.Invoke(() =>
                {
                    SetValue(MessageProperty, value);
                });
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBase), new PropertyMetadata(default(string), (d, e) =>
             {
                 MessageBase control = d as MessageBase;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

        public void Close()
        {
            this.Dispatcher.Invoke(() =>
            {
                var parent = this.GetParent<MessageContainer>();

                if (parent == null) return;

                parent.Items.Remove(this);
            });

        }

        
    }

    public abstract class DailogMessageBase : MessageBase
    {
        public Func<bool> Sumit { get; set; }
    }

    public class WarnMessage : MessageBase
    {

    }

    public class WaittingMessage : MessageBase
    {

    }

    public abstract class AutoCloseMessage : MessageBase
    {
        public AutoCloseMessage()
        {
            this.Loaded += (l, k) =>
            {
                Task.Delay(5000).ContinueWith(m => this.Close());
            };
        }
    }

    public class SuccessMessage : AutoCloseMessage
    {
       
    }

    public class ErrorMessage : MessageBase
    {

    }

    public class InfoMessage : AutoCloseMessage
    {

    }

    public class FatalMessage : MessageBase
    {

    }

    public class DailogMessage : DailogMessageBase
    {
        public static RoutedUICommand SumitCommand = new RoutedUICommand() { Text = "确定" };

        public DailogMessage()
        {
            {
                CommandBinding binding = new CommandBinding(SumitCommand, (l, k) =>
                {
                    //  Do ：点击确定先检查是否合法
                    if(this.IsMatch?.Invoke(this)==true)
                    {
                        this.Close();
                    }
                });

                this.CommandBindings.Add(binding);
            }
        }

        public Predicate<DailogMessage> IsMatch { get; set; }
    }

    public class StringProgressMessage : MessageBase
    {

    }

    public class PercentProgressMessage : MessageBase
    {
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(PercentProgressMessage), new PropertyMetadata(default(double), (d, e) =>
             {
                 PercentProgressMessage control = d as PercentProgressMessage;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


    }
}
