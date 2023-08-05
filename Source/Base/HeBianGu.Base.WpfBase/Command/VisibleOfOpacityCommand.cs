// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public class VisibleOfOpacityCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            if (parameter is UIElement element)
            {
                element.Visibility = Visibility.Visible;

                //var engine = DoubleStoryboardEngine.Create(0, 1, 0.4, UIElement.OpacityProperty.Name);

                //engine.Start(element);


                DoubleAnimation animation = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.4)));

                element.BeginAnimation(UIElement.OpacityProperty, animation);
            }
        }
    }
}