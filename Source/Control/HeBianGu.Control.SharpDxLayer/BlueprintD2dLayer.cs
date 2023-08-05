using HeBianGu.Base.WpfBase;
using HeBianGu.Control.LayerBox;
using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.SharpDxLayer
{
    [Displayer(Name = "图纸")]
    public class BlueprintSharpDxLayer : SharpDxLayer, IBlueprintLayer
    {
        public BlueprintSharpDxLayer()
        {
            Fill = Brushes.White;
            Stroke = Brushes.LightGray;
            Dash = null;
        }


        public double PaperWidth
        {
            get { return (double)GetValue(PaperWidthProperty); }
            set { SetValue(PaperWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaperWidthProperty =
            DependencyProperty.Register("PaperWidth", typeof(double), typeof(BlueprintSharpDxLayer), new FrameworkPropertyMetadata(1200.0, (d, e) =>
            {
                BlueprintSharpDxLayer control = d as BlueprintSharpDxLayer;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        public double PaperHeight
        {
            get { return (double)GetValue(PaperHeightProperty); }
            set { SetValue(PaperHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PaperHeightProperty =
            DependencyProperty.Register("PaperHeight", typeof(double), typeof(BlueprintSharpDxLayer), new FrameworkPropertyMetadata(800.0, (d, e) =>
            {
                BlueprintSharpDxLayer control = d as BlueprintSharpDxLayer;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));

        public Size GetSize()
        {
            return new Size(this.PaperWidth, this.PaperHeight);
        }

        public Point Center
        {
            get { return (Point)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(BlueprintSharpDxLayer), new FrameworkPropertyMetadata(new Point(), (d, e) =>
            {
                BlueprintSharpDxLayer control = d as BlueprintSharpDxLayer;

                if (control == null) return;

                if (e.OldValue is Point o)
                {

                }

                if (e.NewValue is Point n)
                {

                }
                control.RefreshDraw();
            }));

        public bool UseCenterLine
        {
            get { return (bool)GetValue(UseCenterLineProperty); }
            set { SetValue(UseCenterLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseCenterLineProperty =
            DependencyProperty.Register("UseCenterLine", typeof(bool), typeof(BlueprintSharpDxLayer), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                BlueprintSharpDxLayer control = d as BlueprintSharpDxLayer;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        public override void OnScaleChanged(object sender, ObjectRoutedEventArgs<Tuple<double, double>> e)
        {
            base.OnScaleChanged(sender, e);
            this.RefreshDraw();
        }


        public bool UseBorder
        {
            get { return (bool)GetValue(UseBorderProperty); }
            set { SetValue(UseBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBorderProperty =
            DependencyProperty.Register("UseBorder", typeof(bool), typeof(BlueprintSharpDxLayer), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                BlueprintSharpDxLayer control = d as BlueprintSharpDxLayer;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public Thickness BorderMargin
        {
            get { return (Thickness)GetValue(BorderMarginProperty); }
            set { SetValue(BorderMarginProperty, value); }
        }

        //public double DPI => throw new NotImplementedException();

        //public Point Offset => throw new NotImplementedException();

        //public double Scale => throw new NotImplementedException();

        public override Rect ContentBounds => new Rect(this.Center.X - this.PaperWidth / 2, this.Center.Y - this.PaperHeight / 2, this.PaperWidth, this.PaperHeight);


        public double Scale { get; protected set; }

        public double DPI => Scale;

        public Point Offset { get; protected set; }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderMarginProperty =
            DependencyProperty.Register("BorderMargin", typeof(Thickness), typeof(BlueprintSharpDxLayer), new FrameworkPropertyMetadata(new Thickness(20), (d, e) =>
            {
                BlueprintSharpDxLayer control = d as BlueprintSharpDxLayer;

                if (control == null) return;

                if (e.OldValue is Thickness o)
                {

                }

                if (e.NewValue is Thickness n)
                {

                }

            }));



        public override void Draw(IDrawing dc)
        {
            base.Draw(dc);

            if (this.PaperWidth == 0 || this.PaperHeight == 0)
                return;
            if (dc.LayerView.ActualWidth == 0 || dc.LayerView.ActualHeight == 0)
                return;
            Rect rect = new Rect(this.Center.X - this.PaperWidth / 2, this.Center.Y - this.PaperHeight / 2, this.PaperWidth, this.PaperHeight);

            dc.DrawRectangle(this.Fill, this.GetPen(), rect);

            if (this.UseBorder)
                //dc.DrawRectangle(Brushes.Transparent, GetPen(), new Rect(rect.Left + BorderMargin.Left, rect.Top + BorderMargin.Top, rect.Width - BorderMargin.Left - BorderMargin.Right, rect.Height - BorderMargin.Top - BorderMargin.Bottom));

                if (this.UseCenterLine)
                {
                    dc.DrawLine(this.GetDashPen(), new Point(this.Center.X, this.Center.Y - this.PaperHeight / 2), new Point(this.Center.X, this.Center.Y + this.PaperHeight / 2));
                    dc.DrawLine(this.GetDashPen(), new Point(this.Center.X - this.PaperWidth / 2, this.Center.Y), new Point(this.Center.X + this.PaperWidth / 2, this.Center.Y));
                    //var format = DrawingContextExtension.GetFormattedText("[0,0]", Math.Min(12 / dc.DPI, 30000), this.Stroke);
                    //dc.DrawText(format, this.Center);
                }
        }

        public Point DeviceToLogical(Point pt)
        {
            throw new NotImplementedException();
        }

        public Rect DeviceToLogical(Rect rc)
        {
            throw new NotImplementedException();
        }

        public Point LogicalToDevice(Point pt)
        {
            throw new NotImplementedException();
        }

        public Rect LogicalToDevice(Rect rc)
        {
            throw new NotImplementedException();
        }
    }

}
