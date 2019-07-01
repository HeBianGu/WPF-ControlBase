using HeBianGu.Base.WpfBase;
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

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// CenterContainControl.xaml 的交互逻辑
    /// </summary>
    public partial class CenterContainControl : UserControl
    {
        public CenterContainControl()
        {
            InitializeComponent();
        }


        public Link Link
        {
            get { return (Link)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Link", typeof(Link), typeof(CenterContainControl), new PropertyMetadata(default(Link), (d, e) =>
             {
                 CenterContainControl control = d as CenterContainControl;

                 if (control == null) return;

                 Link config = e.NewValue as Link;

             }));

    }
}
