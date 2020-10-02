using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    public class ScatterBase : Layer
    {
        public bool AlignmentCenter
        {
            get { return (bool)GetValue(AlignmentCenterProperty); }
            set { SetValue(AlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignmentCenterProperty =
            DependencyProperty.Register("AlignmentCenter", typeof(bool), typeof(ScatterBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                xAxis control = d as xAxis;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.Draw(control);

            }));


        protected override void InitX()
        {
            base.InitX();

            if (this.AlignmentCenter)
            {
                double span = (this.maxX - this.minX) / this.xAxis.Count;

                this.maxX = this.maxX + span / 2;

                this.minX = this.minX - span / 2;
            }
        }
    }

    /// <summary> 散点图 </summary>
    public class Scatter : ScatterBase
    {
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                //  Do ：显示标记
                Shape m = Activator.CreateInstance(this.MarkStyle.TargetType) as Shape;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    Canvas.SetLeft(m, this.GetX(x, this.ActualWidth));
                    Canvas.SetTop(m, this.GetY(y, this.ActualHeight));
                    this.Children.Add(m);
                }
            }
        }
    }

    /// <summary> 散点图 </summary>
    public class Bubble : ScatterBase
    {
        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> BubbleData
        {
            get { return (ObservableCollection<double>)GetValue(BubbleDataProperty); }
            set { SetValue(BubbleDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BubbleDataProperty =
            DependencyProperty.Register("BubbleData", typeof(ObservableCollection<double>), typeof(Bubble), new PropertyMetadata(default(ObservableCollection<double>), (d, e) =>
             {
                 Bubble control = d as Bubble;

                 if (control == null) return;

                 ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                double v = this.BubbleData[i];

                //  Do ：显示标记
                EllipseMarker m = Activator.CreateInstance(this.MarkStyle.TargetType) as EllipseMarker;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    m.Rect = new Rect(0, 0, v, v);

                    Canvas.SetLeft(m, this.GetX(x, this.ActualWidth));
                    Canvas.SetTop(m, this.GetY(y, this.ActualHeight));
                    this.Children.Add(m);
                }
            }
        }
    }
}
