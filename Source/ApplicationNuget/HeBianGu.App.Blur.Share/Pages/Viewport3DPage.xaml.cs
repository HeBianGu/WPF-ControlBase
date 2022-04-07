using System;
using System.Windows.Controls;
using System.Windows.Input;
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

        private DispatcherTimer clock = null;
        private double rotAngle = 90;

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
