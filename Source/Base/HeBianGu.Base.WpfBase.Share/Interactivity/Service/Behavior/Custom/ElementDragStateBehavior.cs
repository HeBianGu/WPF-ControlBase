// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Input;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 设置当前控件是否处于拖拽状态 </summary>
    public class ElementDragStateBehavior : Behavior<FrameworkElement>
    {
        /// <summary>
        /// 是否正在拖拽
        /// </summary>
        public static readonly DependencyProperty IsDraggingProperty = DependencyProperty.RegisterAttached(
            "IsDragging", typeof(bool), typeof(ElementDragStateBehavior), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDraggingChanged));

        public static bool GetIsDragging(DependencyObject d)
        {
            if (d == null) return false;

            return (bool)d.GetValue(IsDraggingProperty);
        }

        public static void SetIsDragging(DependencyObject obj, bool value)
        {
            if (obj == null) return;

            obj.SetValue(IsDraggingProperty, value);
        }

        private static void OnIsDraggingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {

        }

        /// <summary> 是否检测鼠标离开后设置未拖动状态 </summary>
        public bool IsUseMouseLeave { get; set; }

        protected override void OnAttached()
        {
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
            this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.IsUseMouseLeave) return;

            ElementDragStateBehavior.SetIsDragging(this.AssociatedObject, false);
        }

        private void AssociatedObject_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ElementDragStateBehavior.SetIsDragging(this.AssociatedObject, false);
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
            this.AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) return;

            ElementDragStateBehavior.SetIsDragging(this.AssociatedObject, true);

            System.Diagnostics.Debug.WriteLine("拖拽中");

        }
    }
}