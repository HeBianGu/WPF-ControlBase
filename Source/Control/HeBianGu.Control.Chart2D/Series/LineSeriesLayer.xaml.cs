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
    public class LineSeriesLayer : SeriesLayer
    {
        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(LineSeriesLayer), new PropertyMetadata(default(Style), (d, e) =>
            {
                LineSeriesLayer control = d as LineSeriesLayer;

                if (control == null) return;

                Style config = e.NewValue as Style;

            }));

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment pls = new PolyLineSegment();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                //  Do ：添加曲线
                pls.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));

            }

            //PathFigure pf = new PathFigure();
            //pf.StartPoint = pls.Points.FirstOrDefault();
            //pf.Segments.Add(pls);

            PathFigure pf = new PathFigure();
            pf.StartPoint = pls.Points.FirstOrDefault();
            pf.Segments.Add(pls);

            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf});

            path.Data = pg;

            this.Children.Add(path);

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

    public class AreaSeriesLayer : SeriesLayer
    {
        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(AreaSeriesLayer), new PropertyMetadata(default(Style), (d, e) =>
            {
                AreaSeriesLayer control = d as AreaSeriesLayer;

                if (control == null) return;

                Style config = e.NewValue as Style;

            }));
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            Path path = new Path();

            path.Style = this.PathStyle;

            PolyLineSegment area = new PolyLineSegment();

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                double x = this.xAxis[i];

                double y = this.Data[i];

                //  Do ：添加曲线
                area.Points.Add(new Point(this.GetX(x, this.ActualWidth), this.GetY(y, this.ActualHeight)));


            }

            area.Points.Add(new Point(this.ActualWidth, this.ActualHeight));

            area.Points.Add(new Point(0, this.ActualHeight));

            area.Points.Add(area.Points.FirstOrDefault());

            //PathFigure pf = new PathFigure();
            //pf.StartPoint = pls.Points.FirstOrDefault();
            //pf.Segments.Add(pls);

            PathFigure pf = new PathFigure();
            pf.StartPoint = area.Points.FirstOrDefault();
            pf.Segments.Add(area);

            //PathFigure pf1 = new PathFigure();
            //{
            //    PolyLineSegment area = new PolyLineSegment();

            //    area.Points.Add(pls.Points.LastOrDefault());

            //    area.Points.Add(new Point(this.ActualWidth, this.ActualHeight));

            //    area.Points.Add(new Point(0, this.ActualHeight));

            //    area.Points.Add(pls.Points.FirstOrDefault());

            //    pf1.StartPoint = area.Points.FirstOrDefault();

            //    pf1.Segments.Add(area);
            //}


            PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

            path.Data = pg;

            this.Children.Add(path);

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

}
