// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public class ListBoxSelectAllCheckBoxBehavior : Behavior<CheckBox>
    {
        public ListBox ListBox
        {
            get { return (ListBox)GetValue(ListBoxProperty); }
            set { SetValue(ListBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListBoxProperty =
            DependencyProperty.Register("ListBox", typeof(ListBox), typeof(ListBoxSelectAllCheckBoxBehavior), new FrameworkPropertyMetadata(default(ListBox), (d, e) =>
             {
                 ListBoxSelectAllCheckBoxBehavior control = d as ListBoxSelectAllCheckBoxBehavior;

                 if (control == null) return;

                 if (e.OldValue is ListBox o)
                 {

                 }

                 if (e.NewValue is ListBox n)
                 {

                 }
             }));


        protected override void OnAttached()
        {
            AssociatedObject.Checked += AssociatedObject_Checked;
            AssociatedObject.Unchecked += AssociatedObject_Unchecked;
        }

        private void AssociatedObject_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.ListBox == null)
            {
                this.ListBox = this.AssociatedObject.GetParent<ListBox>();
            }
            if (e.OriginalSource is FrameworkElement element)
                if (element.GetParent<ListBoxItem>().IsMouseOver == false)
                    return;
            this.ListBox.UnselectAll();
        }

        private void AssociatedObject_Checked(object sender, RoutedEventArgs e)
        {
            if (this.ListBox == null)
            {
                this.ListBox = this.AssociatedObject.GetParent<ListBox>();
            }
            if (e.OriginalSource is FrameworkElement element)
                if (element.GetParent<ListBoxItem>().IsMouseOver == false)
                    return;
            this.ListBox.SelectAll();
        }
    }


    public class ListDropBlendBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += AssociatedObjectOnDrop;
            AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Drop -= AssociatedObjectOnDrop;
            AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (AssociatedObject == null)
                return;
            if (AssociatedObject.AllowDrop)
                return;
            AssociatedObject.AllowDrop = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            AssociatedObject.AllowDrop = true;
        }
        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            AssociatedObject.AllowDrop = e.Data.GetDataPresent(this.DragGroup);
            Debug.WriteLine("设置类型一样才可以拖动");
        }

        /// <summary> 判断可否放置的分组 </summary>
        public string DragGroup
        {
            get { return (string)GetValue(DragGroupProperty); }
            set { SetValue(DragGroupProperty, value); }
        }

        public static readonly DependencyProperty DragGroupProperty =
            DependencyProperty.Register("DragGroup", typeof(string), typeof(ListDropBlendBehavior), new PropertyMetadata("DragGroup", (d, e) =>
            {
                ListDropBlendBehavior control = d as ListDropBlendBehavior;

                if (control == null) return;

                string config = e.NewValue as string;

            }));

        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            ListBox dropTarget = sender as ListBox;
            if (dropTarget == null)
                return;
            if (!dragEventArgs.Data.GetDataPresent(this.DragGroup))
                return;
            object data = this.GetDataByMousePoint(dragEventArgs.GetPosition(this.AssociatedObject));
            int index = data == null ? (this.AssociatedObject.ItemsSource as IList).Count : (this.AssociatedObject.ItemsSource as IList).IndexOf(data);
            object dragData = dragEventArgs.Data.GetData(this.DragGroup);
            if (dragEventArgs.AllowedEffects == DragDropEffects.Copy)
            {
                if (dragData is ICloneable cloneable)
                {
                    (dropTarget.ItemsSource as IList).Insert(index, cloneable.Clone());
                }
                else
                {
                    (dropTarget.ItemsSource as IList).Insert(index, dragData);
                }
            }
            else
            {
                (dropTarget.ItemsSource as IList).Insert(index, dragData);
            }
        }

        private object GetDataByMousePoint(Point point)
        {
            UIElement element = this.AssociatedObject.InputHitTest(point) as UIElement;

            if (element == null)
            {
                element = this.AssociatedObject.InputHitTest(new Point(point.X, point.Y - 10)) as UIElement;
            }

            object data = DependencyProperty.UnsetValue;

            while (data == DependencyProperty.UnsetValue)
            {
                data = this.AssociatedObject.ItemContainerGenerator.ItemFromContainer(element);

                if (data == DependencyProperty.UnsetValue)
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }

                if (element == this.AssociatedObject) return null;
            }

            return data == DependencyProperty.UnsetValue ? null : data;
        }
    }

    /// <summary> 单点击当前控件时ListBoxItem项值也选中 </summary>
    public class SelectListBoxItemElementBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent,
                new MouseButtonEventHandler(OnMouseLeftButtonDown), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.PreviewMouseLeftButtonDownEvent
                , new MouseButtonEventHandler(OnMouseLeftButtonDown));
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem parent = AssociatedObject.GetParent<ListBoxItem>();

            if (parent != null)
            {
                parent.IsSelected = true;
            }
        }
    }

    /// <summary> 但点击当前控件时ListBoxItem项值也选中 </summary>
    public class TextBoxSelectionStartBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(TextBox.TextChangedEvent,
                new TextChangedEventHandler(OnTextChangedEvent), false);
        }

        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(TextBox.TextChangedEvent
                , new TextChangedEventHandler(OnTextChangedEvent));
        }



        private void OnTextChangedEvent(object sender, TextChangedEventArgs e)
        {
            TextBox parent = sender as TextBox;

            if (parent != null)
            {
                parent.Focus();

                parent.SelectionStart = 200;

            }


        }
    }

}