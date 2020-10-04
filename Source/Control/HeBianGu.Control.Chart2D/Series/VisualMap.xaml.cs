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
        public ObservableCollection<SolidColorBrush> MarkBrushes
        {
            get { return (ObservableCollection<SolidColorBrush>)GetValue(MarkBrushesProperty); }
            set { SetValue(MarkBrushesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkBrushesProperty =
            DependencyProperty.Register("MarkBrushes", typeof(ObservableCollection<SolidColorBrush>), typeof(VisualMap), new PropertyMetadata(new ObservableCollection<SolidColorBrush>(), (d, e) =>
             {
                 VisualMap control = d as VisualMap;

                 if (control == null) return;

                 ObservableCollection<SolidColorBrush> config = e.NewValue as ObservableCollection<SolidColorBrush>;

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
                        path.Fill = new LinearGradientBrush((this.Foreground as SolidColorBrush).Color, (this.Foreground as SolidColorBrush).Color, new Point(0, 0), new Point(0, 1));
                        
                    }
                    else
                    {
                        path.Fill = new LinearGradientBrush(this.MarkBrushes[i - 1].Color, this.MarkBrushes[i].Color, new Point(0, 0), new Point(0, 1));

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
