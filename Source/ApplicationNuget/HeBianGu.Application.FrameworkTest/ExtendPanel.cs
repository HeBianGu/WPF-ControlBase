using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Application.FrameworkTest
{
    class ExtendPanel: Panel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var item in this.Children.Cast<UIElement>())
            {
                item.Measure(finalSize);

                item.Arrange(new Rect(0, 0, item.DesiredSize.Width, item.DesiredSize.Height));
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}
