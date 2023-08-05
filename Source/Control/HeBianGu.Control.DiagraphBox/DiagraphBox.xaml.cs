// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.Control.DiagraphBox
{
    public class DiagraphBox : FrameworkElement
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DiagraphBox), "S.DiagraphBox.Default");

        static DiagraphBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagraphBox), new FrameworkPropertyMetadata(typeof(DiagraphBox)));
        }

        public double StartX
        {
            get { return (double)GetValue(StartXProperty); }
            set { SetValue(StartXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartXProperty =
            DependencyProperty.Register("StartX", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(0.0, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                     control.InvalidateVisual();
                 }

             }));


        public double StartY
        {
            get { return (double)GetValue(StartYProperty); }
            set { SetValue(StartYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartYProperty =
            DependencyProperty.Register("StartY", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(0.0, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                     control.InvalidateVisual();
                 }

             }));


        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register("Scale", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {
                     control.InvalidateVisual();
                 }

             }));



        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(DiagraphBox), new PropertyMetadata(Brushes.Black, (d, e) =>
            {
                DiagraphBox control = d as DiagraphBox;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

                control.InvalidateVisual();

            }));


        public int SplitValue
        {
            get { return (int)GetValue(SplitValueProperty); }
            set { SetValue(SplitValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitValueProperty =
            DependencyProperty.Register("SplitValue", typeof(int), typeof(DiagraphBox), new PropertyMetadata(6, (d, e) =>
            {
                DiagraphBox control = d as DiagraphBox;

                if (control == null) return;

                //double config = e.NewValue as double;

                control.InvalidateVisual();

            }));


        public int SmallSplitValue
        {
            get { return (int)GetValue(SmallSplitValueProperty); }
            set { SetValue(SmallSplitValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallSplitValueProperty =
            DependencyProperty.Register("SmallSplitValue", typeof(int), typeof(DiagraphBox), new FrameworkPropertyMetadata(1, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {
                     control.InvalidateVisual();
                 }

             }));



        public VerticalAlignment TickVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TickVerticalAlignmentProperty); }
            set { SetValue(TickVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickVerticalAlignmentProperty =
            DependencyProperty.Register("TickVerticalAlignment", typeof(VerticalAlignment), typeof(DiagraphBox), new FrameworkPropertyMetadata(VerticalAlignment.Bottom, (d, e) =>
            {
                DiagraphBox control = d as DiagraphBox;

                if (control == null) return;

                if (e.OldValue is VerticalAlignment o)
                {

                }

                if (e.NewValue is VerticalAlignment n)
                {

                }

            }));



        public bool UseText
        {
            get { return (bool)GetValue(UseTextProperty); }
            set { SetValue(UseTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseTextProperty =
            DependencyProperty.Register("UseText", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseTop
        {
            get { return (bool)GetValue(UseTopProperty); }
            set { SetValue(UseTopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseTopProperty =
            DependencyProperty.Register("UseTop", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseBottom
        {
            get { return (bool)GetValue(UseBottomProperty); }
            set { SetValue(UseBottomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBottomProperty =
            DependencyProperty.Register("UseBottom", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseLeft
        {
            get { return (bool)GetValue(UseLeftProperty); }
            set { SetValue(UseLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseLeftProperty =
            DependencyProperty.Register("UseLeft", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseRight
        {
            get { return (bool)GetValue(UseRightProperty); }
            set { SetValue(UseRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseRightProperty =
            DependencyProperty.Register("UseRight", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseBorder
        {
            get { return (bool)GetValue(UseBorderProperty); }
            set { SetValue(UseBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBorderProperty =
            DependencyProperty.Register("UseBorder", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));



        public bool UseSplitVerticalLine
        {
            get { return (bool)GetValue(UseSplitVerticalLineProperty); }
            set { SetValue(UseSplitVerticalLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSplitVerticalLineProperty =
            DependencyProperty.Register("UseSplitVerticalLine", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseSmallSplitVerticalLine
        {
            get { return (bool)GetValue(UseSmallSplitVerticalLineProperty); }
            set { SetValue(UseSmallSplitVerticalLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSmallSplitVerticalLineProperty =
            DependencyProperty.Register("UseSmallSplitVerticalLine", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));

        public bool UseSplitHorizontalLine
        {
            get { return (bool)GetValue(UseSplitHorizontalLineProperty); }
            set { SetValue(UseSplitHorizontalLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSplitHorizontalLineProperty =
            DependencyProperty.Register("UseSplitHorizontalLine", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public bool UseSmallSpliteHorizontalLine
        {
            get { return (bool)GetValue(UseSmallSpliteHorizontalLineProperty); }
            set { SetValue(UseSmallSpliteHorizontalLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseSmallSpliteHorizontalLineProperty =
            DependencyProperty.Register("UseSmallSpliteHorizontalLine", typeof(bool), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.InvalidateVisual();
                 }
             }));


        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(12.0, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {
                     control.InvalidateVisual();
                 }

             }));

        public double SmallSplitStrokeThickness
        {
            get { return (double)GetValue(SmallSplitStrokeThicknessProperty); }
            set { SetValue(SmallSplitStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallSplitStrokeThicknessProperty =
            DependencyProperty.Register("SmallSplitStrokeThickness", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(0.5, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public Brush GridLineBrush
        {
            get { return (Brush)GetValue(GridLineBrushProperty); }
            set { SetValue(GridLineBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridLineBrushProperty =
            DependencyProperty.Register("GridLineBrush", typeof(Brush), typeof(DiagraphBox), new FrameworkPropertyMetadata(Brushes.Black, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public Brush SmallSplitBrush
        {
            get { return (Brush)GetValue(SmallSplitBrushProperty); }
            set { SetValue(SmallSplitBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallSplitBrushProperty =
            DependencyProperty.Register("SmallSplitBrush", typeof(Brush), typeof(DiagraphBox), new FrameworkPropertyMetadata(Brushes.Black, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public Brush SplitBrush
        {
            get { return (Brush)GetValue(SplitBrushProperty); }
            set { SetValue(SplitBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitBrushProperty =
            DependencyProperty.Register("SplitBrush", typeof(Brush), typeof(DiagraphBox), new FrameworkPropertyMetadata(Brushes.Black, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(DiagraphBox), new FrameworkPropertyMetadata(Brushes.Transparent, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public Brush GridBackground
        {
            get { return (Brush)GetValue(GridBackgroundProperty); }
            set { SetValue(GridBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridBackgroundProperty =
            DependencyProperty.Register("GridBackground", typeof(Brush), typeof(DiagraphBox), new FrameworkPropertyMetadata(Brushes.Transparent, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {
                     control.InvalidateVisual();
                 }

             }));



        public double SplitStrokeThickess
        {
            get { return (double)GetValue(SplitStrokeThickessProperty); }
            set { SetValue(SplitStrokeThickessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitStrokeThickessProperty =
            DependencyProperty.Register("SplitStrokeThickess", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {
                     control.InvalidateVisual();
                 }

             }));



        public Thickness GridMargin
        {
            get { return (Thickness)GetValue(GridMarginProperty); }
            set { SetValue(GridMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridMarginProperty =
            DependencyProperty.Register("GridMargin", typeof(Thickness), typeof(DiagraphBox), new FrameworkPropertyMetadata(default(Thickness), (d, e) =>
             {
                 DiagraphBox control = d as DiagraphBox;

                 if (control == null) return;

                 if (e.OldValue is Thickness o)
                 {

                 }

                 if (e.NewValue is Thickness n)
                 {
                     control.InvalidateVisual();
                 }

             }));


        public double SmallLen
        {
            get { return (double)GetValue(SmallLenProperty); }
            set { SetValue(SmallLenProperty, value); }
        }

        public static readonly DependencyProperty SmallLenProperty =
            DependencyProperty.Register("SmallLen", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(10.0, (d, e) =>
            {
                DiagraphBox control = d as DiagraphBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {
                    control.InvalidateVisual();
                }

            }));


        public double LargeLen
        {
            get { return (double)GetValue(LargeLenProperty); }
            set { SetValue(LargeLenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LargeLenProperty =
            DependencyProperty.Register("LargeLen", typeof(double), typeof(DiagraphBox), new FrameworkPropertyMetadata(15.0, (d, e) =>
            {
                DiagraphBox control = d as DiagraphBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {
                    control.InvalidateVisual();
                }

            }));
        public FormattedText CreateFormattedText(double i)
        {
            string text = (Math.Ceiling(i / Scale)).ToString();

#if NETCOREAPP
            return new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 10, this.Foreground, 1);
#endif

#if NETFRAMEWORK
            return new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 10, this.Foreground);
#endif
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.ActualHeight == 0 || this.ActualWidth == 0)
                return;
            dc.DrawRectangle(this.Background, new Pen(this.GridBackground, 0), new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            dc.DrawRectangle(this.GridBackground, new Pen(this.GridBackground, 0), new Rect(this.GridMargin.Left, this.GridMargin.Top, this.ActualWidth - this.GridMargin.Left - this.GridMargin.Right, this.ActualHeight - this.GridMargin.Top - this.GridMargin.Bottom));

            this.DrawSplit(dc);
            if (this.UseBorder)
            {
                dc.DrawRectangle(Brushes.Transparent, new Pen(this.SplitBrush, this.SplitStrokeThickess), new Rect(this.GridMargin.Left, this.GridMargin.Top, this.ActualWidth - this.GridMargin.Left - this.GridMargin.Right, this.ActualHeight - this.GridMargin.Top - this.GridMargin.Bottom));
            }
        }


        protected virtual void DrawSplit(DrawingContext dc)
        {
            double splitvalue = this.SplitValue * Scale;
            double smallSplitvalue = this.SmallSplitValue * Scale;

            Pen pen = new Pen(this.SplitBrush, this.SplitStrokeThickess);
            Pen penSmall = new Pen(this.SmallSplitBrush, this.SmallSplitStrokeThickness);

            Pen penGrid = new Pen(this.GridLineBrush, this.SplitStrokeThickess);
            Pen penGridSmall = new Pen(this.GridLineBrush, this.SmallSplitStrokeThickness);

            {
                double max = this.ActualWidth - this.GridMargin.Right - this.GridMargin.Left;
                double x = this.StartX == 0 ? 0 : splitvalue - this.StartX % splitvalue;
                while (x <= max)
                {
                    var formattedText = this.CreateFormattedText(x + this.StartX);
                    if (this.UseTop)
                    {
                        dc.DrawLine(pen, new Point(x + this.GridMargin.Left, this.GridMargin.Top), new Point(x + this.GridMargin.Left, this.GridMargin.Top - this.LargeLen));
                        if (this.UseText)
                        {
                            dc.DrawText(formattedText, new Point(x + this.GridMargin.Left, this.GridMargin.Top - this.LargeLen - formattedText.Height));
                        }
                    }

                    if (this.UseBottom)
                    {
                        dc.DrawLine(pen, new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom), new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom + this.LargeLen));
                        if (this.UseText)
                        {
                            dc.DrawText(formattedText, new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom + this.LargeLen));
                        }
                    }

                    if (this.UseSplitVerticalLine)
                    {
                        dc.DrawLine(penGrid, new Point(x + this.GridMargin.Left, this.GridMargin.Top), new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom));
                    }

                    x += splitvalue;
                }

                x = this.StartX == 0 ? 0 : smallSplitvalue - this.StartX % smallSplitvalue;
                while (x <= max)
                {
                    if (this.UseTop)
                    {
                        dc.DrawLine(penSmall, new Point(x + this.GridMargin.Left, this.GridMargin.Top), new Point(x + this.GridMargin.Left, this.GridMargin.Top - this.SmallLen));
                    }
                    if (this.UseBottom)
                    {
                        dc.DrawLine(penSmall, new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom), new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom + this.SmallLen));
                    }
                    if (this.UseSmallSplitVerticalLine)
                    {
                        dc.DrawLine(penGridSmall, new Point(x + this.GridMargin.Left, this.GridMargin.Top), new Point(x + this.GridMargin.Left, this.ActualHeight - this.GridMargin.Bottom));
                    }
                    x += smallSplitvalue;
                }
            }

            {
                double max = this.ActualHeight - this.GridMargin.Top - this.GridMargin.Bottom;
                double x = this.StartY == 0 ? 0 : splitvalue - this.StartY % splitvalue;
                while (x <= max)
                {
                    var formattedText = this.CreateFormattedText(x + this.StartY);

                    if (this.UseLeft)
                    {
                        dc.DrawLine(pen, new Point(this.GridMargin.Left, x + this.GridMargin.Top), new Point(this.GridMargin.Left - this.LargeLen, x + this.GridMargin.Top));
                        if (this.UseText)
                        {
                            dc.DrawText(formattedText, new Point(this.GridMargin.Left - formattedText.Width, x + this.GridMargin.Top));
                        }
                    }

                    if (this.UseRight)
                    {
                        dc.DrawLine(pen, new Point(this.ActualWidth - this.GridMargin.Right, x + this.GridMargin.Top), new Point(this.ActualWidth - this.GridMargin.Right + this.LargeLen, x + this.GridMargin.Top));
                        if (this.UseText)
                        {
                            dc.DrawText(formattedText, new Point(this.ActualWidth - this.GridMargin.Right, x + this.GridMargin.Top));
                        }
                    }

                    if (this.UseSplitHorizontalLine)
                    {
                        double y = this.LargeLen;
                        dc.DrawLine(penGrid, new Point(this.GridMargin.Left, x + this.GridMargin.Top), new Point(this.ActualWidth - this.GridMargin.Right, x + this.GridMargin.Top));
                    }

                    x += splitvalue;
                }

                x = this.StartY == 0 ? 0 : smallSplitvalue - this.StartY % smallSplitvalue;
                while (x <= max)
                {
                    if (this.UseLeft)
                    {
                        dc.DrawLine(penSmall, new Point(this.GridMargin.Left, x + this.GridMargin.Top), new Point(this.GridMargin.Left - this.SmallLen, x + this.GridMargin.Top));
                    }
                    if (this.UseRight)
                    {
                        dc.DrawLine(penSmall, new Point(this.ActualWidth - this.GridMargin.Right, x + this.GridMargin.Top), new Point(this.ActualWidth - this.GridMargin.Right + this.SmallLen, x + this.GridMargin.Top));
                    }
                    if (this.UseSplitHorizontalLine)
                    {
                        dc.DrawLine(penGridSmall, new Point(this.GridMargin.Left, x + this.GridMargin.Top), new Point(this.ActualWidth - this.GridMargin.Right, x + this.GridMargin.Top));
                    }
                    x += smallSplitvalue;
                }
            }
        }


    }
}
