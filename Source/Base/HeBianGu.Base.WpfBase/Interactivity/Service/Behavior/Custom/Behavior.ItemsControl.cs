// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// ItemsControl 带有删除子项的行为
    /// </summary>
    public class CloseItemsAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ClosingCheckFuncProperty =
            DependencyProperty.Register("ClosingCheckFunc", typeof(Func<bool>), typeof(CloseItemsAction), new PropertyMetadata(null));


        public static readonly DependencyProperty ItemsControlProperty =
                    DependencyProperty.Register(nameof(ItemsControl), typeof(ItemsControl), typeof(CloseItemsAction), new PropertyMetadata(default(ItemsControl)));


        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register(nameof(Item), typeof(FrameworkElement), typeof(CloseItemsAction), new PropertyMetadata(default(FrameworkElement)));


        public Func<bool> ClosingCheckFunc
        {
            get { return (Func<bool>)GetValue(ClosingCheckFuncProperty); }
            set { SetValue(ClosingCheckFuncProperty, value); }
        }

        public ItemsControl ItemsControl
        {
            get { return (ItemsControl)this.GetValue(ItemsControlProperty); }
            set { this.SetValue(ItemsControlProperty, value); }
        }

        public FrameworkElement Item
        {
            get { return (FrameworkElement)this.GetValue(ItemProperty); }
            set { this.SetValue(ItemProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            ItemsControl tabControl = this.ItemsControl;

            FrameworkElement tabItem = this.Item;

            if (tabControl == null || tabItem == null)
            {
                return;
            }

            if (ClosingCheckFunc != null && !ClosingCheckFunc())
            {
                return;
            }

            if (tabControl.ItemsSource == null)
            {
                tabControl.Items.Remove(tabItem);
            }
            else
            {
                IList collection = tabControl.ItemsSource as IList;

                if (collection == null) return;

                //  ToEdit ：
                object find = tabControl.ItemContainerGenerator.ItemFromContainer(tabItem);

                //  Do ：此处数据源要使用INotifyCollectionChanged 否则页面没有更新
                collection.Remove(find);

            }
        }
    }


    /// <summary> ItemsControl支持显示行号 </summary>
    public class ItemsControlItemIndexBehavior : Behavior<TextBlock>
    {
        //public FrameworkElement Item
        //{
        //    get { return (FrameworkElement)GetValue(ItemProperty); }
        //    set { SetValue(ItemProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemProperty =
        //    DependencyProperty.Register("Item", typeof(FrameworkElement), typeof(ItemsControlItemIndexBehavior), new PropertyMetadata(default(FrameworkElement), (d, e) =>
        //     {
        //         ItemsControlItemIndexBehavior control = d as ItemsControlItemIndexBehavior;

        //         if (control == null) return;

        //         FrameworkElement config = e.NewValue as FrameworkElement;

        //     }));

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPresenter item = this.AssociatedObject.GetParent<ContentPresenter>();
            if (item == null) 
                return;
            ItemsControl items = item.GetParent<ItemsControl>();
            int index = items.ItemContainerGenerator.Items.IndexOf(item.DataContext);
            AssociatedObject.Text = index.ToString();

        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }
    }
}