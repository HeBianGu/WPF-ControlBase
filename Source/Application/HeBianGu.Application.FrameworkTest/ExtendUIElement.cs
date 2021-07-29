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
    class RenderElement : UIElement
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);


            Brush background = Brushes.Red;
            if (background != null)
            {
                // Using the Background brush, draw a rectangle that fills the
                // render bounds of the panel.
                Size renderSize = RenderSize;
                drawingContext.DrawRectangle(background,
                                 null,
                                 new Rect(0.0, 0.0, renderSize.Width, renderSize.Height));

                drawingContext.DrawEllipse(Brushes.Green,
                      null,new Point(renderSize.Width/2.0, renderSize.Height/2.0),200,200);
            }
        }
    }

    [ContentProperty("Items")]
    class ArrangeElement : UIElement
    {
        
        public List<UIElement> Items
        {
            get { return (List<UIElement>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<UIElement>), typeof(ArrangeElement), new PropertyMetadata(new List<UIElement>(), (d, e) =>
            {
                ArrangeElement control = d as ArrangeElement;

                if (control == null) return;

                List<UIElement> config = e.NewValue as List<UIElement>;  


            }));

        protected override void ArrangeCore(Rect finalRect)
        {
            foreach (var item in this.Items)
            {
                item.Arrange(finalRect);
            } 
        }

        protected override Visual GetVisualChild(int index)
        {
           return this.Items[index]; 
        }

        protected override int VisualChildrenCount => this.Items.Count;
    }

     
    class ObjectElement : UIElement
    {
        public ObjectElement()
        {
            this.ItemSource.CollectionChanged +=(l, k) =>
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
            DependencyProperty.Register("ItemSource", typeof(ObservableCollection<object>), typeof(ObjectElement), new PropertyMetadata(new ObservableCollection<object>(), (d, e) =>
             {
                 ObjectElement control = d as ObjectElement;

                 if (control == null) return;

                 ObservableCollection<object> config = e.NewValue as ObservableCollection<object>;

             }));



        public List<UIElement> Items
        {
            get { return (List<UIElement>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<UIElement>), typeof(ObjectElement), new PropertyMetadata(new List<UIElement>(), (d, e) =>
             {
                 ObjectElement control = d as ObjectElement;

                 if (control == null) return;

                 List<UIElement> config = e.NewValue as List<UIElement>;

             }));

        void GenerateItems()
        {
            this.Items.Clear();

            foreach (var item in this.ItemSource)
            {
                ContentControl control = new ContentControl() { Content = item };

                this.Items.Add(control);

            } 

            this.InvalidateArrange();
        }

        protected override void ArrangeCore(Rect finalRect)
        {
            foreach (var item in this.Items)
            { 
                item.Arrange(finalRect);
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.Items[index];
        }

        protected override int VisualChildrenCount => this.Items.Count;
    }
}
