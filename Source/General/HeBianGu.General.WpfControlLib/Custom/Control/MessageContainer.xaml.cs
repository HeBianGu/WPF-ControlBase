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

        public ObservableCollection<MessageBase> Source
        {
            get { return (ObservableCollection<MessageBase>)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ObservableCollection<MessageBase>), typeof(MessageContainer), new PropertyMetadata(new ObservableCollection<MessageBase>(), (d, e) =>
            {
                MessageContainer control = d as MessageContainer;

                if (control == null) return;

                //ProgressBarState config = e.NewValue as ProgressBarState;

            }));

    }

    public abstract class MessageBase
    {

        public string Message { get; set; }

        public string Time { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public abstract class DailogMessageh: MessageBase
    {

        public Action Cancel { get; set; }

        public Action Sumit { get; set; }
    }



    public class WarnMessage : MessageBase
    {

    }

    public class SuccessMessage : MessageBase
    {

    }

    public class ErrorMessage : MessageBase
    {

    }

    public class InfoMessage : MessageBase
    {

    }


    public class FatalMessage : MessageBase
    {

    }

    public class DailogMessage : MessageBase
    {

    }
}
