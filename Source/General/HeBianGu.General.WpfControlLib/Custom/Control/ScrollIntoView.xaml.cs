using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 用于绑定数据的导航效果 </summary>
    [TemplatePart(Name = "PART_NAVIGATION", Type = typeof(ListBox))]
    [TemplatePart(Name = "PART_CONTAINT", Type = typeof(ListBox))]
    public partial class ScrollIntoView : ContentControl
    {
        ListBox _navigation;
        ListBox _contain;

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
        ListBox _navigation;

        ScrollViewer _scrollviewer;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._navigation = Template.FindName("PART_NAVIGATION", this) as ListBox;

            this._scrollviewer = Template.FindName("PART_SCROLLVIEWER", this) as ScrollViewer;

            this._navigation.SelectionChanged += (l, k) =>
            {
                int index = Math.Min(this._navigation.SelectedIndex, this.Items.Count - 1);

                this.ScrollIntoView(this.Items[index]);
            };

            //  Do ：设置滚动到指定位置后导航跟着改变
            this._scrollviewer.ScrollChanged += (l, k) =>
              {
                  var point = this._scrollviewer.TranslatePoint(new Point(10, 10), this);

                  PointHitTestParameters parameters = new PointHitTestParameters(point);

                  VisualTreeHelper.HitTest(this, HitTestFilter, HitTestCallBack, parameters);
              };
        }

        HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            if (result.VisualHit is ListBoxItem)
            {
                return HitTestResultBehavior.Stop;
            }

            return HitTestResultBehavior.Continue;

        }

        HitTestFilterBehavior HitTestFilter(DependencyObject obj)
        {
            Type type = obj.GetType();

            if (type.Name == this.GetType().Name) return HitTestFilterBehavior.ContinueSkipSelf;

            if (obj is ListBoxItem item)
            {
                var index = this.ItemContainerGenerator.IndexFromContainer(item);

                this._navigation.SelectedIndex = index;
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
    }
}
