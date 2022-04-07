// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace HeBianGu.Control.Guide
{
    public class GuideAdornerBehavior : Behavior<FrameworkElement>
    {
        public IEnumerable<GuideAdorner> ToolTips
        {
            get { return (List<GuideAdorner>)GetValue(ToolTipsProperty); }
            set { SetValue(ToolTipsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolTipsProperty =
            DependencyProperty.Register("ToolTips", typeof(List<GuideAdorner>), typeof(GuideAdornerBehavior), new PropertyMetadata(default(List<GuideAdorner>), (d, e) =>
            {
                GuideAdornerBehavior control = d as GuideAdornerBehavior;

                if (control == null) return;

                List<GuideAdorner> config = e.NewValue as List<GuideAdorner>;

            }));

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);

            if (layer == null) return;

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

    }

    public class ToolTipAdorners : List<GuideAdorner>
    {

    }
}
