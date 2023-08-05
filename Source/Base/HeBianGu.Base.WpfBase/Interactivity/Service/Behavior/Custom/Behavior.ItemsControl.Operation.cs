// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Windows;
using System.Windows.Controls;


namespace HeBianGu.Base.WpfBase
{
    public abstract class ItemsControlButtonBehaviorBase : Behavior<Button>
    {
        public FrameworkElement Item
        {
            get { return (FrameworkElement)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(FrameworkElement), typeof(ItemsControlButtonBehaviorBase), new FrameworkPropertyMetadata(default(FrameworkElement), (d, e) =>
             {
                 ItemsControlButtonBehaviorBase control = d as ItemsControlButtonBehaviorBase;

                 if (control == null) return;

                 if (e.OldValue is FrameworkElement o)
                 {

                 }

                 if (e.NewValue is FrameworkElement n)
                 {

                 }

             }));


        protected override void OnAttached()
        {
            AssociatedObject.Click += AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            this.OnClick();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
        }

        protected abstract void OnClick();
    }

    public class RemoveItemsControlButtonBehavior : ItemsControlButtonBehaviorBase
    {
        protected override void OnClick()
        {
            var item = this.Item ?? this.AssociatedObject;
            if (item == null)
                return;

            ItemsControl ic = item.GetParent<ItemsControl>();
            if (ic == null)
                return;
            IEnumerable inner = ItemsControlService.GetInnerSource(ic);
            if (inner is IList l)
            {
                l.Remove(item.DataContext);
            }
            else
            {
                if (ic.ItemsSource is IList list)
                {
                    list.Remove(item.DataContext);
                }
                else
                {

                    ic.Items.Remove(item);
                }
            }
        }
    }

    public class ClearItemsControlButtonBehavior : ItemsControlButtonBehaviorBase
    {
        protected override void OnClick()
        {
            if (this.Item == null)
                return;

            ItemsControl ic = this.Item.GetParent<ItemsControl>();

            if (ic == null) return;

            IEnumerable inner = ItemsControlService.GetInnerSource(ic);

            if (inner is IList l)
            {
                l.Clear();
            }
            else
            {
                if (ic.ItemsSource is IList list)
                {
                    list.Clear();
                }
                else
                {

                    ic.Items.Clear();
                }
            }
        }
    }
}