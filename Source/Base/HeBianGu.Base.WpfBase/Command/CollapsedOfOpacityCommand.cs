using System;
using System.Windows;
using System.Windows.Input;
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
                marginAnimation.To = new Thickness(-element.ActualHeight, 0, 0, 0);
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
                marginAnimation.From = new Thickness(-element.ActualHeight, 0, 0, 0);
                marginAnimation.To = new Thickness(0, 0, 0, 0);
                marginAnimation.Duration = TimeSpan.FromSeconds(0.5);
                element.BeginAnimation(FrameworkElement.MarginProperty, marginAnimation);

            }
        }

        public event EventHandler CanExecuteChanged;
    }
}