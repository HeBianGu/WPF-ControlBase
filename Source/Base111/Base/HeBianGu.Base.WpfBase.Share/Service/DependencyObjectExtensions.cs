using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// Extension methods for <see cref="DependencyObject"/>.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Get the first elment with the specific type and condition under the current element in visual tree.
        /// </summary>
        /// <typeparam name="T"/><peparam/>
        /// <param name="p_element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static T GetChild<T>(this DependencyObject p_element, Func<T, bool> p_func = null) where T : UIElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
            {
                UIElement child = VisualTreeHelper.GetChild(p_element, i) as FrameworkElement;

                if (child == null)
                {
                    continue;
                }

                var t = child as T;

                if (t != null && (p_func == null || p_func(t)))
                {
                    return (T)child;
                }

                var grandChild = child.GetChild(p_func);
                if (grandChild != null)
                    return grandChild;
            }

            return null;
        }

        /// <summary>
        /// Get all the sub item with the specific type and condition
        /// </summary>
        /// <typeparam name="T"/><peparam/>
        /// <param name="p_element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetChildren<T>(this DependencyObject p_element, Func<T, bool> p_func = null) where T : UIElement
        { 
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
            {
                UIElement child = VisualTreeHelper.GetChild(p_element, i) as FrameworkElement;

                if (child == null)
                {
                    continue;
                }

                if (child is T)
                {
                    var t = (T)child;

                    if (p_func != null && !p_func(t))
                    {
                        continue;
                    }

                    foreach (var c in child.GetChildren(p_func))
                    {
                        yield return c;
                    }

                    yield return t;
                }
                else
                {
                    foreach (var c in child.GetChildren(p_func))
                    {
                        yield return c;
                    }
                }
            }
        }

        /// <summary>
        /// Get the parent element matched the given type on the visual tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T GetParent<T>(this DependencyObject element) where T : DependencyObject
        {
            if (element == null) return null;
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while ((parent != null) && !(parent is T))
            {
                DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
                if (newVisualParent != null)
                {
                    parent = newVisualParent;
                }
                else
                {
                    // try to get the logical parent ( e.g. if in Popup)
                    if (parent is FrameworkElement)
                    {
                        parent = (parent as FrameworkElement).Parent;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return parent as T;
        }

        /// <summary>
        /// Get the parent element matched the given type on the visual tree by the specific condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static T GetParent<T>(this DependencyObject element, Func<T, bool> p_func) where T : DependencyObject
        {
            if (element == null) return null;
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while (((parent != null) && !(parent is T)) || !p_func(parent as T))
            {
                DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
                if (newVisualParent != null)
                {
                    parent = newVisualParent;
                }
                else
                {
                    // try to get the logical parent ( e.g. if in Popup)
                    if (parent is FrameworkElement)
                    {
                        parent = (parent as FrameworkElement).Parent;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return parent as T;
        }

        /// <summary>
        /// Returns the template element of the given name within the Control.
        /// </summary>
        public static T GetTemplateByName<T>(this Control control, string name) where T : FrameworkElement
        {
            ControlTemplate template = control.Template;
            if (template != null)
            {
                return template.FindName(name, control) as T;
            }

            return null;
        }

        /// <summary>
        /// Searches the subtree of an element (including that element) 
        /// for an element of a particluar type.
        /// </summary>
        public static T GetElement<T>(this DependencyObject element) where T : FrameworkElement
        {
            T correctlyTyped = element as T;

            if (correctlyTyped != null)
            {
                return correctlyTyped;
            }

            if (element != null)
            {
                int numChildren = VisualTreeHelper.GetChildrenCount(element);
                for (int i = 0; i < numChildren; i++)
                {
                    T child = GetElement<T>(VisualTreeHelper.GetChild(element, i) as FrameworkElement);
                    if (child != null)
                    {
                        return child;
                    }
                }

                // Popups continue in another window, jump to that tree
                Popup popup = element as Popup;

                if (popup != null)
                {
                    return GetElement<T>(popup.Child as FrameworkElement);
                }
            }

            return null;
        }


        public static IEnumerable<T> GetElements<T>(this DependencyObject element) where T : FrameworkElement
        {
            T correctlyTyped = element as T;

            if (correctlyTyped != null)
            {
               yield return correctlyTyped;
            }

            if (element != null)
            {
                int numChildren = VisualTreeHelper.GetChildrenCount(element);

                for (int i = 0; i < numChildren; i++)
                {
                    foreach (var item in GetElements<T>(VisualTreeHelper.GetChild(element, i) as FrameworkElement))
                    {
                        yield return item;
                    }
                }

                // Popups continue in another window, jump to that tree
                Popup popup = element as Popup;

                if (popup != null)
                {
                    foreach (var item in GetElements<T>(popup.Child as FrameworkElement))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static VisualStateGroup TryGetVisualStateGroup(this DependencyObject d, string groupName)
        {
            var root = GetImplementationRoot(d);
            if (root == null)
            {
                return null;
            }

            return VisualStateManager
                .GetVisualStateGroups(root)?
                .OfType<VisualStateGroup>()
                .FirstOrDefault(group => string.CompareOrdinal(groupName, group.Name) == 0);
        }

        internal static FrameworkElement GetImplementationRoot(DependencyObject d)
        {
            return 1 == VisualTreeHelper.GetChildrenCount(d)
                ? VisualTreeHelper.GetChild(d, 0) as FrameworkElement
                : null;
        }
    }
}