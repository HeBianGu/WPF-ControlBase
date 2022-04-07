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
    class ArrangeFrameworkElement : FrameworkElement
    {
        public List<UIElement> Items
        {
            get { return (List<UIElement>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<UIElement>), typeof(ArrangeFrameworkElement), new PropertyMetadata(new List<UIElement>(), (d, e) =>
            {
                ArrangeFrameworkElement control = d as ArrangeFrameworkElement;

                if (control == null) return;

                List<UIElement> config = e.NewValue as List<UIElement>;

            }));

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (var item in this.Items)
            {
                item.Measure(finalSize);

                item.Arrange(new Rect(0,0,item.DesiredSize.Width,item.DesiredSize.Height));
            }

            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.Items[index];
        }

        protected override int VisualChildrenCount => this.Items.Count;
    }

    [ContentProperty("Items")]
    class ObjectFrameworkElement : FrameworkElement
    {
        public ObjectFrameworkElement()
        {
            this.ItemSource.CollectionChanged += (l, k) =>
            {
                GenerateItems();
            };

        }
        public ObservableCollection<object> ItemSource
        {
            get { return (ObservableCollection<object>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(ObservableCollection<object>), typeof(ObjectFrameworkElement), new PropertyMetadata(new ObservableCollection<object>(), (d, e) =>
            {
                ObjectFrameworkElement control = d as ObjectFrameworkElement;

                if (control == null) return;

                ObservableCollection<object> config = e.NewValue as ObservableCollection<object>;

            }));


        UIElementCollection _items;
        /// <summary> 只有这样定义UIElementCollection，添加的子项才可以操作 </summary>
        public UIElementCollection Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new UIElementCollection(this, this);
                }

                return _items;
            }
        }


        void GenerateItems()
        {
            this.Items.Clear();

            foreach (var item in this.ItemSource)
            {
                ContentControl control = new ContentControl() { Content = item };

                //control.ContentTemplate = this.DataTemplate;

                this.Items.Add(control);

                this.AddLogicalChild(control);

            }

            this.InvalidateArrange();
        }


        //public DataTemplate DataTemplate
        //{
        //    get { return (DataTemplate)GetValue(DataTemplateProperty); }
        //    set { SetValue(DataTemplateProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DataTemplateProperty =
        //    DependencyProperty.Register("DataTemplate", typeof(DataTemplate), typeof(ObjectFrameworkElement), new PropertyMetadata(default(DataTemplate), (d, e) =>
        //     {
        //         ObjectFrameworkElement control = d as ObjectFrameworkElement;

        //         if (control == null) return;

        //         DataTemplate config = e.NewValue as DataTemplate;

        //     }));


        /// <summary>
        ///     Fills in the background based on the Background property.
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            Brush background = Brushes.Yellow;

            if (background != null)
            {
                // Using the Background brush, draw a rectangle that fills the
                // render bounds of the panel.
                Size renderSize = RenderSize;
                dc.DrawRectangle(background,
                                 null,
                                 new Rect(0.0, 0.0, renderSize.Width, renderSize.Height));
            }
        } 

        protected override Size ArrangeOverride(Size finalSize)
        {
            
            foreach (UIElement item in this.Items)
            { 
                item.Measure(finalSize);

                item.Arrange(new Rect(0, 0, item.DesiredSize.Width, item.DesiredSize.Height)); 
                 
            } 

            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.Items[index];
        }

        protected override int VisualChildrenCount => this.Items.Count;
    }


    [ContentProperty("Children")]
    class UIElementCollectionDemo : FrameworkElement
    {

        UIElementCollection _children;

        public UIElementCollection Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new UIElementCollection(this, this);
                }

                return _children;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {

            foreach (UIElement item in this.Children)
            {
                item.Measure(finalSize); 

                this.AddLogicalChild(item);

                item.Arrange(new Rect(0, 0, item.DesiredSize.Width, item.DesiredSize.Height));

            }

            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.Children[index];
        }

        protected override int VisualChildrenCount => this.Children.Count;
    }
}
