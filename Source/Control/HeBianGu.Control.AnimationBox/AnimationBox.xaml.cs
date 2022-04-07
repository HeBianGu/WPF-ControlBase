// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.AnimationBox
{
    public class AnimationBox : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(AnimationBox), "S.AnimationBox.Default");
        public static ComponentResourceKey RandomOSTKey => new ComponentResourceKey(typeof(AnimationBox), "S.AnimationBox.Random.OST");

        static AnimationBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimationBox), new FrameworkPropertyMetadata(typeof(AnimationBox)));
        }

        public double SplitMilliSecond
        {
            get { return (double)GetValue(SplitMilliSecondProperty); }
            set { SetValue(SplitMilliSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitMilliSecondProperty =
            DependencyProperty.Register("SplitMilliSecond", typeof(double), typeof(AnimationBox), new PropertyMetadata(default(double), (d, e) =>
             {
                 AnimationBox control = d as AnimationBox;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RepeatBehaviorProperty =
            DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(AnimationBox), new PropertyMetadata(default(RepeatBehavior), (d, e) =>
             {
                 AnimationBox control = d as AnimationBox;

                 if (control == null) return;

                 //RepeatBehavior config = e.NewValue as RepeatBehavior;

             }));

    }


}
