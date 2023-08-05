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


        public Style ScrollViewStyle
        {
            get { return (Style)GetValue(ScrollViewStyleProperty); }
            set { SetValue(ScrollViewStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScrollViewStyleProperty =
            DependencyProperty.Register("ScrollViewStyle", typeof(Style), typeof(StoryBoardPlayer), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));



        public double TickFrequency
        {
            get { return (double)GetValue(TickFrequencyProperty); }
            set { SetValue(TickFrequencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickFrequencyProperty =
            DependencyProperty.Register("TickFrequency", typeof(double), typeof(StoryBoardPlayer), new FrameworkPropertyMetadata(1.0, (d, e) =>
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




        public int TickSplitValue
        {
            get { return (int)GetValue(TickSplitValueProperty); }
            set { SetValue(TickSplitValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickSplitValueProperty =
            DependencyProperty.Register("TickSplitValue", typeof(int), typeof(StoryBoardPlayer), new FrameworkPropertyMetadata(20, (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));


        public int TickSmallSplitValue
        {
            get { return (int)GetValue(TickSmallSplitValueProperty); }
            set { SetValue(TickSmallSplitValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TickSmallSplitValueProperty =
            DependencyProperty.Register("TickSmallSplitValue", typeof(int), typeof(StoryBoardPlayer), new FrameworkPropertyMetadata(1, (d, e) =>
             {
                 StoryBoardPlayer control = d as StoryBoardPlayer;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));




    }




}