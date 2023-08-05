// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public static class AnimationCommander
    {
        public static CollapsedSplitAnimationCommand CollapsedSplitAnimationCommand { get; } = new CollapsedSplitAnimationCommand();

        public static VisibleSplitAnimationCommand VisibleSplitAnimationCommand { get; } = new VisibleSplitAnimationCommand();
    }

    /// <summary> 向右侧显示区域 </summary>

    public class VisibleSplitAnimationCommand : ICommand
    {
        public int SplitMilliSecond { get; set; } = 10;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is FrameworkElement element)) return;

            System.Collections.Generic.IEnumerable<UIElement> items = element.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            items = items.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except" && !InvokeRandomSplitAnimationAction.GetIsExcept(l));

            System.Collections.Generic.List<UIElement> controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = StoryboardFactory.Create();

            for (int i = 0; i < controls.Count; i++)
            {
                TimeSpan span = TimeSpan.FromMilliseconds(i * SplitMilliSecond);

                ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();

                DiscreteObjectKeyFrame key1 = new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromTimeSpan(TimeSpan.Zero));
                frames.KeyFrames.Add(key1);

                DiscreteObjectKeyFrame key2 = new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(span));
                frames.KeyFrames.Add(key2);

                Storyboard.SetTarget(frames, controls[i]);
                Storyboard.SetTargetProperty(frames, new PropertyPath(FrameworkElement.VisibilityProperty));
                storyboard.Children.Add(frames);
            }

            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();

        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary> 向右侧显示区域 </summary>

    public class CollapsedSplitAnimationCommand : ICommand
    {

        public int SplitMilliSecond { get; set; } = 10;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is FrameworkElement element)) return;

            System.Collections.Generic.IEnumerable<UIElement> items = element.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            items = items.Where(l => (l as FrameworkElement)?.Tag?.ToString() != "Except" && !InvokeRandomSplitAnimationAction.GetIsExcept(l));

            System.Collections.Generic.List<UIElement> controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = StoryboardFactory.Create();

            controls.Reverse();

            for (int i = 0; i < controls.Count; i++)
            {
                TimeSpan span = TimeSpan.FromMilliseconds(i * SplitMilliSecond);

                ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();

                DiscreteObjectKeyFrame key1 = new DiscreteObjectKeyFrame(Visibility.Collapsed, KeyTime.FromTimeSpan(span));
                frames.KeyFrames.Add(key1);

                Storyboard.SetTarget(frames, controls[i]);
                Storyboard.SetTargetProperty(frames, new PropertyPath(FrameworkElement.VisibilityProperty));
                storyboard.Children.Add(frames);
            }

            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();

        }

        public event EventHandler CanExecuteChanged;
    }
}
