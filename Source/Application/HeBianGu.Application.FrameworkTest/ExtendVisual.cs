using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.Application.FrameworkTest
{
    [ContentProperty("Items")]
    class ExtendVisual: Visual
    {
        public List<UIElement> Items
        {
            get { return (List<UIElement>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<UIElement>), typeof(ExtendVisual), new PropertyMetadata(new List<UIElement>(), (d, e) =>
            {
                ArrangeElement control = d as ArrangeElement;

                if (control == null) return;

                List<UIElement> config = e.NewValue as List<UIElement>;


            }));
    }

    class MyClass:ContentControl
    {
        public MyClass()
        {
           
        }
    }
}
