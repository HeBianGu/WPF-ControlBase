using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 用于显示ListBoxItem拖拽进行时的效果，通过附加属性Bool修改样式 </summary>
    public class ListBoxItemAllowDropBehavior : Behavior<ListBoxItem>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;

            AssociatedObject.Drop += AssociatedObjectOnDrop;

            AssociatedObject.DragEnter += AssociatedObject_DragEnter; 

            AssociatedObject.DragLeave += AssociatedObject_DragLeave;

        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            ControlAttachProperty.SetBool(this.AssociatedObject, false);
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            ControlAttachProperty.SetBool(this.AssociatedObject, true);
        }

        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            ControlAttachProperty.SetBool(this.AssociatedObject,false);
        }



        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(ListBoxItemAllowDropBehavior),
                new PropertyMetadata(default(bool), OnIsActiveChanged));

        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(ListBoxItemAllowDropBehavior.IsActiveProperty);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(IsActiveProperty, value);
        }

        private static void OnIsActiveChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            BehaviorCollection bc = Interaction.GetBehaviors(dpo);

            if (Convert.ToBoolean(e.NewValue))
            {
                bc.Add(new ListBoxItemAllowDropBehavior());
            }
            else
            {
                var behavior = bc.FirstOrDefault(beh => beh.GetType() == typeof(ListBoxItemAllowDropBehavior));
                if (behavior != null)
                    bc.Remove(behavior);
            }
        }
    }

    //  ToDo：目前不好用 后面测试
    public abstract class ActiveBehavior<B,T> : Behavior<T> where T : DependencyObject  where B : Behavior,new()
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof(bool), typeof(B),
                new PropertyMetadata(default(bool), OnIsActiveChanged));

        public static bool GetIsActive(DependencyObject obj)
        {
            return (bool)obj.GetValue(ActiveBehavior <B,T>.IsActiveProperty);
        }

        public static void SetIsActive(DependencyObject obj, bool value)
        {
            obj.SetValue(ActiveBehavior <B,T>.IsActiveProperty, value);
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



