// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Screen
{
    public class ScreenCover : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScreenCover), "S.ScreenCover.Default");
        public static ComponentResourceKey Cross1Key => new ComponentResourceKey(typeof(ScreenCover), "S.ScreenCover.Cross.1");
        public static ComponentResourceKey Bacground1Key => new ComponentResourceKey(typeof(ScreenCover), "S.ScreenCover.Bacground.1");

        static ScreenCover()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenCover), new FrameworkPropertyMetadata(typeof(ScreenCover)));
        }


        public Brush ImageBrush
        {
            get { return (Brush)GetValue(ImageBrushProperty); }
            set { SetValue(ImageBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageBrushProperty =
            DependencyProperty.Register("ImageBrush", typeof(Brush), typeof(ScreenCover), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 ScreenCover control = d as ScreenCover;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));

    }
}
