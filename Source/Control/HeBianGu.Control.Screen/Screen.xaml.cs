// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Screen
{
    public class Screen : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Screen), "S.Screen.Default");

        static Screen()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Screen), new FrameworkPropertyMetadata(typeof(Screen)));
        }

        public object Back
        {
            get { return GetValue(BackProperty); }
            set { SetValue(BackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackProperty =
            DependencyProperty.Register("Back", typeof(object), typeof(Screen), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 Screen control = d as Screen;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));


        public object Cover
        {
            get { return GetValue(CoverProperty); }
            set { SetValue(CoverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverProperty =
            DependencyProperty.Register("Cover", typeof(object), typeof(Screen), new FrameworkPropertyMetadata(default(object), (d, e) =>
             {
                 Screen control = d as Screen;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));



        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(Screen), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                ScreenBackground control = d as ScreenBackground;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));


    }
}
