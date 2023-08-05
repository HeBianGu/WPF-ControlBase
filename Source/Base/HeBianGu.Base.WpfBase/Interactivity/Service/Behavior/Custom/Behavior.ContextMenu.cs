// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 自定义ContextMenu显示时机  有个问题就是PlaceTarget没有传递进来 后面看下
    /// </summary>
    public class ContextMenuDisplayBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
            this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
        }


        public MouseButton MouseButton
        {
            get { return (MouseButton)GetValue(MouseButtonProperty); }
            set { SetValue(MouseButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseButtonProperty =
            DependencyProperty.Register("MouseButton", typeof(MouseButton), typeof(ContextMenuDisplayBehavior), new PropertyMetadata(MouseButton.Right, (d, e) =>
             {
                 ContextMenuDisplayBehavior control = d as ContextMenuDisplayBehavior;

                 if (control == null) return;

                 //MouseButton config = e.NewValue as MouseButton;

             }));
        private Point _startPos;

        private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.AssociatedObject.ContextMenu == null) return;

            e.Handled = true;

            this.AssociatedObject.ContextMenu.IsOpen = false;

            if (this.MouseButton != e.ChangedButton) return;

            Point point = e.GetPosition(this.AssociatedObject);

            Point currentPosition = e.GetPosition(this.AssociatedObject);

            if (Math.Abs(currentPosition.X - _startPos.X) > 5 ||
                     Math.Abs(currentPosition.Y - _startPos.Y) > 5)
                return;
            this.AssociatedObject.ContextMenu.PlacementTarget = this.AssociatedObject;
            this.AssociatedObject.ContextMenu.IsOpen = true;


        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _startPos = e.GetPosition(this.AssociatedObject);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
        }
    }

    public class MouseOverContextMenuBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        }

        private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
        {
            this.AssociatedObject.ContextMenu.PlacementTarget = this.AssociatedObject;
            this.AssociatedObject.ContextMenu.IsOpen = true;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
        }
    }

    public class ToggleButtonContextMenuBehavior : Behavior<ToggleButton>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Checked += AssociatedObject_Checked;
            this.AssociatedObject.Unchecked += AssociatedObject_Unchecked;
            this.AssociatedObject.ContextMenu.Closed += ContextMenu_Closed;
            //DependencyPropertyDescriptor isOpen = DependencyPropertyDescriptor.FromProperty(ContextMenu.IsOpenProperty, typeof(ContextMenu));
            //isOpen.AddValueChanged(this.AssociatedObject.ContextMenu, new EventHandler(ContextMenu_Closed));
        }

        private void AssociatedObject_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
            this.AssociatedObject.IsChecked = false;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.Checked -= AssociatedObject_Checked;
            this.AssociatedObject.Unchecked -= AssociatedObject_Unchecked;
            this.AssociatedObject.ContextMenu.Closed -= ContextMenu_Closed;
        }

        private void ContextMenu_Closed(object sender, EventArgs e)
        {
            this.AssociatedObject.IsChecked = false;
        }

        private void AssociatedObject_Unchecked(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.ContextMenu.IsOpen = false;
        }

        private void AssociatedObject_Checked(object sender, RoutedEventArgs e)
        {
            this.AssociatedObject.ContextMenu.PlacementTarget = this.AssociatedObject;
            this.AssociatedObject.ContextMenu.IsOpen = true;
        }
    }
}
