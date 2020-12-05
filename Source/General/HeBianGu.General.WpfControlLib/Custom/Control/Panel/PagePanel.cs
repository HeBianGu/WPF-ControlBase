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
        public IAnimationAction AnimationAction
        {
            get { return (IAnimationAction)GetValue(AnimationActionProperty); }
            set { SetValue(AnimationActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationActionProperty =
            DependencyProperty.Register("AnimationAction", typeof(IAnimationAction), typeof(PagePanel), new PropertyMetadata(new NoneAction(), (d, e) =>
             {
                 PagePanel control = d as PagePanel;

                 if (control == null) return;

                 IAnimationAction config = e.NewValue as IAnimationAction;

             }));


        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = this.GetChildren().OfType<FrameworkElement>()?.ToList();

            if (children == null || children.Count == 0) return finalSize;

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
