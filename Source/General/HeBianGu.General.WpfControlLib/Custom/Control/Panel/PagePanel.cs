using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 分页布局容器 </summary>
    public class PagePanel : AnimationPanel
    {
        public AnimationAction AnimationAction
        {
            get { return (AnimationAction)GetValue(AnimationActionProperty); }
            set { SetValue(AnimationActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationActionProperty =
            DependencyProperty.Register("AnimationAction", typeof(AnimationAction), typeof(PagePanel), new PropertyMetadata(new NoneAction(), (d, e) =>
             {
                 PagePanel control = d as PagePanel;

                 if (control == null) return;

                 AnimationAction config = e.NewValue as AnimationAction;

             }));

        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = this.GetChildren().OfType<FrameworkElement>()?.ToList();

            var elment = children[0];

            var lasts = children.Where(l => l.Visibility == Visibility.Visible && l != elment)?.ToList();

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            //Point to = new Point(finalSize.Width, finalSize.Height);

            elment.RenderTransformOrigin = new Point(0.5, 0.5);

            elment.Measure(finalSize);

            if (Double.IsNaN(elment.Width) && elment.HorizontalAlignment == HorizontalAlignment.Stretch)
            {
                elment.Width = finalSize.Width;
            }

            if (Double.IsNaN(elment.Height) && elment.VerticalAlignment == VerticalAlignment.Stretch)
            {
                elment.Height = finalSize.Height;
            }

            Point from = new Point(-2 * elment.DesiredSize.Width, -2 * elment.DesiredSize.Height);

            Point end = new Point(center.X - elment.DesiredSize.Width / 2, center.Y - elment.DesiredSize.Height / 2);

            elment.Arrange(new Rect(end, elment.DesiredSize));



            if (this.IsAnimation)
            {
                foreach (var item in lasts)
                {
                    this.AnimationAction?.BeginHide(item);

                    Panel.SetZIndex(item, 1);
                }

                this.AnimationAction?.BeginVisible(elment);

                Panel.SetZIndex(elment, 0);
            }

            return base.ArrangeOverride(finalSize);
        }


        //  Do ：用于设置整个容器的大小
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        protected override void RefreshAnimation()
        {

        }
    }

    public interface AnimationAction
    {
        void BeginHide(UIElement element);

        void BeginVisible(UIElement element);
    }

    public abstract class AnimationActionBase : AnimationAction
    {
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

        public Point RenderTransformOrigin { get; set; } = new Point(0.5, 0.5);

        public Double BeginTime { get; set; } = 0.0;

        public virtual void BeginHide(UIElement element)
        {
            element.RenderTransformOrigin = RenderTransformOrigin;
        }

        public virtual void BeginVisible(UIElement element)
        {
            element.RenderTransformOrigin = RenderTransformOrigin;

            if (this.BeginTime == 0.0)
            {
                element.Visibility = Visibility.Visible;
            }
            else
            {
                Storyboard storyboard = new Storyboard();

                ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();

                DiscreteObjectKeyFrame keyFrame1 = new DiscreteObjectKeyFrame(Visibility.Hidden, KeyTime.FromTimeSpan(TimeSpan.Zero));
                DiscreteObjectKeyFrame keyFrame2 = new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(this.BeginTime)));

                frames.KeyFrames.Add(keyFrame1);
                frames.KeyFrames.Add(keyFrame2);

                Storyboard.SetTarget(frames, element);
                Storyboard.SetTargetProperty(frames, new PropertyPath(FrameworkElement.VisibilityProperty));
                storyboard.Children.Add(frames);

                storyboard.Begin();
            }
        }
    }

    public class OpacityAction : AnimationActionBase
    {
        public OpacityAction()
        {
            this.Start = 0;

            this.End = 1;
        }
        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            element.BeginAnimationOpacity(this.End, this.Start, this.HideDuration, l => l.Visibility = Visibility.Hidden);

        }

        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            element.Visibility = Visibility.Visible;

            element.BeginAnimationOpacity(this.Start, this.End, this.VisibleDuration);
        }
    }

    public class NoneAction : AnimationActionBase
    {
        public override void BeginVisible(UIElement element)
        {
            base.BeginVisible(element);
        }

        public override void BeginHide(UIElement element)
        {
            base.BeginHide(element);

            element.Visibility = Visibility.Collapsed;
        }
    }

    public class AngleAction : OpacityAction
    {
        public double StartAngle { get; set; } = 0;

        public double EndAngle { get; set; } = 360 * 4;

        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginVisible(element);

            element.BeginAnimationAngle(this.StartAngle, this.EndAngle, this.VisibleDuration, null, l =>
            {
                l.FillBehavior = FillBehavior.Stop;
            });

            element.BeginAnimationScaleX(0, 1, this.VisibleDuration * 1.2, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
            element.BeginAnimationScaleY(0, 1, this.VisibleDuration * 1.2, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
        }

        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginHide(element);

            element.BeginAnimationAngle(this.EndAngle, this.StartAngle, this.HideDuration, l => l.Visibility = Visibility.Hidden);

            element.BeginAnimationScaleX(1, 0, this.HideDuration, null, l => l.FillBehavior = FillBehavior.Stop);

            element.BeginAnimationScaleY(1, 0, this.HideDuration, null, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class ScaleAction : OpacityAction
    {
        public double StartX { get; set; } = 0.2;

        public double EndX { get; set; } = 0.5;

        public double StartY { get; set; } = 0.2;

        public double EndY { get; set; } = 0.5;

        public double CenterX { get; set; } = 1;

        public double CenterY { get; set; } = 1;

        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginVisible(element);

            if (this.StartX != this.CenterX)
            {
                element.BeginAnimationScaleX(this.StartX, this.CenterX, this.VisibleDuration, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
            }

            if (this.StartY != this.CenterY)
            {
                element.BeginAnimationScaleY(this.StartY, this.CenterY, this.VisibleDuration, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
            }
        }

        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginHide(element);

            if (this.CenterX != this.EndX)
            {
                element.BeginAnimationScaleX(this.CenterX, this.EndX, this.HideDuration, l => l.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);
            }

            if (this.CenterY != this.EndY)
            {
                element.BeginAnimationScaleY(this.CenterY, this.EndY, this.HideDuration, l => l.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);
            }
        }
    }

    public class TranslateAction : OpacityAction
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginVisible(element);

            element.BeginAnimationX(this.StartPoint.X, 0, this.VisibleDuration, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
            element.BeginAnimationY(this.StartPoint.Y, 0, this.VisibleDuration, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));

        }

        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginHide(element);

            element.BeginAnimationX(0, this.EndPoint.X, this.HideDuration, l => l.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);
            element.BeginAnimationY(0, this.EndPoint.Y, this.HideDuration, l => l.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);

        }
    }

    public class HorizontalAction : AnimationActionBase
    {
        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginVisible(element);

            element.BeginAnimationX(-(element as FrameworkElement).ActualWidth, 0, this.VisibleDuration);
        }

        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginHide(element);

            element.BeginAnimationX(0, (element as FrameworkElement).ActualWidth, this.HideDuration, l => l.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class VerticalAction : AnimationActionBase
    {
        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginVisible(element);

            element.BeginAnimationY(-(element as FrameworkElement).ActualHeight, 0, this.VisibleDuration);
        }

        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginHide(element);

            element.BeginAnimationY(0, (element as FrameworkElement).ActualHeight, this.HideDuration, l => l.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class SkewAction : OpacityAction
    {
        public double StartAngleX { get; set; } = 0;

        public double EndAngleX { get; set; } = 360;

        public double StartAngleY { get; set; } = 0;

        public double EndAngleY { get; set; } = 360;

        public override void BeginVisible(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginVisible(element);

            if (this.StartAngleX != this.EndAngleX)
                element.BeginAnimationSkewX(this.StartAngleX, this.EndAngleX, this.VisibleDuration * 1.5, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));

            if (this.StartAngleY != this.EndAngleY)
                element.BeginAnimationSkewY(this.StartAngleY, this.EndAngleY, this.VisibleDuration * 1.5, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
        }

        public override void BeginHide(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginHide(element);

            if (this.StartAngleX != this.EndAngleX)
                element.BeginAnimationSkewX(this.EndAngleX, this.StartAngleX, this.HideDuration, null, l => l.FillBehavior = FillBehavior.Stop);

            if (this.StartAngleY != this.EndAngleY)
                element.BeginAnimationSkewY(this.EndAngleY, this.StartAngleY, this.HideDuration, null, l => l.FillBehavior = FillBehavior.Stop);
        }
    }
}
