using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public class ContainPanel : AnimationPanel
    {
        public IAnimationAction AnimationAction
        {
            get { return (IAnimationAction)GetValue(AnimationActionProperty); }
            set { SetValue(AnimationActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationActionProperty =
            DependencyProperty.Register("AnimationAction", typeof(IAnimationAction), typeof(ContainPanel), new PropertyMetadata(new NoneAction(), (d, e) =>
            {
                ContainPanel control = d as ContainPanel;

                if (control == null) return;

                IAnimationAction config = e.NewValue as IAnimationAction;

            }));

        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = this.GetChildren().OfType<FrameworkElement>()?.ToList();

            if (children == null || children.Count == 0) return finalSize;

            //  Do ：中心点
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);

            foreach (FrameworkElement elment in children)
            {
                if (Double.IsNaN(elment.Width) && elment.HorizontalAlignment == HorizontalAlignment.Stretch)
                {
                    elment.Width = finalSize.Width;
                }

                if (Double.IsNaN(elment.Height) && elment.VerticalAlignment == VerticalAlignment.Stretch)
                {
                    elment.Height = finalSize.Height;
                }
                elment.Measure(finalSize);
                elment.Arrange(new Rect(finalSize));

                //elment.InvalidateVisual();
                //elment.InvalidateMeasure();
                //elment.InvalidateArrange();
            }

            return finalSize;
        }


        //  Do ：用于设置整个容器的大小
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        protected override void RefreshAnimation()
        {

        }

        public void Add(UIElement element)
        {
            this.Children.Add(element);

            this.AnimationAction?.BeginCurrent(element);

            if(this.Children.Count==1)
            {
                this.Visibility = Visibility.Visible;

                DoubleStoryboardEngine.Create(0, 1, 0.3, "Opacity").Start(this);
            }
        }

        public void Remove()
        {
            if (this.Children == null) return;

            if (this.Children.Count == 0) return;

            var element = this.Children[this.Children.Count - 1]; 

            Action compate = () =>
              {
                  this.Children.Remove(element);

                  if (this.Children.Count == 0)
                  {
                      DoubleStoryboardEngine.Create(1, 0, 0.3, "Opacity").Start(this, l => l.Visibility = Visibility.Collapsed);
                  }
              };

            this.AnimationAction?.BeginPrevious(element, compate);
        }
    }
}
