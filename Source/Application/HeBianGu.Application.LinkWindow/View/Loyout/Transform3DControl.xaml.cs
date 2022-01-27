using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace HeBianGu.Applications.ControlBase.LinkWindow.View.Loyout
{
    /// <summary>
    /// Transform3DControl.xaml 的交互逻辑
    /// </summary>
    public partial class Transform3DControl : UserControl
    {
        public Transform3DControl()
        {
            InitializeComponent();
        }

        private void Button_Click_Left(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.To = 0d;
            this.axr.Axis = new Vector3D(0, 1, 0);
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void Button_Click_Right(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(0, 1, 0);
            da.To = 180d;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void Button_Click_Top(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(1, 0, 0);
            da.To = 0;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void Button_Click_Botton(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.To = 180d;
            this.axr.Axis = new Vector3D(1, 0, 0);
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }


        private void Button_Click_Rotate_Left(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(0, 0, 1);
            da.To = 180d;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void Button_Click_Rotate_Right(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(0, 0, 1);
            da.To = 0;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }


    }
}
