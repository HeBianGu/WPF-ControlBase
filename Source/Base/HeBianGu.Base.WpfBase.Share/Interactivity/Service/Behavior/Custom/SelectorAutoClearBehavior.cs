// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 当选中一个项时，自动恢复不选中状态，只触发OnSelectionChanged事件</summary>
    public class SelectorAutoClearBehavior : Behavior<Selector>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;

            AssociatedObject.SelectionChanged += OnSelectionChanged;

            base.OnAttached();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            base.OnDetaching();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //if (this.AssociatedObject.SelectedItems == null) return;
            //if (this.AssociatedObject.SelectedItems.Count==0) return; 
            //this.AssociatedObject.SelectedItems.Clear();

            this.Refresh();
        }

        private void Refresh()
        {
            if (this.AssociatedObject.SelectedItem == null) return;

            ListBoxItem find = this.AssociatedObject.ItemContainerGenerator.ContainerFromItem(this.AssociatedObject.SelectedItem) as ListBoxItem;

            if (find == null) return;

            find.IsSelected = false;
        }
    }

}