// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace HeBianGu.Control.StoryBoard
{

    public class StoryBoardPlayer : ItemsControl
    {
        static StoryBoardPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StoryBoardPlayer), new FrameworkPropertyMetadata(typeof(StoryBoardPlayer)));
        }

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


        [TypeConverter(typeof(LengthConverter))]
        public double SliderWidth
        {
            get { return (double)GetValue(SliderWidthProperty); }
            set { SetValue(SliderWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderWidthProperty =
            DependencyProperty.Register("SliderWidth", typeof(double), typeof(StoryBoardPlayer), new FrameworkPropertyMetadata(default(double), (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

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