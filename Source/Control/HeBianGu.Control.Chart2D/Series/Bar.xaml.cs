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
    public class BarBase : Layer
    {

        public double WidthPercent
        {
            get { return (double)GetValue(WidthPercentProperty); }
            set { SetValue(WidthPercentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthPercentProperty =
            DependencyProperty.Register("WidthPercent", typeof(double), typeof(BarBase), new PropertyMetadata(0.8, (d, e) =>
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
            DependencyProperty.Register("ItemPercent", typeof(double), typeof(BarBase), new PropertyMetadata(1.0, (d, e) =>
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
            DependencyProperty.Register("MulCount", typeof(int), typeof(BarBase), new PropertyMetadata(1, (d, e) =>
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
            DependencyProperty.Register("MulIndex", typeof(int), typeof(BarBase), new PropertyMetadata(0, (d, e) =>
            {
                Bar control = d as Bar;

                if (control == null) return;

                //int config = e.NewValue as int;
                control.TryDraw();

            }));


    }

    /// <summary> 柱状图 </summary>
    public class Bar : BarBase
    {
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

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                Path path = new Path();

                path.Style = this.PathStyle;

                PolyLineSegment area = new PolyLineSegment();

                //  Do ：添加曲线
                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount, this.ActualWidth), this.GetY(this.minY, this.ActualHeight)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                area.Points.Add(new Point(this.GetX((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent, this.ActualWidth), this.GetY(this.minY, this.ActualHeight)));

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

    public class StackBarBase : BarBase
    {
        [TypeConverter(typeof(DataArrayTypeConverter))]
        public new ObservableCollection<double[]> Data
        {
            get { return (ObservableCollection<double[]>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double[]>), typeof(StackBarBase), new PropertyMetadata(new ObservableCollection<double[]>(), (d, e) =>
            {
                StackBarBase control = d as StackBarBase;

                if (control == null) return;

                ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

                control.DataBoolean = Enumerable.Range(0, config.FirstOrDefault().Length).Select(l => true)?.ToObservable();

                control.TryDraw();

            }));

        //  Do ：过滤掉基类Data刷新的DataBoolean
        protected override void InitDataBoolean(int count, bool value = true)
        {
            return;
        }

        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public new ObservableCollection<Color> Foreground
        {
            get { return (ObservableCollection<Color>)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static new readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(ObservableCollection<Color>), typeof(StackBarBase), new PropertyMetadata(new ObservableCollection<Color>(), (d, e) =>
            {
                StackBar control = d as StackBar;

                if (control == null) return;

                ObservableCollection<Color> config = e.NewValue as ObservableCollection<Color>; 

                control.TryDraw();

            }));
    }

    public class StackBar : StackBarBase
    {
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

            if (this.Data == null || this.xAxis == null) return;

            if (this.xAxis.Count != this.Data.Count) return;

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = 0;

                for (int j = 0; j < this.Data[i].Length; j++)
                {
                    double start = y;

                    bool use = this.DataBoolean.Count>j? this.DataBoolean[j]:true;

                    if (!use) continue;

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

                    //  Do ：用于标识Legend显示信息
                    path.Tag = this.xDisplay.Count > i ? this.xDisplay[i] : this.Data[i].ToString();

                    this.Children.Add(path);
                }
            }


        }

    }

    /// <summary> 柱状图 </summary>
    public class yBar : BarBase
    {
        protected override void InitY()
        {
            base.InitY();

            double span = (this.maxY - this.minY) / this.yAxis.Count;

            this.maxY = this.maxY + span / 2;

            this.minY = this.minY - span / 2;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = (this.maxY - this.minY) / this.yAxis.Count;

            double itemWidth = span * this.WidthPercent;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                double x = this.yAxis[i];

                double d = this.Data[i];

                Path path = new Path();

                path.Style = this.PathStyle;

                PolyLineSegment area = new PolyLineSegment();

                //  Do ：添加曲线
                area.Points.Add(new Point(this.GetX(this.minX), this.GetY((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount)));

                area.Points.Add(new Point(this.GetX(d), this.GetY((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount)));

                area.Points.Add(new Point(this.GetX(d), this.GetY((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent)));

                area.Points.Add(new Point(this.GetX(this.minX), this.GetY((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent)));

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

    public class yStackBar : StackBarBase
    {
        protected override void InitY()
        {
            base.InitY();

            double span = (this.maxY - this.minY) / this.yAxis.Count;

            this.maxY = this.maxY + span / 2;

            this.minY = this.minY - span / 2;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = (this.maxY - this.minY) / this.yAxis.Count;

            double itemWidth = span * this.WidthPercent;

            for (int i = 0; i < this.yAxis.Count; i++)
            {
                double x = this.yAxis[i];

                double d = this.minX;

                for (int j = 0; j < this.Data[i].Length; j++)
                {
                    bool use = this.DataBoolean.Count > j ? this.DataBoolean[j] : true;

                    if (!use) continue;

                    double start = d;

                    d = d + this.Data[i][j];

                    Path path = new Path();

                    path.Style = this.PathStyle;

                    path.Stroke = path.Fill = new SolidColorBrush(this.Foreground[j]);

                    PolyLineSegment area = new PolyLineSegment();

                    //  Do ：添加曲线
                    area.Points.Add(new Point(this.GetX(start), this.GetY((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount)));

                    area.Points.Add(new Point(this.GetX(d), this.GetY((x - itemWidth / 2) + this.MulIndex * itemWidth / this.MulCount)));

                    area.Points.Add(new Point(this.GetX(d), this.GetY((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent)));

                    area.Points.Add(new Point(this.GetX(start), this.GetY((x - itemWidth / 2) + ((this.MulIndex + 1) * itemWidth / this.MulCount) * this.ItemPercent)));

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
