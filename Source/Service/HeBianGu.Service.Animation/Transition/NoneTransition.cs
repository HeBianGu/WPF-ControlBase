// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Service.Animation
{
    public class NoneTransition : TransitionBase
    {
        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element, complate);
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element, complate);

            element.Visibility = Visibility.Collapsed;
        }
    }
}
