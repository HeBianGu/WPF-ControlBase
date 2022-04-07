// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace HeBianGu.Control.Adorner
{

    /// <summary> 用于显示ListBoxItem拖拽进行时的效果，通过附加属性Bool修改样式 </summary>
    public abstract class DropAdornerBehavior<T> : Behavior<T> where T : UIElement
    {
        /// <summary> 判断可否放置的分组 </summary>
        public string DragGroup
        {
            get { return (string)GetValue(DragGroupProperty); }
            set { SetValue(DragGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragGroupProperty =
            DependencyProperty.Register("DragGroup", typeof(string), typeof(DropAdornerBehavior<T>), new PropertyMetadata("DragGroup", (d, e) =>
            {
                DropAdornerBehavior<T> control = d as DropAdornerBehavior<T>;

                if (control == null) return;

                string config = e.NewValue as string;

            }));

        protected override void OnAttached()
        {
            this.AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            DragAdorner adorner = e.Data.GetData("DragGroup") as DragAdorner;

            if (adorner == null) return;

            string temp = XamlWriter.Save(adorner.AdornedElement);

            StringReader stringReader = new StringReader(temp);

            XmlReader xmlReader = XmlReader.Create(stringReader);

            UIElement element = XamlReader.Load(xmlReader) as UIElement;

            Point offset = adorner.Offset;

            Point location = e.GetPosition(this.AssociatedObject);

            this.DropElement(element, location, offset);
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        protected abstract void DropElement(UIElement element, Point location, Point offset);
    }


    public class DropAdornerCanvasBehavior : DropAdornerBehavior<Canvas>
    {
        protected override void DropElement(UIElement element, Point location, Point offset)
        {
            this.AssociatedObject.Children.Add(element);

            Canvas.SetLeft(element, location.X - offset.X);

            Canvas.SetTop(element, location.Y - offset.Y);
        }
    }
}



