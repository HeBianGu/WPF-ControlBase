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

namespace HeBianGu.Application.TouchWindow.View.Share
{
    /// <summary>
    /// GuideMessageControl.xaml 的交互逻辑
    /// </summary>
    public partial class GuideMessageControl : UserControl
    {
        public GuideMessageControl()
        {
            InitializeComponent();

            this.Loaded +=(l, k) =>
             {
                 this.btn_skipguide.IsActive = true;
             };

        }


        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(GuideMessageControl), new PropertyMetadata(default(ImageSource), (d, e) =>
             {
                 GuideMessageControl control = d as GuideMessageControl;

                 if (control == null) return;

                 ImageSource config = e.NewValue as ImageSource;

             }));



        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(GuideMessageControl), new PropertyMetadata(default(string), (d, e) =>
             {
                 GuideMessageControl control = d as GuideMessageControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

    }
}
