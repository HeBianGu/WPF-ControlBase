// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// This behavior provides a multi-selection binder to tie a ViewModel collection to
    /// a WPF multi-select list.  This includes ListBox, ListView, DataGrid, Calendar, and anything
    /// else which supports the Selector or MultiSelector base class.
    /// </summary>
    /// 
    /// <summary> 支持列表控件绑定多选的行为 </summary>
    public class SelectedItemsCollectionSynchronizerBehavior : Behavior<Selector>
    {
        private volatile bool _updatingList;
        private IList _selectedItems;

        /// <summary>
        /// Property to associate selected items
        /// </summary>
        public static readonly DependencyProperty CollectionProperty =
            DependencyProperty.Register("Collection", typeof(IList), typeof(SelectedItemsCollectionSynchronizerBehavior),
                                new FrameworkPropertyMetadata(null, OnBackingCollectionChanged));

        /// <summary>
        /// Instance wrapper for selected items backing storage
        /// </summary>
        public IList Collection
        {
            get { return (IList)GetValue(CollectionProperty); }
            set { SetValue(CollectionProperty, value); }
        }

        /// <summary>
        /// True/False whether to go both directions in selection
        /// </summary>
        public static readonly DependencyProperty BindsTwoWayProperty =
            DependencyProperty.Register("BindsTwoWay", typeof(bool), typeof(SelectedItemsCollectionSynchronizerBehavior), new PropertyMetadata(true));

        /// <summary>
        /// True/False whether to go both directions in selection
        /// </summary>
        public bool BindsTwoWay
        {
            get { return (bool)GetValue(BindsTwoWayProperty); }
            set { SetValue(BindsTwoWayProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += OnSelectionChanged;

            // Special case ListBox.
            if (AssociatedObject is ListBox && ((ListBox)AssociatedObject).SelectionMode != SelectionMode.Single)
            {
                _selectedItems = ((ListBox)AssociatedObject).SelectedItems;
            }
            // Special case MultiSelector.
            if (AssociatedObject is MultiSelector)
            {
                _selectedItems = ((MultiSelector)AssociatedObject).SelectedItems;
            }

            if (Collection != null)
                SynchronizeBackingStore();

            base.OnAttached();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
            base.OnDetaching();
        }

        /// <summary>
        /// Called when the SelectedItems property is changed.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnBackingCollectionChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ((SelectedItemsCollectionSynchronizerBehavior)dpo).OnBackingCollectionChanged((IList)e.OldValue, (IList)e.NewValue);
        }

        /// <summary>
        /// Called when the SelectedItems property is changed.
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private void OnBackingCollectionChanged(IList oldValue, IList newValue)
        {
            // First, unwire from any existing collection.
            if (oldValue != null)
            {
                INotifyCollectionChanged notifyCollection = oldValue as INotifyCollectionChanged;
                if (notifyCollection != null)
                    notifyCollection.CollectionChanged -= OnBackingStoreElementsChanged;
            }

            if (newValue != null)
            {
                if (AssociatedObject != null)
                    SynchronizeBackingStore();

                // See if it supports change notification ala ObservableCollection.
                INotifyCollectionChanged notifyCollection = newValue as INotifyCollectionChanged;
                if (notifyCollection != null)
                    notifyCollection.CollectionChanged += OnBackingStoreElementsChanged;
            }
        }

        /// <summary>
        /// This synchronizes the backing collection with the ItemsControl
        /// </summary>
        private void SynchronizeBackingStore()
        {
            // Pickup any existing selection
            Collection.Clear();

            if (_selectedItems != null)
            {
                foreach (object item in _selectedItems)
                    Collection.Add(item);
            }
            else
            {
                object item = AssociatedObject.SelectedItem;
                if (item != null)
                    Collection.Add(item);
            }
        }

        /// <summary>
        /// This is called when the elements inside the backed storage collection change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBackingStoreElementsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Ignore if we don't support two-way binding.
            if (!BindsTwoWay)
            {
                INotifyCollectionChanged notifyCollection = sender as INotifyCollectionChanged;
                if (notifyCollection != null)
                    notifyCollection.CollectionChanged -= OnBackingStoreElementsChanged;
                return;
            }

            if (!_updatingList)
            {
                _updatingList = true;
                try
                {
                    if (_selectedItems != null)
                    {
                        _selectedItems.Clear();
                        foreach (object item in Collection)
                            _selectedItems.Add(item);
                    }
                    else
                    {
                        object firstItem = Collection.Count > 0 ? Collection[0] : null;
                        AssociatedObject.SelectedItem = firstItem;
                    }
                }
                finally
                {
                    _updatingList = false;
                }
            }
        }

        /// <summary>
        /// Called when the Selection changed event is raised by the associated selector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_updatingList)
                return;

            _updatingList = true;
            try
            {
                if (Collection != null)
                {
                    if (e.RemovedItems != null)
                    {
                        foreach (object val in e.RemovedItems)
                            Collection.Remove(val);
                    }

                    if (e.AddedItems != null)
                    {
                        foreach (object val in e.AddedItems)
                            Collection.Add(val);
                    }
                }
            }
            finally
            {
                _updatingList = false;
            }
        }
    }

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



    public class SelectorBindingSelectItemsBehavior : Behavior<Selector>
    {
        public IEnumerator SelectedItems
        {
            get { return (IEnumerator)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IEnumerator), typeof(SelectorBindingSelectItemsBehavior), new FrameworkPropertyMetadata(default(IEnumerator), (d, e) =>
            {
                SelectorBindingSelectItemsBehavior control = d as SelectorBindingSelectItemsBehavior;

                if (control == null) return;

                if (e.OldValue is IEnumerator o)
                {

                }

                if (e.NewValue is IEnumerator n)
                {

                }

            }));


        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SelectedItems is IList list)
            {
                foreach (var item in e.RemovedItems)
                {
                    list.Remove(item);
                }
                foreach (var item in e.AddedItems)
                {
                    list.Add(item);
                }
            }
        }
    }
}
