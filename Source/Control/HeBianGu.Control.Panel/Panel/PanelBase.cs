// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HeBianGu.Control.Panel
{
    public abstract class PanelBase : System.Windows.Controls.Panel
    {
        public PanelBase()
        {
            //  Do ：下一项
            {
                CommandBinding binding = new CommandBinding(Commander.Next);

                this.CommandBindings.Add(binding);

                binding.Executed += (l, k) => this.Next();
            }

            //  Do ：上一项
            {
                CommandBinding binding = new CommandBinding(Commander.Prev);

                this.CommandBindings.Add(binding);

                binding.Executed += (l, k) => this.Previous();
            }

            this.SizeChanged += (l, k) =>
              {
                  this.InvalidateArrange();

              };

        }

        protected virtual void Next()
        {
            this.StartIndex = this.StartIndex + 1;

            this.StartIndex = this.StartIndex > this.Children.Count - 1 ? 0 : this.StartIndex;

            //System.Diagnostics.Debug.WriteLine(this.StartIndex);

            //  Do ：刷新
            this.InvalidateArrange();
        }

        protected virtual void Previous()
        {
            this.StartIndex = this.StartIndex - 1;

            this.StartIndex = this.StartIndex < 0 ? this.Children.Count - 1 : this.StartIndex;

            //  Do ：刷新
            this.InvalidateArrange();
        }

        public int StartIndex
        {
            get { return (int)GetValue(StartIndexProperty); }
            set { SetValue(StartIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartIndexProperty =
            DependencyProperty.Register("StartIndex", typeof(int), typeof(PanelBase), new PropertyMetadata(0, (d, e) =>
            {
                PanelBase control = d as PanelBase;

                if (control == null) return;

                //int config = e.NewValue as int;

                //  Do ：刷新
                control.InvalidateArrange();

            }));


        public int DisplayCount
        {
            get { return (int)GetValue(DisplayCountProperty); }
            set { SetValue(DisplayCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayCountProperty =
            DependencyProperty.Register("DisplayCount", typeof(int), typeof(PanelBase), new PropertyMetadata(-1, (d, e) =>
             {
                 PanelBase control = d as PanelBase;

                 if (control == null) return;

                 //int config = e.NewValue as int;

                 control.InvalidateArrange();

             }));


        protected List<UIElement> GetChildren()
        {
            int count = this.DisplayCount < 0 || this.DisplayCount > this.Children.Count ? this.Children.Count : this.DisplayCount;

            //  Do ：重新排序
            UIElement[] orders = new UIElement[this.Children.Count];

            for (int i = 0; i < this.Children.Count; i++)
            {
                int index = this.StartIndex + i;

                index = index > this.Children.Count - 1 ? index - this.Children.Count : index;

                orders[i] = this.Children[index];

                orders[i].Arrange(new Rect(new Point(0, 0), orders[i].DesiredSize));

                //orders[i].Arrange(new Rect(0, 0, 0, 0));

                //orders[i].Opacity = 0.0;
            }

            List<UIElement> result = orders?.ToList()?.Take(count)?.ToList();


            //result.ForEach(l => l.Opacity = 1);

            return result;
        }

    }

    public abstract class AnimationPanel : PanelBase
    {

        public AnimationMode AnimationMode
        {
            get { return (AnimationMode)GetValue(AnimationModeProperty); }
            set { SetValue(AnimationModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationModeProperty =
            DependencyProperty.Register("AnimationMode", typeof(AnimationMode), typeof(AnimationPanel), new PropertyMetadata(default(AnimationMode), (d, e) =>
             {
                 AnimationPanel control = d as AnimationPanel;

                 if (control == null) return;

                 //AnimationMode config = e.NewValue as AnimationMode;

             }));


        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(double), typeof(AnimationPanel), new PropertyMetadata(500.0, (d, e) =>
            {
                AnimationPanel control = d as AnimationPanel;

                if (control == null) return;

                //double config = e.NewValue as double;

            }));


        public bool IsAnimation
        {
            get { return (bool)GetValue(IsAnimationProperty); }
            set { SetValue(IsAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAnimationProperty =
            DependencyProperty.Register("IsAnimation", typeof(bool), typeof(AnimationPanel), new PropertyMetadata(true, (d, e) =>
             {
                 AnimationPanel control = d as AnimationPanel;

                 if (control == null) 
                     return;

                 //bool config = e.NewValue as bool;
                 control.InvalidateArrange();

             }));
        public ObservableCollection<Timeline> Timelines { get; } = new ObservableCollection<Timeline>();

        //public ArrayList Timelines
        //{
        //    get { return (ArrayList)GetValue(TimelinesProperty); }
        //    set { SetValue(TimelinesProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TimelinesProperty =
        //    DependencyProperty.Register("Timelines", typeof(ArrayList), typeof(AnimationPanel), new PropertyMetadata(new ArrayList(), (d, e) =>
        //    {
        //        AnimationPanel control = d as AnimationPanel;

        //        if (control == null) return;

        //        ArrayList config = e.NewValue as ArrayList;

        //    }));

        public double SplitMilliSecond
        {
            get { return (double)GetValue(SplitMilliSecondProperty); }
            set { SetValue(SplitMilliSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitMilliSecondProperty =
            DependencyProperty.Register("SplitMilliSecond", typeof(double), typeof(AnimationPanel), new PropertyMetadata(5.0, (d, e) =>
            {
                AnimationPanel control = d as AnimationPanel;

                if (control == null) return;
            }));


        protected override Size ArrangeOverride(Size finalSize)
        {
            //  Do ：触发动画
            if (this.IsAnimation)
            {
                this.RefreshAnimation();
            }

            return base.ArrangeOverride(finalSize);
        }

        protected virtual void RefreshAnimation()
        {

        }
    }


    public enum AnimationMode
    {
        Postion = 0, FromCenter
    }
}
