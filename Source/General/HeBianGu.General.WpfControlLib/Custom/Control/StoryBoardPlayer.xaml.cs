using HeBianGu.Base.WpfBase;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.General.WpfControlLib
{

    public class StoryBoardPlayer : ItemsControl
    {
        public Thickness SliderMargin
        {
            get { return (Thickness)GetValue(SliderMarginProperty); }
            set { SetValue(SliderMarginProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderMarginProperty =
            DependencyProperty.Register("SliderMargin", typeof(Thickness), typeof(StoryBoardPlayer), new PropertyMetadata(default(Thickness), (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 //Thickness config = e.NewValue as Thickness;

             }));


        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(StoryBoardPlayer), new PropertyMetadata(default(double), (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(StoryBoardPlayer), new PropertyMetadata(default(double), (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(StoryBoardPlayer), new PropertyMetadata(default(double), (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));




    }




}