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
    /// ToolBarControl.xaml 的交互逻辑
    /// </summary>
    public partial class ToolBarControl : UserControl
    {
        public ToolBarControl()
        {
            InitializeComponent();

            this.Loaded += ToolBarControl_Loaded;
        }

        private void ToolBarControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshUI(Buttons);
        }


        public List<FrameworkElement> Buttons { get; set; } = new List<FrameworkElement>();

        void RefreshUI(List<FrameworkElement> config)
        {
            this.dp_left.Children.Clear();
            this.dp_right.Children.Clear();

            foreach (var item in config)
            {
                if(item.Tag==null)
                {
                    this.dp_left.Children.Add(item);
                }
                else
                {
                    this.dp_right.Children.Add(item);
                }
            }
        }
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(ToolBarControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 ToolBarControl control = d as ToolBarControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 control.tb_header.Text = config;

             }));



        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register("HorizontalAlignment", typeof(HorizontalAlignment), typeof(ToolBarControl), new PropertyMetadata(default(HorizontalAlignment), (d, e) =>
             {
                 ToolBarControl control = d as ToolBarControl;

                 if (control == null) return;

                 HorizontalAlignment config = (HorizontalAlignment)e.NewValue;

                 control.sp_button.HorizontalAlignment = config;

             }));

        //private void cb_Checked(object sender, RoutedEventArgs e)
        //{
        //    this.sp_button.HorizontalAlignment = HorizontalAlignment.Left;
        //}

        //private void cb_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    this.sp_button.HorizontalAlignment = HorizontalAlignment.Right;
        //}
    }
}
