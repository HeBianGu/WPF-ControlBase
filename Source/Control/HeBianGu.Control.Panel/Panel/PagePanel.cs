// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Service.Animation;
using System;
using System.Linq;
using System.Windows;

namespace HeBianGu.Control.Panel
{
    /// <summary> 分页布局容器 </summary>
    public class PagePanel : AnimationPanel
    {
        public ITransition AnimationAction
        {
            get { return (ITransition)GetValue(AnimationActionProperty); }
            set { SetValue(AnimationActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationActionProperty =
            DependencyProperty.Register("AnimationAction", typeof(ITransition), typeof(PagePanel), new PropertyMetadata(new NoneTransition(), (d, e) =>
             {
                 PagePanel control = d as PagePanel;

                 if (control == null) return;

                 ITransition config = e.NewValue as ITransition;

             }));


        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<FrameworkElement> children = this.GetChildren().OfType<FrameworkElement>()?.ToList();

            if (children == null || children.Count == 0) return finalSize;

            FrameworkElement elment = children[0];

            System.Collections.Generic.List<FrameworkElement> lasts = children.Where(l => l.Visibility == Visibility.Visible && l != elment)?.ToList();

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            //Point to = new Point(finalSize.Width, finalSize.Height);

            elment.RenderTransformOrigin = new Point(0.5, 0.5);

            elment.Measure(finalSize);

            //if (Double.IsNaN(elment.Width) && elment.HorizontalAlignment == HorizontalAlignment.Stretch)
            //{
            //    elment.Width = finalSize.Width;
            //}

            //if (Double.IsNaN(elment.Height) && elment.VerticalAlignment == VerticalAlignment.Stretch)
            //{
            //    elment.Height = finalSize.Height;
            //}

            if (elment.HorizontalAlignment == HorizontalAlignment.Stretch)
            {
                elment.Width = finalSize.Width;
            }

            if ( elment.VerticalAlignment == VerticalAlignment.Stretch)
            {
                elment.Height = finalSize.Height;
            }

            Point from = new Point(-2 * elment.DesiredSize.Width, -2 * elment.DesiredSize.Height);

            Point end = new Point(center.X - elment.DesiredSize.Width / 2, center.Y - elment.DesiredSize.Height / 2);

            elment.Arrange(new Rect(end, elment.DesiredSize));

            if (this.IsAnimation)
            {
                foreach (FrameworkElement item in lasts)
                {
                    this.AnimationAction?.BeginPrevious(item);
                }

                this.AnimationAction?.BeginCurrent(elment);
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

}
