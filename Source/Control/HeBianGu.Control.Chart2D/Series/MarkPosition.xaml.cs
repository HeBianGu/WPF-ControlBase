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
    /// <summary> 曲线视图 </summary>
    public class MarkPosition : Layer
    {

        public double WidthPercent
        {
            get { return (double)GetValue(WidthPercentProperty); }
            set { SetValue(WidthPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthPercentProperty =
            DependencyProperty.Register("WidthPercent", typeof(double), typeof(MarkPosition), new PropertyMetadata(0.8, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));


        public double ItemPercent
        {
            get { return (double)GetValue(ItemPercentProperty); }
            set { SetValue(ItemPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemPercentProperty =
            DependencyProperty.Register("ItemPercent", typeof(double), typeof(MarkPosition), new PropertyMetadata(1.0, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.TryDraw();

            }));

        public int MulCount
        {
            get { return (int)GetValue(MulCountProperty); }
            set { SetValue(MulCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MulCountProperty =
            DependencyProperty.Register("MulCount", typeof(int), typeof(MarkPosition), new PropertyMetadata(1, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //int config = e.NewValue as int;

                control.TryDraw();

            }));


        public int MulIndex
        {
            get { return (int)GetValue(MulIndexProperty); }
            set { SetValue(MulIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MulIndexProperty =
            DependencyProperty.Register("MulIndex", typeof(int), typeof(MarkPosition), new PropertyMetadata(0, (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //int config = e.NewValue as int;
                control.TryDraw();

            }));

        public bool AlignmentCenter
        {
            get { return (bool)GetValue(AlignmentCenterProperty); }
            set { SetValue(AlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignmentCenterProperty =
            DependencyProperty.Register("AlignmentCenter", typeof(bool), typeof(MarkPosition), new PropertyMetadata(default(bool), (d, e) =>
            {
                MarkPosition control = d as MarkPosition;

                if (control == null) return;

                //bool config = e.NewValue as bool;

                control.TryDraw();

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


        public MarkValueType MarkValueType
        {
            get { return (MarkValueType)GetValue(MarkValueTypeProperty); }
            set { SetValue(MarkValueTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkValueTypeProperty =
            DependencyProperty.Register("MarkValueType", typeof(MarkValueType), typeof(MarkPosition), new PropertyMetadata(default(MarkValueType), (d, e) =>
             {
                 MarkPosition control = d as MarkPosition;

                 if (control == null) return;

                 //MarkValueType config = e.NewValue as MarkValueType;

                 control.TryDraw();

             }));

        Point GetPoint()
        {
            double span = this.AlignmentCenter ? (this.maxX - this.minX) / (this.xAxis.Count) : 0;

            double itemWidth = span * this.WidthPercent;

            double v = this.MarkValueType == MarkValueType.Max ? this.Data.Max() : this.Data.Min();

            int index = this.Data.IndexOf(v);

            double x = this.xAxis[index];

            return new Point((x - itemWidth / 2) + (this.MulIndex+0.5) * (itemWidth / this.MulCount), v);
        }


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (this.Data.Count == 0) return;

            Point point = this.GetPoint();

            //Canvas.SetLeft(Marker,this.GetX(point.X) - Marker.ActualWidth / 2);
            //Canvas.SetTop(Marker, this .GetY(point.Y) - Marker.ActualHeight);

            //this.Children.Add(Marker);

            //  Do ：显示文本
            Label t = new Label();

            t.Content = point.Y.ToString();
            t.Style = this.LabelStyle;

            t.Foreground = Brushes.White;

            t.Loaded += (o, e) =>
            {
                Canvas.SetLeft(t, this.GetX(point.X) - t.ActualWidth / 2);
                Canvas.SetTop(t, this.GetY(point.Y) - t.ActualHeight * 1.2);
            };
            canvas.Children.Add(t);
        }
    }


    public enum MarkValueType
    {
        Max = 0, Min
    }

}
