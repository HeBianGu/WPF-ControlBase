using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Provides a framework for <see cref="Panel"/> elements that virtualize their child data collection. This is an abstract class.
    /// </summary>
    public abstract class VirtualPanel : VirtualizingPanel
    {
        /// <summary>
        /// Identifies the <see cref="IsVirtualizing"/> property.
        /// </summary>
        public static readonly DependencyProperty IsVirtualizingProperty = VirtualizingStackPanel.IsVirtualizingProperty.AddOwner(typeof(VirtualPanel), new FrameworkPropertyMetadata(VirtualizingStackPanel.IsVirtualizingProperty.DefaultMetadata.DefaultValue, OnIsVirtualizingChanged));

        /// <summary>
        /// Handles the event that occurs when the value of the <see cref="IsVirtualizing"/> dependency property has changed.
        /// </summary>
        /// <param name="d">The dependency object on which the dependency property has changed.</param>
        /// <param name="e">The event args containing the old and new values of the dependency property.</param>
        private static void OnIsVirtualizingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var panel = d as VirtualPanel;
            if (panel != null)
            {
                panel.OnIsVirtualizingChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates that this <see cref="VirtualPanel"/> is virtualizing its child collection.
        /// </summary>
        public bool IsVirtualizing
        {
            get { return (bool)GetValue(IsVirtualizingProperty); }
            set { SetValue(IsVirtualizingProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="RealizationPriority"/> property.
        /// </summary>
        public static readonly DependencyProperty RealizationPriorityProperty = DependencyProperty.Register("RealizationPriority", typeof(DispatcherPriority), typeof(VirtualPanel), new FrameworkPropertyMetadata(DispatcherPriority.Normal));

        /// <summary>
        /// Gets or sets the <see cref="DispatcherPriority"/> of the realization pass for this <see cref="VirtualPanel"/>.
        /// </summary>
        public DispatcherPriority RealizationPriority
        {
            get { return (DispatcherPriority)GetValue(RealizationPriorityProperty); }
            set { SetValue(RealizationPriorityProperty, value); }
        }

        /// <summary>
        /// This is an attached property that the panel sets on each container (generated or direct) to point back to the index of the item.
        /// </summary>
        private static readonly DependencyProperty IndexForItemContainerProperty = DependencyProperty.RegisterAttached("IndexForItemContainer", typeof(int), typeof(VirtualPanel), new FrameworkPropertyMetadata(-1));

        /// <summary>
        /// Holds the latest queued realization operation.
        /// </summary>
        private DispatcherOperation RealizeOperation
        {
            get; 
            set;
        }

        /// <summary>
        /// Returns the <see cref="ItemsControl"/> that this panel hosts items for.
        /// </summary>
        /// <value></value>
        public ItemsControl ItemsOwner
        {
            get
            {
                return ItemsControl.GetItemsOwner(this);
            }
        }

        /// <summary>
        /// Returns the index to an item that corresponds to the specified, generated <see cref="UIElement"/>. 
        /// </summary>
        /// <param name="container">The <see cref="UIElement"/> that corresponds to the item index to be returned.</param>
        /// <returns>An <see cref="Int32"/> index to an item that corresponds to the specified <see cref="UIElement"/> if it was generated and hosted by this panel; otherwise, <c>-1</c>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public int IndexFromContainer(UIElement container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            // The generator is technically not necessary, but it ensures the container was generated and hosted by this panel.
            var generator = ItemContainerGenerator as ItemContainerGenerator;
            if (generator != null && generator.ItemFromContainer(container) != DependencyProperty.UnsetValue)
            {
                var index = container.ReadLocalValue(IndexForItemContainerProperty);
                return index as int? ?? -1;
            }
            return -1;
        }

        /// <summary>
        /// Returns the item that corresponds to the specified, generated <see cref="UIElement"/>. 
        /// </summary>
        /// <param name="container">The <see cref="UIElement"/> that corresponds to the item to be returned.</param>
        /// <returns>An <see cref="Object"/> that is the item which corresponds to the specified <see cref="UIElement"/> if it was generated and hosted by this panel; otherwise, <c>null</c>.</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public object ItemFromContainer(UIElement container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            var generator = ItemContainerGenerator as ItemContainerGenerator;
            if (generator != null)
            {
                var item = generator.ItemFromContainer(container);
                return item != DependencyProperty.UnsetValue ? item : null;
            }
            return null;
        }

        /// <summary>
        /// Returns the <see cref="UIElement"/> corresponding to the item at the given index within the item collection if it has been realized.
        /// </summary>
        /// <param name="itemIndex">The index of the desired item. </param>
        /// <returns>The element corresponding to the item at the given index within the item collection or returns <c>null</c> if the item is not realized.</returns>
        public UIElement ContainerFromIndex(int itemIndex)
        {
            var generator = ItemContainerGenerator as ItemContainerGenerator;
            if (generator != null)
            {
                return generator.ContainerFromIndex(itemIndex) as UIElement;
            }
            return null;
        }

        /// <summary>
        /// Returns the <see cref="UIElement"/> corresponding to the given item if it has been realized.
        /// </summary>
        /// <param name="item">The <see cref="Object"/> item to find the <see cref="UIElement"/> for.</param>
        /// <returns>A <see cref="UIElement"/> that corresponds to the given item. Returns <c>null</c> if the item does not belong to the item collection, or if a <see cref="UIElement"/> has not been generated for it.</returns>
        /// <remarks>Use caution when calling this method as it does a linear search for the item.  Consider calling <see cref="ContainerFromIndex"/> instead.</remarks>
        public UIElement ContainerFromItem(object item)
        {
            var generator = ItemContainerGenerator as ItemContainerGenerator;
            if (generator != null)
            {
                return generator.ContainerFromItem(item) as UIElement;
            }
            return null;
        }

        /// <summary>
        /// Invalidates the realization state of all items being hosted by this panel. After the invalidation, the panel will have its reality updated, which will occur asynchronously unless subsequently forced by <see cref="UpdateReality"/>. 
        /// </summary>
        public void InvalidateReality()
        {
            if (RealizeOperation != null)
            {
                RealizeOperation.Abort();
            }

            object state = null;
            Action action = null;

            action = delegate
            {
                RealizeOperation = null;
                state = RealizeCore(state);
                if (state != null && RealizeOperation == null)
                {
                    RealizeOperation = Dispatcher.BeginInvoke(action, RealizationPriority);
                }
            };

            RealizeOperation = Dispatcher.BeginInvoke(action, RealizationPriority);
        }

        /// <summary>
        /// Ensures that all items being hosted by this panel are properly realized or virtualized.
        /// </summary>
        public void UpdateReality()
        {
            RealizeOperation.Abort();
            RealizeOperation = null;
            object state = null;
            do
            {
                state = RealizeCore(state);
            }
            while (state != null);
        }

        /// <summary>
        /// Manages calls to <see cref="RealizeOverride"/>.
        /// </summary>
        /// <param name="state">A custom state object left over from a previous call to <see cref="RealizeCore"/> if additional processing was needed.</param>
        /// <returns>A custom state object if additional processing is needed; otherwise, <c>null</c>.</returns>
        private object RealizeCore(object state)
        {
            if (IsItemsHost)
            {
                if (IsVirtualizing)
                {
                    var owner = ItemsOwner;
                    if (owner != null)
                    {
                        return RealizeOverride(owner.ItemsSource ?? owner.Items, state);
                    }
                }
                else if (InternalChildren.Count == 0)
                {
                    var generator = ItemContainerGenerator;
                    using (generator.StartAt(new GeneratorPosition(-1, 0), GeneratorDirection.Forward))
                    {
                        int index = 0;
                        DependencyObject next;
                        while ((next = generator.GenerateNext()) != null)
                        {
                            var container = next as UIElement;
                            if (container != null)
                            {
                                container.SetValue(IndexForItemContainerProperty, index);
                                AddInternalChild(container);
                                generator.PrepareItemContainer(next);
                            }
                            index++;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// When overridden in a derived class, realizes and/or virtualizes items, optionally deferring additional realization.
        /// </summary>
        /// <param name="items">The current items being hosted by this panel.</param>
        /// <param name="state">A custom state object left over from a previous call to <see cref="RealizeOverride"/> if additional processing was needed.</param>
        /// <returns>Implementations may optionally defer additional processing by return a non-<c>null</c> object, which will then be passed to a future call to <see cref="RealizeOverride"/>.</returns>
        protected abstract object RealizeOverride(IEnumerable items, object state);

        /// <summary>
        /// Indicates that the <see cref="Panel.IsItemsHost"/> property value has changed.
        /// </summary>
        /// <param name="oldIsItemsHost">The old property value.</param>
        /// <param name="newIsItemsHost">The new property value.</param>
        protected override void OnIsItemsHostChanged(bool oldIsItemsHost, bool newIsItemsHost)
        {
            base.OnIsItemsHostChanged(oldIsItemsHost, newIsItemsHost);

            InvalidateReality();
        }

        /// <summary>
        /// Indicates that the <see cref="IsVirtualizing"/> property value has changed.
        /// </summary>
        /// <param name="oldIsVirtualizing">The old property value.</param>
        /// <param name="newIsVirtualizing">The new property value.</param>
        protected virtual void OnIsVirtualizingChanged(bool oldIsVirtualizing, bool newIsVirtualizing)
        {
            InvalidateReality();
        }

        /// <summary>
        /// Maintains event handlers when the items source has changed.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="args">Provides data for the <see cref="ItemContainerGenerator.ItemsChanged"/> event.</param>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override sealed void OnItemsChanged(object sender, ItemsChangedEventArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            var owner = ItemsOwner;
            var items = owner != null ? owner.Items : null;

            var action = args.Action;
            if (action == NotifyCollectionChangedAction.Reset)
            {
                OnItemsChanged(items, new NotifyCollectionChangedEventArgs(action));
            }
            else if (items != null)
            {
                var generator = sender as IItemContainerGenerator;
                if (generator != null)
                {
                    if (action == NotifyCollectionChangedAction.Add)
                    {
                        var index = generator.IndexFromGeneratorPosition(args.Position);
                        // ItemContainerGenerator is infested with bugs. One of which is that as items are added to the end of a collection, the index from IndexFromGeneratorPosition will usually be wrong.
                        if (args.Position.Offset == 1)
                        {
                            index = items.Count - 1;
                        }
                        var newItems = new VirtualItemsList(items, index, args.ItemCount);

                        if (!IsVirtualizing)
                        {
                            for (int i = index; i < index + args.ItemCount; i++)
                            {
                                RealizeItem(i);
                            }
                        }

                        OnItemsChanged(items, new NotifyCollectionChangedEventArgs(action, newItems, index));
                    }
                    else if (action == NotifyCollectionChangedAction.Remove)
                    {
                        var oldIndex = generator.IndexFromGeneratorPosition(args.Position);
                        var oldItems = new ArrayList(args.ItemCount);
                        // Since we can't actually get the old items directly, and sometimes we can't even get the old index from the generator, we'll get as many as we can from the visuals.
                        for (int i = 0; i < args.ItemUICount; i++)
                        {
                            var element = InternalChildren[args.Position.Index + i];
                            oldItems.Add(ItemFromContainer(element));
                            if (oldIndex == -1)
                            {
                                oldIndex = (int)element.ReadLocalValue(IndexForItemContainerProperty);

                            }
                            element.ClearValue(IndexForItemContainerProperty);
                        }

                        // Unfortunately ItemContainerGenerator always expects us to remove the child, even if we didn't want to.
                        if (args.ItemUICount > 0)
                        {
                            RemoveInternalChildRange(args.Position.Index, args.ItemUICount);
                            
                        }
                        // HACK ($$$) - the position generating logic is returning the wrong position
                        // the position generated for the last item is beyond the bounds of the collection
                        // causing "RemoveRange" to fail 
                        int indexToRemove = (oldIndex > args.Position.Index) ? args.Position.Index : oldIndex;
                        OnItemsChanged(items, new NotifyCollectionChangedEventArgs(action, oldItems, indexToRemove));
                    }
                    else if (action == NotifyCollectionChangedAction.Move)
                    {
                        var oldIndex = -1;
                        var index = generator.IndexFromGeneratorPosition(args.Position);
                        var movedItems = new VirtualItemsList(items, index, args.ItemCount);

                        var count = args.ItemUICount;
                        if (count > 0)
                        {
                            var elements = new UIElement[count];
                            for (var i = 0; i < count; i++)
                            {
                                elements[i] = InternalChildren[args.OldPosition.Index + i];
                                if (oldIndex == -1)
                                {
                                    oldIndex = (int)elements[i].ReadLocalValue(IndexForItemContainerProperty);
                                }
                            }
                            RemoveInternalChildRange(args.OldPosition.Index + Math.Min(args.OldPosition.Offset, 1), count);
                            for (var i = 0; i < count; i++)
                            {
                                InsertInternalChild(args.Position.Index + i, elements[i]);
                            }
                        }

                        OnItemsChanged(items, new NotifyCollectionChangedEventArgs(action, movedItems, index, oldIndex));
                    }
                    else if (action == NotifyCollectionChangedAction.Replace)
                    {
                        var index = generator.IndexFromGeneratorPosition(args.Position);
                        var newItems = new VirtualItemsList(items, index, args.ItemCount);
                        // Todo: Actually get the old items.  ItemContainerGenerator doesn't make this easy.
                        var oldItems = new VirtualItemsList(null, index, args.ItemCount);

                        OnItemsChanged(items, new NotifyCollectionChangedEventArgs(action, newItems, oldItems, index));
                    }
                }
            }

            base.OnItemsChanged(sender, args);

            InvalidateReality();
        }

        /// <summary>
        /// Called when the <see cref="ItemsControl.Items"/> collection that is associated with the <see cref="ItemsControl"/> for this <see cref="Panel"/> changes.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="args">Provides data for the <see cref="ItemsChanged"/> event.</param>
        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers")]
        protected virtual void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
        }

        /// <summary>
        /// Realizes an item container for the item with the given index.
        /// </summary>
        /// <param name="itemIndex">The index of the item.</param>
        /// <returns>The child that was created and added to the internal children.</returns>
        protected UIElement RealizeItem(int itemIndex)
        {
            var generator = ItemContainerGenerator;
            var position = generator.GeneratorPositionFromIndex(itemIndex);
            using (generator.StartAt(position, GeneratorDirection.Forward, true))
            {
                var isNewlyRealized = false;
                var container = generator.GenerateNext(out isNewlyRealized) as UIElement;
                if (position.Offset != 0 && container != null && isNewlyRealized)
                {
                    container.SetValue(IndexForItemContainerProperty, itemIndex);
                    InsertInternalChild(position.Index + 1, container);
                    generator.PrepareItemContainer(container);
                }
                return container;
            }
        }

        /// <summary>
        /// Removes an item container for the item with the given index.
        /// </summary>
        /// <param name="itemIndex">The index of the item.</param>
        /// <returns><c>true</c> if the child had been previously realized and was now removed; otherwise, <c>false</c>.</returns>
        protected void VirtualizeItem(int itemIndex)
        {
            var generator = ItemContainerGenerator;
            if (generator != null)
            {
                var position = generator.GeneratorPositionFromIndex(itemIndex);
                if (position.Offset == 0)
                {
                    generator.Remove(position, 1);
                    InternalChildren[position.Index].ClearValue(IndexForItemContainerProperty);
                    RemoveInternalChildRange(position.Index, 1);
                }
            }
        }

        /// <summary>
        /// Returns a list which represents a subset of the elements in the source list. 
        /// </summary>
        /// <remarks>
        /// This list is used in NotifyCollectionChangedEventArgs because we might be dealing with
        /// virtualized lists that raise events for items changing when the items haven't been
        /// loaded into memory yet.  If the client needs to inspect the item, then they can index
        /// into this list and it will retrieve it from the original source, but if they don't need
        /// to inspect the item then we spare the cost of the lookup and retrieval.
        /// </remarks>
        private class VirtualItemsList : IList
        {
            public VirtualItemsList(IList items, int offset, int count)
            {
                Items = items;
                Offset = offset;
                Count = count;
            }

            public IList Items
            {
                get;
                set;
            }

            public int Offset
            {
                get;
                set;
            }

            public int Count
            {
                get;
                set;
            }

            public object this[int index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }

                    if (Items != null && index + Offset < Items.Count)
                    {
                        return Items[index + Offset];
                    }

                    return null;
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            public int IndexOf(object value)
            {
                if (Items != null)
                {
                    for (int i = 0; i < Count && i + Offset < Items.Count; i++)
                    {
                        if (Object.Equals(Items[i + Offset], value))
                        {
                            return i;
                        }
                    }
                }

                return -1;
            }

            public bool Contains(object value)
            {
                return IndexOf(value) >= 0;
            }

            public IEnumerator GetEnumerator()
            {
                if (Items != null)
                {
                    for (int i = 0; i < Count && i + Offset < Items.Count; i++)
                    {
                        yield return Items[i + Offset];
                    }
                }
            }

            #region Unsupported IList Members

            public int Add(object value)
            {
                throw new NotSupportedException();
            }

            public void Clear()
            {
                throw new NotSupportedException();
            }

            public void Insert(int index, object value)
            {
                throw new NotSupportedException();
            }

            public void Remove(object value)
            {
                throw new NotSupportedException();
            }

            public void RemoveAt(int index)
            {
                throw new NotSupportedException();
            }

            public void CopyTo(Array array, int index)
            {
                throw new NotSupportedException();
            }

            public bool IsFixedSize
            {
                get { return true; }
            }

            public bool IsReadOnly
            {
                get { return true; }
            }

            public bool IsSynchronized
            {
                get { return false; }
            }

            public object SyncRoot
            {
                get { return null; }
            }

            #endregion
        }
    }
}