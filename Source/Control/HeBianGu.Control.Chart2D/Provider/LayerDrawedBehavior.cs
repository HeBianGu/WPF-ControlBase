using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 容器内子控件加载时触发动画效果</summary>
    public class LayerDrawedBehavior : Behavior<LayerBase>
    {
        //public Storyboard ItemStoryBoard
        //{
        //    get { return (Storyboard)GetValue(ItemStoryBoardProperty); }
        //    set { SetValue(ItemStoryBoardProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemStoryBoardProperty =
        //    DependencyProperty.Register("ItemStoryBoard", typeof(Storyboard), typeof(LayerDrawedBehavior), new PropertyMetadata(default(Storyboard), (d, e) =>
        //    {
        //        LayerDrawedBehavior control = d as LayerDrawedBehavior;

        //        if (control == null) return;

        //        Storyboard config = e.NewValue as Storyboard;

        //    }));



        //public ParallelTimeline ParallelTimeline
        //{
        //    get { return (ParallelTimeline)GetValue(ParallelTimelineProperty); }
        //    set { SetValue(ParallelTimelineProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ParallelTimelineProperty =
        //    DependencyProperty.Register("ParallelTimeline", typeof(ParallelTimeline), typeof(LayerDrawedBehavior), new PropertyMetadata(new ParallelTimeline(), (d, e) =>
        //     {
        //         LayerDrawedBehavior control = d as LayerDrawedBehavior;

        //         if (control == null) return;

        //         ParallelTimeline config = e.NewValue as ParallelTimeline;

        //     }));


        public ArrayList Timelines
        {
            get { return (ArrayList)GetValue(TimelinesProperty); }
            set { SetValue(TimelinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimelinesProperty =
            DependencyProperty.Register("Timelines", typeof(ArrayList), typeof(LayerDrawedBehavior), new PropertyMetadata(new ArrayList(), (d, e) =>
             {
                 LayerDrawedBehavior control = d as LayerDrawedBehavior;

                 if (control == null) return;

                 ArrayList config = e.NewValue as ArrayList;

             }));

        protected override void OnAttached()
        {
            AssociatedObject.Drawed += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var items = AssociatedObject.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            var controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = new Storyboard();

            for (int i = 0; i < controls.Count; i++)
            {
                foreach (var item in Timelines.OfType<DoubleAnimation>())
                { 
                    TimeSpan span = TimeSpan.FromMilliseconds(i * (Split + item.Duration.TimeSpan.TotalMilliseconds));

                    TimeSpan end = item.Duration.TimeSpan + span;

                    DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

                    //EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(item.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));
                    //EasingDoubleKeyFrame key2 = new EasingDoubleKeyFrame(item.From.Value, KeyTime.FromTimeSpan(span));
                    //EasingDoubleKeyFrame key3 = new EasingDoubleKeyFrame(item.To.Value, KeyTime.FromTimeSpan(end));

                    //frames.KeyFrames.Add(key1);
                    //frames.KeyFrames.Add(key2);
                    //frames.KeyFrames.Add(key3);

                    EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(item.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));

                    frames.KeyFrames.Add(key1);
                    Storyboard.SetTarget(frames, controls[i]);
                    Storyboard.SetTargetProperty(frames, Storyboard.GetTargetProperty(item));
                    storyboard.Children.Add(frames);

                    DoubleAnimation animation = item.Clone();
                    animation.BeginTime = span;
                    Storyboard.SetTarget(animation, controls[i]);

                    storyboard.Children.Add(animation);
                }
            }

            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();
        }

        public int Split
        {
            get { return (int)GetValue(SplitProperty); }
            set { SetValue(SplitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitProperty =
            DependencyProperty.Register("Split", typeof(int), typeof(LayerDrawedBehavior), new PropertyMetadata(5, (d, e) =>
            {
                LayerDrawedBehavior control = d as LayerDrawedBehavior;

                if (control == null) return;
            }));

    }
}
