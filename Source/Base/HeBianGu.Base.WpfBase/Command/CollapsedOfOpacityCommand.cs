using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public class CollapsedOfOpacityCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is UIElement element)
            {
                var engine = DoubleStoryboardEngine.Create(1, 0, 0.2, UIElement.OpacityProperty.Name);

                engine.CompletedEvent += (l, k) =>
                {
                    element.Visibility = Visibility.Collapsed;
                };


                engine.Start(element);
            }
        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary> 向左侧隐藏区域 </summary>
    public class CollapsedOfMarginLeftCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                ThicknessAnimation marginAnimation = new ThicknessAnimation();
                marginAnimation.From = new Thickness(0, 0, 0, 0);
                marginAnimation.To = new Thickness(-element.ActualWidth, 0, 0, 0);
                marginAnimation.Duration = TimeSpan.FromSeconds(0.5);
                //marginAnimation.Completed += (l, k) =>
                //  {
                //      element.Visibility = Visibility.Hidden;
                //  };
                element.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);

            }
        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary> 向右侧显示区域 </summary>

    public class VisibleOfMarginLeftCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                //element.Visibility = Visibility.Visible;
                ThicknessAnimation marginAnimation = new ThicknessAnimation();
                marginAnimation.From = new Thickness(-element.ActualWidth, 0, 0, 0);
                marginAnimation.To = new Thickness(0, 0, 0, 0);
                marginAnimation.Duration = TimeSpan.FromSeconds(0.5);
                element.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);

            }
        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary> 向左侧隐藏区域 </summary>
    public class CollapsedOfMarginBottomCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                ThicknessAnimation marginAnimation = new ThicknessAnimation();
                marginAnimation.From = new Thickness(0, 0, 0, 0);
                marginAnimation.To = new Thickness(0, 0, 0, -element.ActualHeight);
                marginAnimation.Duration = TimeSpan.FromSeconds(0.5);
                //marginAnimation.Completed += (l, k) =>
                //  {
                //      element.Visibility = Visibility.Hidden;
                //  };
                element.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);

            }
        }

        public event EventHandler CanExecuteChanged;
    }

    /// <summary> 向右侧显示区域 </summary>

    public class VisibleOfMarginBottomCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                element.Visibility = Visibility.Visible;
                ThicknessAnimation marginAnimation = new ThicknessAnimation();
                marginAnimation.From = new Thickness(0, 0, 0, -element.ActualHeight);
                marginAnimation.To = new Thickness(0, 0, 0, 0);
                marginAnimation.Duration = TimeSpan.FromSeconds(0.5);
                element.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);

            }
        }

        public event EventHandler CanExecuteChanged;
    }


    /// <summary> 向右侧显示区域 </summary>

    public class VisibleSplitAnimationCommand : ICommand
    {
        public int SplitMilliSecond { get; set; } = 30;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is FrameworkElement element)) return;

            var items = element.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            var controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = new Storyboard();

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

        public int SplitMilliSecond { get; set; } = 30;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is FrameworkElement element)) return;

            var items = element.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            var controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = new Storyboard();

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

