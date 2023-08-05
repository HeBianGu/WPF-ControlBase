// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Multiplies a time span unit by the index of an item in a list.  
    /// </summary>
    /// <remarks>
    /// Example usage is for a <see cref="TransitioningContent"/> to have a <see cref="TransitionEffect.OffsetTime" />
    /// time delayed according to position in a list, so cascading animations can occur.
    /// </remarks>
    [MarkupExtensionReturnType(typeof(TimeSpan))]
    public class IndexedItemOffsetMultiplierExtension : MarkupExtension
    {
        public IndexedItemOffsetMultiplierExtension(TimeSpan unit)
        {
            Unit = unit;
        }

        [ConstructorArgument("unit")]
        public TimeSpan Unit { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (provideValueTarget == null) return TimeSpan.Zero;

            if (provideValueTarget.TargetObject != null &&
                provideValueTarget.TargetObject.GetType().FullName == "System.Windows.SharedDp")
                //we are inside a template, return this, so we can re-evaluate later...
                return this;

            DependencyObject element = provideValueTarget?.TargetObject as DependencyObject;
            if (element == null) return TimeSpan.Zero;

            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(element);
            if (itemsControl == null)
            {
                DependencyObject ancestor = element;
                while (ancestor != null && itemsControl == null)
                {
                    ancestor = VisualTreeHelper.GetParent(ancestor);
                    itemsControl = ItemsControl.ItemsControlFromItemContainer(ancestor);
                }
            }
            if (itemsControl == null) return TimeSpan.Zero;

            bool isOwnContainer = itemsControl.IsItemItsOwnContainer(element);
            DependencyObject container = isOwnContainer
                ? element
                : itemsControl.ItemContainerGenerator.ContainerFromItem(element);
            if (container == null)
            {
                object dataContext = (element as FrameworkElement)?.DataContext;
                if (dataContext != null)
                    container = itemsControl.ItemContainerGenerator.ContainerFromItem(dataContext);
            }

            if (container == null) return TimeSpan.Zero;

            int multiplier = itemsControl.ItemContainerGenerator.IndexFromContainer(container);
            if (multiplier == -1) //container generation may not have completed
            {
                multiplier = itemsControl.Items.IndexOf(element);
            }

            if (multiplier == -1) //still not found, repeat now using datacontext
            {
                FrameworkElement frameworkElement = element as FrameworkElement;
                if (frameworkElement != null)
                {
                    multiplier = itemsControl.Items.IndexOf(frameworkElement.DataContext);
                }
            }

            return multiplier > -1 ? new TimeSpan(Unit.Ticks * multiplier) : TimeSpan.Zero;
        }
    }
}