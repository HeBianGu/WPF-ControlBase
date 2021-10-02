using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.Application.FrameworkTest
{
    /// <summary>
    /// 在ContentPresenter上覆盖一层，或者是在原来的可视化树上增加一个绘制
    /// </summary>
    class ExtendContentPresenter : ContentPresenter
    {
        Button button = new Button() { Content = "上层" };

        public ExtendContentPresenter()
        {
            //_children = new UIElementCollection(this, this);

            //_children.Add(new Button() { Content="上层"});

            this.AddLogicalChild(button);

        }
        protected override Size ArrangeOverride(Size finalSize)
        { 
            {
                UIElement child = (VisualTreeHelper.GetChildrenCount(this) > 0) ? VisualTreeHelper.GetChild(this, 0) as UIElement : null;

                if (child != null)
                {
                    child.Arrange(new Rect(finalSize));
                }

            }


            {
                UIElement child = (VisualTreeHelper.GetChildrenCount(this) > 0) ? VisualTreeHelper.GetChild(this, 1) as UIElement : null;

                if (child != null)
                {
                    child.Arrange(new Rect(finalSize));
                }

            }

            return finalSize; 
        }

        protected override int VisualChildrenCount => base.VisualChildrenCount + 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index == this.VisualChildrenCount - 1) return this.button;

            return base.GetVisualChild(index);
        }

    }
}
