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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void FButton_Click_Left(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.To = 0d;
            this.axr.Axis = new Vector3D(0, 1, 0);
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void FButton_Click_Right(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(0, 1, 0);
            da.To = 180d;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void FButton_Click_Top(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(1, 0, 0);
            da.To = 0;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void FButton_Click_Botton(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            da.To = 180d;
            this.axr.Axis = new Vector3D(1, 0, 0);
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }


        private void FButton_Click_Rotate_Left(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(0, 0, 1);
            da.To = 180d;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }

        private void FButton_Click_Rotate_Right(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            this.axr.Axis = new Vector3D(0, 0, 1);
            da.To = 0;
            this.axr.BeginAnimation(AxisAngleRotation3D.AngleProperty, da);
        }
        
        
    }
}
