// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    public class SetTreeViewItemSelectedOnMouseDownBehavior : Behavior<FrameworkElement>
    {
        public bool Use
        {
            get { return (bool)GetValue(UseProperty); }
            set { SetValue(UseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseProperty =
            DependencyProperty.Register("Use", typeof(bool), typeof(SetTreeViewItemSelectedOnMouseDownBehavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 SetTreeViewItemSelectedOnMouseDownBehavior control = d as SetTreeViewItemSelectedOnMouseDownBehavior;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));

        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseDown += AssociatedObject_MouseDown; ;
        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.Use) return;
            var item = this.AssociatedObject.GetParent<TreeViewItem>();
            if (item == null) return;
            item.IsSelected = true;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseDown -= AssociatedObject_MouseDown;
        }
    }

    public class TreeViewItemContainSelectItemBehavior : Behavior<TreeViewItem>
    {
        public static bool GetIsContain(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsContainProperty);
        }

        public static void SetIsContain(DependencyObject obj, bool value)
        {
            obj.SetValue(IsContainProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsContainProperty =
            DependencyProperty.RegisterAttached("IsContain", typeof(bool), typeof(TreeViewItemContainSelectItemBehavior), new PropertyMetadata(default(bool), OnIsContainChanged));

        static public void OnIsContainChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }

        protected override void OnAttached()
        {
            AssociatedObject.Selected += AssociatedObject_Selected;
            AssociatedObject.Unselected += AssociatedObject_Unselected;
        }

        private void AssociatedObject_Unselected(object sender, RoutedEventArgs e)
        {
            Action<TreeViewItem> action = null;
            action = t =>
            {
                var parent = ItemsControl.ItemsControlFromItemContainer(t);
                if (parent is TreeViewItem item)
                {
                    TreeViewItemContainSelectItemBehavior.SetIsContain(item, false);
                    action(item);
                }

                return;
            };
            action.Invoke(this.AssociatedObject);
        }

        private void AssociatedObject_Selected(object sender, RoutedEventArgs e)
        {
            Action<TreeViewItem> action = null;
            action = t =>
            {
                var parent = ItemsControl.ItemsControlFromItemContainer(t);
                if (parent is TreeViewItem item)
                {
                    TreeViewItemContainSelectItemBehavior.SetIsContain(item, true);
                    action(item);
                }

                return;
            };
            action.Invoke(this.AssociatedObject);
        }
    }


}
