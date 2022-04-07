// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class ScreenGroupBox : GroupBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenGroupBox), "S.ScreenGroupBox.Default");
        public static ComponentResourceKey CornermarkKey => new ComponentResourceKey(typeof(ScreenGroupBox), "S.ScreenGroupBox.Cornermark");
        public static ComponentResourceKey LineKey => new ComponentResourceKey(typeof(ScreenGroupBox), "S.ScreenGroupBox.Line");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(ScreenGroupBox), "S.ScreenGroupBox.Clear");

        static ScreenGroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenGroupBox), new FrameworkPropertyMetadata(typeof(ScreenGroupBox)));
        }


        public Thickness LeftTopBorderThickness
        {
            get { return (Thickness)GetValue(LeftTopBorderThicknessProperty); }
            set { SetValue(LeftTopBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftTopBorderThicknessProperty =
            DependencyProperty.Register("LeftTopBorderThickness", typeof(Thickness), typeof(ScreenGroupBox), new FrameworkPropertyMetadata(default(Thickness), (d, e) =>
             {
                 ScreenGroupBox control = d as ScreenGroupBox;

                 if (control == null) return;

                 if (e.OldValue is Thickness o)
                 {

                 }

                 if (e.NewValue is Thickness n)
                 {

                 }

             }));


        public Thickness RightTopBorderThickness
        {
            get { return (Thickness)GetValue(RightTopBorderThicknessProperty); }
            set { SetValue(RightTopBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightTopBorderThicknessProperty =
            DependencyProperty.Register("RightTopBorderThickness", typeof(Thickness), typeof(ScreenGroupBox), new FrameworkPropertyMetadata(default(Thickness), (d, e) =>
             {
                 ScreenGroupBox control = d as ScreenGroupBox;

                 if (control == null) return;

                 if (e.OldValue is Thickness o)
                 {

                 }

                 if (e.NewValue is Thickness n)
                 {

                 }

             }));

        public Thickness RightBottomBorderThickness
        {
            get { return (Thickness)GetValue(RightBottomBorderThicknessProperty); }
            set { SetValue(RightBottomBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightBottomBorderThicknessProperty =
            DependencyProperty.Register("RightBottomBorderThickness", typeof(Thickness), typeof(ScreenGroupBox), new FrameworkPropertyMetadata(default(Thickness), (d, e) =>
             {
                 ScreenGroupBox control = d as ScreenGroupBox;

                 if (control == null) return;

                 if (e.OldValue is Thickness o)
                 {

                 }

                 if (e.NewValue is Thickness n)
                 {

                 }

             }));


        public Thickness LeftBottomBorderThickness
        {
            get { return (Thickness)GetValue(LeftBottomBorderThicknessProperty); }
            set { SetValue(LeftBottomBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftBottomBorderThicknessProperty =
            DependencyProperty.Register("LeftBottomBorderThickness", typeof(Thickness), typeof(ScreenGroupBox), new FrameworkPropertyMetadata(default(Thickness), (d, e) =>
             {
                 ScreenGroupBox control = d as ScreenGroupBox;

                 if (control == null) return;

                 if (e.OldValue is Thickness o)
                 {

                 }

                 if (e.NewValue is Thickness n)
                 {

                 }

             }));


        public double CornermarkHeight
        {
            get { return (double)GetValue(CornermarkHeightProperty); }
            set { SetValue(CornermarkHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornermarkHeightProperty =
            DependencyProperty.Register("CornermarkHeight", typeof(double), typeof(ScreenGroupBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
             {
                 ScreenGroupBox control = d as ScreenGroupBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


        public double CornermarkWidth
        {
            get { return (double)GetValue(CornermarkWidthProperty); }
            set { SetValue(CornermarkWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornermarkWidthProperty =
            DependencyProperty.Register("CornermarkWidth", typeof(double), typeof(ScreenGroupBox), new FrameworkPropertyMetadata(default(double), (d, e) =>
             {
                 ScreenGroupBox control = d as ScreenGroupBox;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


    }
}
