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
