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
using System.Windows.Threading;

namespace WpfControlDemo.View
{
    /// <summary>
    /// Viewport3DPage.xaml 的交互逻辑
    /// </summary>
    public partial class Viewport3DPage : Page
    {
        public Viewport3DPage()
        {
            InitializeComponent();

            if (clock == null) clock = new DispatcherTimer();
            clock.Tick += new EventHandler(clock_Tick);
            clock.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        DispatcherTimer clock = null;
        double rotAngle = 90;

        private void clock_Tick(object sender, EventArgs e)
        {
            camRotation.Angle += 5;
            if (camRotation.Angle >= rotAngle) clock.Stop();
        }

        private void FrontSide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //初始化值
            camRotation.Angle = 0;
            rotAngle = 90;

            clock.Start();
        }

        private void LeftSide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            rotAngle = 180;
            clock.Start();
        }

        private void BackSide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            rotAngle = 270;
            clock.Start();
        }


        private void RightSide_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            rotAngle = 360;
            clock.Start();
        }


    }
}
