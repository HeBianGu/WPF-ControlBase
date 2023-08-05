// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.ScrollInto
{
    /// <summary> 用于绑定数据的导航效果 </summary>
    [TemplatePart(Name = "PART_NAVIGATION", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_CONTAINT", Type = typeof(ListBox))]
    public partial class ScrollIntoView : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollIntoView), "S.ScrollIntoView.Default");
        public static ComponentResourceKey ItemKey => new ComponentResourceKey(typeof(ScrollIntoView), "S.ScrollIntoItems.Default");

        private ListBox _navigation;
        private ListBox _contain;

        static ScrollIntoView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollIntoView), new FrameworkPropertyMetadata(typeof(ScrollIntoView)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._navigation = Template.FindName("PART_NAVIGATION", this) as ListBox;


            this._contain = Template.FindName("PART_CONTAINT", this) as ListBox;

            this._navigation.SelectionChanged += (l, k) =>
              {
                  this._contain.ScrollIntoView(this._navigation.SelectedItem);
              };
        }

        public DataTemplate NavigationDataTemplate
        {
            get { return (DataTemplate)GetValue(NavigationDataTemplateProperty); }
            set { SetValue(NavigationDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationDataTemplateProperty =
            DependencyProperty.Register("NavigationDataTemplate", typeof(DataTemplate), typeof(ScrollIntoView), new PropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 ScrollIntoView control = d as ScrollIntoView;

                 if (control == null) return;

                 DataTemplate config = e.NewValue as DataTemplate;

             }));


        public DataTemplate ContainDataTemplate
        {
            get { return (DataTemplate)GetValue(ContainDataTemplateProperty); }
            set { SetValue(ContainDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContainDataTemplateProperty =
            DependencyProperty.Register("ContainDataTemplate", typeof(DataTemplate), typeof(ScrollIntoView), new PropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 ScrollIntoView control = d as ScrollIntoView;

                 if (control == null) return;

                 DataTemplate config = e.NewValue as DataTemplate;

             }));


        public IEnumerable Source
        {
            get { return (IEnumerable)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(IEnumerable), typeof(ScrollIntoView), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 ScrollIntoView control = d as ScrollIntoView;

                 if (control == null) return;

                 IEnumerable config = e.NewValue as IEnumerable;

             }));
    }

    /// <summary> 用于根据索引导航到指定Item效果 </summary>
    [TemplatePart(Name = "PART_NAVIGATION", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_SCROLLVIEWER", Type = typeof(ScrollViewer))]
    public partial class ScrollIntoItems : ListBox
    {
        static ScrollIntoItems()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollIntoItems), new FrameworkPropertyMetadata(typeof(ScrollIntoItems)));
        }

        private ListBox _navigation;
        private ScrollViewer _scrollviewer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._navigation = Template.FindName("PART_NAVIGATION", this) as ListBox;

            this._scrollviewer = Template.FindName("PART_SCROLLVIEWER", this) as ScrollViewer;

            this._navigation.SelectionChanged += (l, k) =>
            {
                int index = Math.Min(this._navigation.SelectedIndex, this.Items.Count - 1);

                if (index < 0) return;

                //this.ScrollIntoView(this.Items[index]);
                var element = this.Items[index] as UIElement;
                var find = this.ItemContainerGenerator.ContainerFromIndex(index) as UIElement;
                this.ScrollTo(find);

            };

            //  Do ：设置滚动到指定位置后导航跟着改变
            this._scrollviewer.ScrollChanged += (l, k) =>
              {
                  //if (k.VerticalChange > 0)
                  //{
                  //    Point n = new Point() { X = this.HitTestPoint.X, Y = this._scrollviewer.ActualHeight - this.HitTestPoint.Y };

                  //    Point point = this._scrollviewer.TranslatePoint(n, this);

                  //    PointHitTestParameters parameters = new PointHitTestParameters(point);

                  //    VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
                  //}
                  //else
                  //{
                  //    Point point = this._scrollviewer.TranslatePoint(this.HitTestPoint, this);

                  //    PointHitTestParameters parameters = new PointHitTestParameters(point);

                  //    VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
                  //}

                  Point point = this._scrollviewer.TranslatePoint(this.HitTestPoint, this);

                  PointHitTestParameters parameters = new PointHitTestParameters(point);

                  VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
              };
        }

        public void ScrollTo(UIElement element)
        {
            ScrollViewer scrollViewer = element.GetParent<ScrollViewer>();

            if (scrollViewer == null) return;

            double currentScrollPosition = scrollViewer.VerticalOffset;
            Point point = new Point(0, currentScrollPosition);

            Point targetPosition = element.TransformToVisual(scrollViewer).Transform(point);
            scrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }

        public Point HitTestPoint
        {
            get { return (Point)GetValue(HitTestPointProperty); }
            set { SetValue(HitTestPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HitTestPointProperty =
            DependencyProperty.Register("HitTestPoint", typeof(Point), typeof(ScrollIntoItems), new FrameworkPropertyMetadata(new Point(10, 10), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ScrollIntoItems control = d as ScrollIntoItems;

                 if (control == null) return;

                 if (e.OldValue is Point o)
                 {

                 }

                 if (e.NewValue is Point n)
                 {

                 }

             }));

        private HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            if (result.VisualHit is ListBoxItem)
            {
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;

        }

        private HitTestFilterBehavior HitTestFilter(DependencyObject obj)
        {
            Type type = obj.GetType();

            if (type.Name == this.GetType().Name) return HitTestFilterBehavior.ContinueSkipSelf;

            if (obj is ListBoxItem item)
            {
                int index = this.ItemContainerGenerator.IndexFromContainer(item);

                if (index >= 0)
                {
                    if (this._navigation.SelectedIndex != index)
                        this._navigation.SelectedIndex = index;
                }

            }

            return HitTestFilterBehavior.Continue;
        }

        public DataTemplate NavigationDataTemplate
        {
            get { return (DataTemplate)GetValue(NavigationDataTemplateProperty); }
            set { SetValue(NavigationDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationDataTemplateProperty =
            DependencyProperty.Register("NavigationDataTemplate", typeof(DataTemplate), typeof(ScrollIntoItems), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                ScrollIntoItems control = d as ScrollIntoItems;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public DataTemplate ContainDataTemplate
        {
            get { return (DataTemplate)GetValue(ContainDataTemplateProperty); }
            set { SetValue(ContainDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContainDataTemplateProperty =
            DependencyProperty.Register("ContainDataTemplate", typeof(DataTemplate), typeof(ScrollIntoItems), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                ScrollIntoItems control = d as ScrollIntoItems;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public IEnumerable NavigationSource
        {
            get { return (IEnumerable)GetValue(NavigationSourceProperty); }
            set { SetValue(NavigationSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationSourceProperty =
            DependencyProperty.Register("NavigationSource", typeof(IEnumerable), typeof(ScrollIntoItems), new PropertyMetadata(default(IEnumerable), (d, e) =>
             {
                 ScrollIntoItems control = d as ScrollIntoItems;

                 if (control == null) return;


                 IEnumerable config = e.NewValue as IEnumerable;

             }));


        public Style NavigationStyle
        {
            get { return (Style)GetValue(NavigationStyleProperty); }
            set { SetValue(NavigationStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationStyleProperty =
            DependencyProperty.Register("NavigationStyle", typeof(Style), typeof(ScrollIntoItems), new FrameworkPropertyMetadata(default(Style), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 ScrollIntoItems control = d as ScrollIntoItems;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));

    }
}
