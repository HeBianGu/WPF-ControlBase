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
    public class FlagTip : Layer
    {
        public FlagTip()
        {
            this.MouseMove += FlagTip_MouseMove;

            this.MouseEnter += (l, k) => this.RefreshVisibility();

            this.MouseLeave += (l, k) => this.RefreshVisibility();

        }

        void RefreshVisibility()
        {
            this.X.Visibility = this.IsMouseOver && (this.FlagTipType == FlagTipType.Cross || this.FlagTipType == FlagTipType.CrossStep || this.FlagTipType == FlagTipType.OnlyX || this.FlagTipType == FlagTipType.StepX) ? Visibility.Visible : Visibility.Collapsed;
            this.Y.Visibility = this.IsMouseOver && (this.FlagTipType == FlagTipType.Cross || this.FlagTipType == FlagTipType.CrossStep || this.FlagTipType == FlagTipType.OnlyY || this.FlagTipType == FlagTipType.StepY) ? Visibility.Visible : Visibility.Collapsed;

            this.Label.Visibility = this.IsMouseOver ? Visibility.Visible : Visibility.Collapsed;
        }

        private void FlagTip_MouseMove(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(this);

            double xValue = (this.maxX - this.minX) * (position.X / this.ActualWidth) + this.minX;

            double yValue = (this.maxY - this.minY) * (position.Y / this.ActualHeight) + this.minY;

            if (this.FlagTipType == FlagTipType.CrossStep || this.FlagTipType == FlagTipType.StepY || this.FlagTipType == FlagTipType.StepX)
            {
                double mx = this.xAxis.Min(l => Math.Abs(l - xValue));
                var xFind = this.xAxis.FirstOrDefault(l => Math.Abs(l - xValue) == mx);
                Canvas.SetLeft(this.Y, this.GetX(xFind));

                double my = this.xAxis.Min(l => Math.Abs(l - yValue));
                var yFind = this.xAxis.FirstOrDefault(l => Math.Abs(l - yValue) == my);
                Canvas.SetBottom(this.X, this.GetY(yFind));
                this.Label.Content = $"({xFind},{yFind})";
            }
            else
            {
                Canvas.SetLeft(this.Y, position.X);
                Canvas.SetTop(this.X, position.Y);
                this.Label.Content = $"({Math.Round(xValue, 2)},{Math.Round(yValue, 2)})";
            }


            Canvas.SetLeft(this.Label, position.X);
            Canvas.SetTop(this.Label, position.Y);
        }

        public Path X
        {
            get { return (Path)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(Path), typeof(FlagTip), new PropertyMetadata(default(Path), (d, e) =>
             {
                 FlagTip control = d as FlagTip;

                 if (control == null) return;

                 Path config = e.NewValue as Path;

             }));

        public Path Y
        {
            get { return (Path)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(Path), typeof(FlagTip), new PropertyMetadata(default(Path), (d, e) =>
             {
                 FlagTip control = d as FlagTip;

                 if (control == null) return;

                 Path config = e.NewValue as Path;

             }));


        public Label Label
        {
            get { return (Label)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(Label), typeof(FlagTip), new PropertyMetadata(default(Label), (d, e) =>
             {
                 FlagTip control = d as FlagTip;

                 if (control == null) return;

                 Label config = e.NewValue as Label;

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.X = new Path();

            this.X.Style = this.PathStyle;

            {
                PolyLineSegment pls = new PolyLineSegment();

                pls.Points.Add(new Point(this.GetX(this.minX), this.GetY(this.maxY)));
                pls.Points.Add(new Point(this.GetX(this.maxX), this.GetY(this.maxY)));

                PathFigure pf = new PathFigure();
                pf.StartPoint = pls.Points.FirstOrDefault();
                pf.Segments.Add(pls);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                pg.Figures.Add(pf);

                this.X.Data = pg;

                this.Children.Add(this.X);
            }

            this.Y = new Path();

            this.Y.Style = this.PathStyle;

            {
                PolyLineSegment pls = new PolyLineSegment();
                pls.Points.Add(new Point(this.GetX(this.minX), this.GetY(this.minY)));
                pls.Points.Add(new Point(this.GetX(this.minX), this.GetY(this.maxY)));

                PathFigure pf = new PathFigure();
                pf.StartPoint = pls.Points.FirstOrDefault();
                pf.Segments.Add(pls);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                pg.Figures.Add(pf);

                this.Y.Data = pg;

                this.Children.Add(this.Y);
            }

            //  Do ：显示文本
            this.Label = new Label();

            this.Label.Style = this.LabelStyle;

            canvas.Children.Add(this.Label);
        }


        public FlagTipType FlagTipType
        {
            get { return (FlagTipType)GetValue(FlagTipTypeProperty); }
            set { SetValue(FlagTipTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlagTipTypeProperty =
            DependencyProperty.Register("FlagTipType", typeof(FlagTipType), typeof(FlagTip), new PropertyMetadata(default(FlagTipType), (d, e) =>
             {
                 FlagTip control = d as FlagTip;

                 if (control == null) return;

                 //FlagTipType config = e.NewValue as FlagTipType;

             }));
    }

    public enum FlagTipType
    {
        Cross, OnlyX, OnlyY, StepX, StepY, CrossStep
    }


}
