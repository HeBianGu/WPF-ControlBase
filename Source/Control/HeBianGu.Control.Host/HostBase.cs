// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Host
{
    public abstract class HostBase : ContentControl
    {
        private List<FrameworkElement> _hosts = new List<FrameworkElement>();

        protected ContentControl Container { get; } = new ContentControl();

        protected override Visual GetVisualChild(int index) => base.VisualChildrenCount <= index ? _hosts[index - base.VisualChildrenCount] : base.GetVisualChild(index);

        protected override int VisualChildrenCount => base.VisualChildrenCount + this._hosts.Count;

        protected void AddHost(FrameworkElement visual)
        {
            _hosts.Add(visual);
            base.AddVisualChild(visual);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Container.Style = this.ContainerStyle;
            this.AddHost(this.Container);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (FrameworkElement item in this._hosts)
            {
                item.Arrange(new Rect(new Point(0, 0), finalSize));
            }
            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (FrameworkElement item in this._hosts)
            {
                item.Measure(availableSize);
            }
            return base.MeasureOverride(availableSize);
        }

        public Style ContainerStyle
        {
            get { return (Style)GetValue(ContainerStyleProperty); }
            set { SetValue(ContainerStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContainerStyleProperty =
            DependencyProperty.Register("ContainerStyle", typeof(Style), typeof(HostBase), new FrameworkPropertyMetadata(default(Style), (d, e) =>
            {
                HostBase control = d as HostBase;

                if (control == null) return;

                if (e.OldValue is Style o)
                {

                }

                if (e.NewValue is Style n)
                {

                }

            }));



        public object HostData
        {
            get { return GetValue(HostDataProperty); }
            set { SetValue(HostDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HostDataProperty =
            DependencyProperty.Register("HostData", typeof(object), typeof(HostBase), new FrameworkPropertyMetadata(default(object), (d, e) =>
            {
                HostBase control = d as HostBase;

                if (control == null) return;

                if (e.OldValue is object o)
                {

                }

                if (e.NewValue is object n)
                {

                }

            }));


        protected Rect GetElementRect(UIElement element)
        {
            if (element == null) return new Rect();
            Point point = element.TranslatePoint(new Point(0, 0), this);
            Rect rect = new Rect(point, element.RenderSize);

            return rect;
        }

        public virtual void Location(Point point)
        {
            this.Container.Visibility = Visibility.Visible;
            double w = this.Container.DesiredSize.Width;
            double h = this.Container.DesiredSize.Height;
            double x = point.X - w / 2.0;
            double y = point.Y - h / 2.0;
            this.Container.Arrange(new Rect(x, y, w, h));
        }
    }
}
