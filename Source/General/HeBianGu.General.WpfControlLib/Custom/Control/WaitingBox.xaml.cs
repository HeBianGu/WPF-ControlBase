// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// WaitingBox.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingBox : UserControl
    {
        //public WaitingBox()
        //{
        //    InitializeComponent();
        //}

        public bool IsBusy
        {

            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(WaitingBox), new PropertyMetadata(true));

        static WaitingBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaitingBox), new FrameworkPropertyMetadata(typeof(WaitingBox)));
        }
    }
}
