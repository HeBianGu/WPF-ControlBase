using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 用于显示ListBoxItem拖拽进行时的效果，通过附加属性Bool修改样式 </summary>
    public class ElementAllowDrapBehavior : DrapAdornerBehavior
    {
        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;

            AssociatedObject.Drop += AssociatedObjectOnDrop;

            AssociatedObject.DragEnter += AssociatedObject_DragEnter;

            AssociatedObject.DragLeave += AssociatedObject_DragLeave;

            base.OnAttached();

        }

        protected override void OnDetaching()
        {
            AssociatedObject.AllowDrop = false;

            AssociatedObject.Drop -= AssociatedObjectOnDrop;

            AssociatedObject.DragEnter -= AssociatedObject_DragEnter;

            AssociatedObject.DragLeave -= AssociatedObject_DragLeave;

            base.OnDetaching();
        }

        protected override void DoDragDrop()
        {
            var items = this.AssociatedObject.GetParent<ItemsControl>();

            if (items == null) return;

            if (items.ItemsSource == null)
            {
                var dragData = new DataObject(this.DragGroup, (this.AssociatedObject is ListBoxItem item) ? item.Content : this.AssociatedObject);

                int index = items.Items.IndexOf(this.AssociatedObject);

                var result = DragDrop.DoDragDrop(this.AssociatedObject, dragData, DragDropEffects);

                //  Do ：移动成功清理数据
                if (result == DragDropEffects.Move)
                {
                    if (this.AssociatedObject is FrameworkElement frameworkElement)
                    {
                        int index1 = items.Items.IndexOf(this.AssociatedObject);

                        if (index == index1)
                        {
                            //  Do ：下移 
                            items.Items.RemoveAt(index);
                        }
                        else
                        {
                            //  Do ：上移
                            items.Items.RemoveAt(index + 1);
                        }
                    }
                }
            }
            else
            {
                var data = (this.AssociatedObject as FrameworkElement).DataContext;

                var dragData = new DataObject(this.DragGroup, (this.AssociatedObject as FrameworkElement).DataContext);

                int index = (items.ItemsSource as IList).IndexOf(data);

                var result = DragDrop.DoDragDrop(this.AssociatedObject, dragData, DragDropEffects);

                //  Do ：移动成功清理数据

                if (result == DragDropEffects.Move)
                {
                    if (this.AssociatedObject is FrameworkElement frameworkElement)
                    {
                        int index1 = (items.ItemsSource as IList).IndexOf(data);

                        if (index == index1)
                        {
                            //  Do ：下移 
                            (items.ItemsSource as IList).RemoveAt(index);
                        }
                        else
                        {
                            //  Do ：上移
                            (items.ItemsSource as IList).RemoveAt(index + 1);
                        }
                    }
                }
            }
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            Cattach.SetIsDragEnter(this.AssociatedObject, false);
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            Cattach.SetIsDragEnter(this.AssociatedObject, true);
        }

        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            Cattach.SetIsDragEnter(this.AssociatedObject, false);
        }

        //public static readonly DependencyProperty IsActiveProperty =
        //    DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(ElementAllowDrapBehavior),
        //        new PropertyMetadata(default(bool), OnIsActiveChanged));

        //public static bool GetIsActive(DependencyObject obj)
        //{
        //    return (bool)obj.GetValue(ElementAllowDrapBehavior.IsActiveProperty);
        //}

        //public static void SetIsActive(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(IsActiveProperty, value);
        //}

        //private static void OnIsActiveChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        //{
        //    BehaviorCollection bc = Interaction.GetBehaviors(dpo);

        //    if (Convert.ToBoolean(e.NewValue))
        //    {
        //        bc.Add(new ElementAllowDrapBehavior());
        //    }
        //    else
        //    {
        //        var behavior = bc.FirstOrDefault(beh => beh.GetType() == typeof(ElementAllowDrapBehavior));
        //        if (behavior != null)
        //            bc.Remove(behavior);
        //    }
        //}
    }

    //  ToDo：目前不好用 后面测试
    public abstract class ActiveBehavior<B, T> : Behavior<T> where T : DependencyObject where B : Behavior, new()
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(B),
                new PropertyMetadata(default(bool), OnIsActiveChanged));

        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(ActiveBehavior<B, T>.IsActiveProperty);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(ActiveBehavior<B, T>.IsActiveProperty, value);
        }

        private static void OnIsActiveChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            BehaviorCollection bc = Interaction.GetBehaviors(dpo);

            if (Convert.ToBoolean(e.NewValue))
            {
                bc.Add(new B());
            }
            else
            {
                var behavior = bc.FirstOrDefault(beh => beh.GetType() == typeof(B));
                if (behavior != null)
                    bc.Remove(behavior);
            }
        }
    }
}



