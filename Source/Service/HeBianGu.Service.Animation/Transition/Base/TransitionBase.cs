// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using HeBianGu.Base.WpfBase;
namespace HeBianGu.Service.Animation
{
    public abstract class TransitionBase : ITransition
    {
        public TransitionBase()
        {
            this.RenderTransformOrigin = new Point(0.5, 0.5);
        }
        public double Start { get; set; }

        public double End { get; set; }

        public Double Duration
        {
            set
            {
                this.HideDuration = value;
                this.VisibleDuration = value;
            }
        }

        public Double HideDuration { get; set; } = 500.0;

        public Double VisibleDuration { get; set; } = 500.0;

        public Point RenderTransformOrigin { get; set; }

        public Double BeginTime { get; set; } = 0.0;

        public Visibility HiddenOrCollapsed { get; set; } = Visibility.Hidden;

        public FillBehavior FillBehavior { get; set; } = FillBehavior.Stop;

        //  Do ：切页反转
        public bool Reverse { get; set; }

        public EasingFunctionBase HideEasing { get; set; }
        public EasingFunctionBase VisibleEasing { get; set; }


        protected bool CanAnimation(UIElement element)
        {
            if (element is FrameworkElement framework)
            {
                return element.IsVisible && framework.IsLoaded;
            }
            return element.IsVisible;
        }

        public virtual void BeginPrevious(UIElement element, Action complate = null)
        {
            Panel.SetZIndex(element, Reverse ? 0 : 1);
            element.RenderTransformOrigin = RenderTransformOrigin;
            complate?.Invoke();
        }

        public virtual void BeginCurrent(UIElement element, Action complate = null)
        {
            element.RenderTransformOrigin = RenderTransformOrigin;
            Panel.SetZIndex(element, Reverse ? 1 : 0);

            if (this.BeginTime == 0.0)
            {
                element.Visibility = Visibility.Visible;
            }
            else
            {
                if (!this.CanAnimation(element))
                {
                    complate?.Invoke();
                    return;
                }
                Storyboard storyboard = StoryboardFactory.Create();
                ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();
                DiscreteObjectKeyFrame keyFrame1 = new DiscreteObjectKeyFrame(Visibility.Hidden, KeyTime.FromTimeSpan(TimeSpan.Zero));
                DiscreteObjectKeyFrame keyFrame2 = new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(this.BeginTime)));

                frames.KeyFrames.Add(keyFrame1);
                frames.KeyFrames.Add(keyFrame2);

                Storyboard.SetTarget(frames, element);
                Storyboard.SetTargetProperty(frames, new PropertyPath(FrameworkElement.VisibilityProperty));
                storyboard.Children.Add(frames);
                //  ToEdit ：20210118
                storyboard.Completed += (l, k) =>
              {
                  complate?.Invoke();
              };

                storyboard.Begin();
            }
        }

        public virtual void BeginHidden(UIElement element, Action complate = null)
        {
            this.BeginPrevious(element, complate);
        }

        public virtual void BeginVisible(UIElement element, Action complate = null)
        {
            this.BeginCurrent(element, complate);
        }
    }
}
