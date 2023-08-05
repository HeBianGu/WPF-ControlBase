// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;

namespace HeBianGu.Control.MessageContainer
{


    public class PercentProgressMessage : MessageBase, IPercentProgressMessage
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PercentProgressMessage), "S.PercentProgressMessage.Default");

        static PercentProgressMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PercentProgressMessage), new FrameworkPropertyMetadata(typeof(PercentProgressMessage)));
        }

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
