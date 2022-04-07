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
}