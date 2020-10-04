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
    public class MarkLine : Layer
    {

        public Style TrangleStyle
        {
            get { return (Style)GetValue(TrangleStyleProperty); }
            set { SetValue(TrangleStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrangleStyleProperty =
            DependencyProperty.Register("TrangleStyle", typeof(Style), typeof(MarkLine), new PropertyMetadata(default(Style), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public ObservableCollection<Brush> MarkBrushes
        {
            get { return (ObservableCollection<Brush>)GetValue(MarkBrushesProperty); }
            set { SetValue(MarkBrushesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushesProperty =
            DependencyProperty.Register("MarkBrushes", typeof(ObservableCollection<Brush>), typeof(MarkLine), new PropertyMetadata(new ObservableCollection<Brush>(), (d, e) =>
             {
                 MarkLine control = d as MarkLine;

                 if (control == null) return;

                 ObservableCollection<Brush> config = e.NewValue as ObservableCollection<Brush>;

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            for (int i = 0; i < this.Data.Count; i++)
            {
                double y = this.Data[i];

                {
                    //  Do ：添加标定线
                    Path path = new Path();
                    path.Style = this.PathStyle;

                    if(this.MarkBrushes.Count > i)
                    {
                        path.Stroke = this.MarkBrushes[i];
                    }
                    else
                    {
                        path.Stroke = this.Foreground;
                    }

                    PolyLineSegment pls = new PolyLineSegment();
                    pls.Points.Add(new Point(this.GetX(this.minX, this.ActualWidth), this.GetY(y, this.ActualHeight)));
                    pls.Points.Add(new Point(this.GetX(this.maxX, this.ActualWidth), this.GetY(y, this.ActualHeight)));

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = pls.Points.FirstOrDefault();
                    pf.Segments.Add(pls);

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    pg.Figures.Add(pf);

                    path.Data = pg;

                    this.Children.Add(path);
                }

                //  Do ：增加箭头 
                {
                    //  Do ：添加标定线
                    Path path = new Path();
                    path.Style = this.TrangleStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        path.Stroke = this.MarkBrushes[i];
                    }
                    else
                    {
                        path.Stroke = this.Foreground;
                    }

                    path.Fill = path.Stroke;

                    PolyLineSegment pls = new PolyLineSegment();

                    pls.Points.Add(new Point(0, 0));
                    pls.Points.Add(new Point(50, 10));
                    pls.Points.Add(new Point(0, 20));
                    pls.Points.Add(new Point(20, 10));
                    pls.Points.Add(new Point(0, 0));

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = pls.Points.FirstOrDefault();
                    pf.Segments.Add(pls);

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    path.Data = pg;

                    Canvas.SetTop(path, this.GetY(y, this.ActualHeight) - 20 / 2);
                    Canvas.SetLeft(path, this.GetX(this.maxX, this.ActualWidth) - 50);

                    this.Children.Add(path);
                }

                //  Do ：绘制文本
                TextBlock text = new TextBlock();
                text.Text = y.ToString();
                text.Style = this.TextStyle;
 
                if (this.MarkBrushes.Count > i)
                {
                    text.Foreground = this.MarkBrushes[i];
                }
                else
                {
                    text.Foreground = this.Foreground;
                }

                Canvas.SetTop(text, this.GetY(y, this.ActualHeight) - 20 / 2);
                Canvas.SetLeft(text, this.GetX(this.maxX, this.ActualWidth) + 50 / 2);

                this.Children.Add(text);

                //  Do ：显示标记
                if (this.MarkStyle == null) return;

                EllipseMarker m = Activator.CreateInstance(this.MarkStyle.TargetType) as EllipseMarker;

                if (m != null)
                {
                    m.Style = this.MarkStyle;

                    if (this.MarkBrushes.Count > i)
                    {
                        m.Stroke = this.MarkBrushes[i];
                    }
                    else
                    {
                        m.Stroke = this.Foreground;
                    }

                    Canvas.SetLeft(m, this.GetX(this.minX, this.ActualWidth));
                    Canvas.SetTop(m, this.GetY(y, this.ActualHeight));
                    this.Children.Add(m);
                }
            }


        }
    }

}
