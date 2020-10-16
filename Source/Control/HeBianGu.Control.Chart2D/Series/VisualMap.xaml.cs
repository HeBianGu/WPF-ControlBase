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
    public class VisualMap : Layer
    {
        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public ObservableCollection<Color> MarkBrushes
        {
            get { return (ObservableCollection<Color>)GetValue(MarkBrushesProperty); }
            set { SetValue(MarkBrushesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushesProperty =
            DependencyProperty.Register("MarkBrushes", typeof(ObservableCollection<Color>), typeof(VisualMap), new PropertyMetadata(new ObservableCollection<Color>(), (d, e) =>
             {
                 VisualMap control = d as VisualMap;

                 if (control == null) return;

                 ObservableCollection<Color> config = e.NewValue as ObservableCollection<Color>;

             }));

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            for (int i = 0; i < this.Data.Count; i++)
            {
                if (i % 2 == 0) continue;

                double y_1 = this.Data[i - 1];

                double y = this.Data[i];
                {
                    //  Do ：添加标定线
                    Path path = new Path();

                    path.Style = this.PathStyle;

                    if(this.MarkBrushes.Count < i)
                    {
                        path.Fill = this.Foreground;
                    }
                
                    else
                    {
                        path.Fill = new LinearGradientBrush(this.MarkBrushes[i - 1], this.MarkBrushes[i], new Point(0, 0), new Point(0, 1));

                    }

                    path.Stroke = path.Fill;

                    PolyLineSegment pls = new PolyLineSegment();

                    pls.Points.Add(new Point(this.GetX(this.minX, this.ActualWidth), this.GetY(y_1, this.ActualHeight)));
                    pls.Points.Add(new Point(this.GetX(this.maxX, this.ActualWidth), this.GetY(y_1, this.ActualHeight)));

                    pls.Points.Add(new Point(this.GetX(this.maxX, this.ActualWidth), this.GetY(y, this.ActualHeight)));
                    pls.Points.Add(new Point(this.GetX(this.minX, this.ActualWidth), this.GetY(y, this.ActualHeight)));


                    pls.Points.Add(pls.Points.FirstOrDefault());

                    PathFigure pf = new PathFigure();
                    pf.StartPoint = pls.Points.FirstOrDefault();
                    pf.Segments.Add(pls);

                    PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                    path.Data = pg;

                    this.Children.Add(path);
                }
            }
        }
    }

}
