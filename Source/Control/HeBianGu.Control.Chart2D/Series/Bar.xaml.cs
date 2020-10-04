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

    /// <summary> 柱状图 </summary>
    public class Bar : Layer
    {
        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(Bar), new PropertyMetadata(default(Style), (d, e) =>
            {
                Bar control = d as Bar;

                if (control == null) return;

                Style config = e.NewValue as Style;

            }));


        public double WidthPercent
        {
            get { return (double)GetValue(WidthPercentProperty); }
            set { SetValue(WidthPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthPercentProperty =
            DependencyProperty.Register("WidthPercent", typeof(double), typeof(Bar), new PropertyMetadata(0.8, (d, e) =>
             {
                 Bar control = d as Bar;

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
            DependencyProperty.Register("ItemPercent", typeof(double), typeof(Bar), new PropertyMetadata(1.0, (d, e) =>
             {
                 Bar control = d as Bar;

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
            DependencyProperty.Register("MulCount", typeof(int), typeof(Bar), new PropertyMetadata(1, (d, e) =>
             {
                 Bar control = d as Bar;

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
            DependencyProperty.Register("MulIndex", typeof(int), typeof(Bar), new PropertyMetadata(0, (d, e) =>
             {
                 Bar control = d as Bar;

                 if (control == null) return;

                 //int config = e.NewValue as int;
                 control.TryDraw();

             }));



        protected override void InitX()
        {
            base.InitX();

            double span = (this.maxX - this.minX) / this.xAxis.Count;

            this.maxX = this.maxX + span / 2;

            this.minX = this.minX - span / 2;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = (this.maxX - this.minX) / this.xAxis.Count;

            double itemWidth = span * this.WidthPercent;

            if (this.xAxis.Count != this.Data.Count) return;

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                Path path = new Path();

                path.Style = this.PathStyle;

                PolyLineSegment area = new PolyLineSegment();

                //  Do ：添加曲线
                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount, this.ActualWidth), this.GetY(0, this.ActualHeight)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent, this.ActualWidth), this.GetY(0, this.ActualHeight)));

                area.Points.Add(area.Points.FirstOrDefault());

                PathFigure pf = new PathFigure();
                pf.StartPoint = area.Points.FirstOrDefault();
                pf.Segments.Add(area);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                path.Data = pg;

                this.Children.Add(path);

            }


        }
    }

    public class StackBar : Bar
    {
        [TypeConverter(typeof(DataArrayTypeConverter))]
        public new ObservableCollection<double[]> Data
        {
            get { return (ObservableCollection<double[]>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double[]>), typeof(StackBar), new PropertyMetadata(default(ObservableCollection<double[]>), (d, e) =>
             {
                 StackBar control = d as StackBar;

                 if (control == null) return;

                 ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

                 control.TryDraw();

             }));

        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public new ObservableCollection<Color> Foreground
        {
            get { return (ObservableCollection<Color>)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(ObservableCollection<Color>), typeof(StackBar), new PropertyMetadata(new ObservableCollection<Color>(), (d, e) =>
             {
                 StackBar control = d as StackBar;

                 if (control == null) return;

                 ObservableCollection<Color> config = e.NewValue as ObservableCollection<Color>;

                 control.TryDraw();

             }));



        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = (this.maxX - this.minX) / this.xAxis.Count;

            double itemWidth = span * this.WidthPercent;

            if (this.xAxis.Count != this.Data.Count) return;

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = 0;

                for (int j = 0; j < this.Data[i].Length; j++)
                {
                    double start = y;

                    y = y + this.Data[i][j];

                    Path path = new Path();

                    path.Style = this.PathStyle;

                    path.Stroke = path.Fill = new SolidColorBrush(this.Foreground[j]);

                    PolyLineSegment area = new PolyLineSegment();

                    //  Do ：添加曲线
                    area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount, this.ActualWidth), this.GetY(start, this.ActualHeight)));

                    area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                    area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                    area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent, this.ActualWidth), this.GetY(start, this.ActualHeight)));

                    area.Points.Add(area.Points.FirstOrDefault());

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = area.Points.FirstOrDefault();
                    pf.Segments.Add(area);

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    path.Data = pg;

                    this.Children.Add(path);
                }
            }


        }

    }
}
