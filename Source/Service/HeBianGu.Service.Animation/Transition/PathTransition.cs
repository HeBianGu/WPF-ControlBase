// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Service.Animation
{
    public class PathTransition : OpacityTransition
    {
        public PathGeometry PathGeometry { get; set; }

        public PathGeometry VisblePathGeometry { get; set; }

        public PathGeometry HidePathGeometry { get; set; }

        public bool UseAngle { get; set; }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            if (this.VisblePathGeometry == null)
                this.VisblePathGeometry = this.PathGeometry;

            element.BeginAnimationPath(this.VisblePathGeometry, this.HideDuration, this.UseAngle, true);
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            if (this.HidePathGeometry == null)
                this.HidePathGeometry = this.PathGeometry;

            element.BeginAnimationPath(this.HidePathGeometry, this.VisibleDuration, this.UseAngle);

        }
    }
}
