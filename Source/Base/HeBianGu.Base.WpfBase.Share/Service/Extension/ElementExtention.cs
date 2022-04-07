// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static class ElementExtention
    {
        public static void BindCommand(this UIElement @ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            CommandBinding bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            @ui.CommandBindings.Add(bind);
        }
        public static void BindCommand(this UIElement @ui, ICommand com)
        {
            CommandBinding bind = new CommandBinding(com);
            @ui.CommandBindings.Add(bind);
        }

        public static void Visible(this UIElement @ui)
        {
            @ui.Visibility = Visibility.Visible;
        }

        public static void VisibilityWith(this UIElement @ui, bool from)
        {
            if (from)
            {
                @ui.Visible();
            }
            else
            {
                ui.Collapsed();
            }
        }

        public static void Hidden(this UIElement @ui)
        {
            @ui.Visibility = Visibility.Hidden;
        }

        public static void Collapsed(this UIElement @ui)
        {
            @ui.Visibility = Visibility.Collapsed;
        }

        public static T GetChild<T>(this DependencyObject p_element, Func<T, bool> p_func = null) where T : UIElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
            {
                UIElement child = VisualTreeHelper.GetChild(p_element, i) as FrameworkElement;

                if (child == null)
                {
                    continue;
                }

                T t = child as T;

                if (t != null && (p_func == null || p_func(t)))
                {
                    return (T)child;
                }

                T grandChild = child.GetChild(p_func);
                if (grandChild != null)
                    return grandChild;
            }

            return null;
        }

        public static IEnumerable<T> GetChildren<T>(this DependencyObject p_element, Func<T, bool> p_func = null, bool filterContain = false) where T : UIElement
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
                    T t = (T)child;

                    foreach (T c in child.GetChildren(p_func, filterContain))
                    {
                        yield return c;
                    }

                    if (p_func != null && !p_func(t))
                    {
                        if (filterContain)
                        {
                            yield return t;
                        }
                        continue;
                    }

                    yield return t;
                }

                else
                {
                    foreach (T c in child.GetChildren(p_func, filterContain))
                    {
                        yield return c;
                    }
                }
            }
        }

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

        public static T GetTemplateByName<T>(this Control control, string name) where T : FrameworkElement
        {
            ControlTemplate template = control.Template;
            if (template != null)
            {
                return template.FindName(name, control) as T;
            }

            return null;
        }

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
                    foreach (T item in GetElements<T>(VisualTreeHelper.GetChild(element, i) as FrameworkElement))
                    {
                        yield return item;
                    }
                }

                Popup popup = element as Popup;

                if (popup != null)
                {
                    foreach (T item in GetElements<T>(popup.Child as FrameworkElement))
                    {
                        yield return item;
                    }
                }
            }
        }

        public static VisualStateGroup TryGetVisualStateGroup(this DependencyObject d, string groupName)
        {
            FrameworkElement root = GetImplementationRoot(d);
            if (root == null)
            {
                return null;
            }

            return VisualStateManager
                .GetVisualStateGroups(root)?
                .OfType<VisualStateGroup>()
                .FirstOrDefault(group => string.CompareOrdinal(groupName, group.Name) == 0);
        }

        public static FrameworkElement GetImplementationRoot(this DependencyObject d)
        {
            return 1 == VisualTreeHelper.GetChildrenCount(d)
                ? VisualTreeHelper.GetChild(d, 0) as FrameworkElement
                : null;
        }

        public static IEnumerable<DependencyObject> PrintVisualTree(this DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                List<DependencyObject> from = PrintVisualTree(VisualTreeHelper.GetChild(obj, i)).ToList();

                foreach (DependencyObject item in from)
                {
                    yield return item;
                }
            }

            yield return obj;
        }


        public static IEnumerable<DependencyObject> PrintLogicalTree(this DependencyObject obj)
        {
            foreach (object v in LogicalTreeHelper.GetChildren(obj))
            {
                if (v is DependencyObject)
                {
                    IEnumerable<DependencyObject> from = PrintLogicalTree(v as DependencyObject);


                    foreach (DependencyObject item in from)
                    {
                        yield return item;
                    }
                }
            }

            yield return obj;
        }


        public static IEnumerable<T> FindAllVisualChild<T>(this DependencyObject obj, Predicate<T> match) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                {
                    if (match(child as T))
                    {
                        yield return (T)child;
                    }
                }

                IEnumerable<T> from = FindAllVisualChild<T>(child, match);

                foreach (T item in from)
                {
                    yield return item;
                }
            }
        }


        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject dependencyObject)
        {
            DependencyObject parent = dependencyObject;
            while (true)
            {
                parent = GetParent(parent);
                if (parent != null)
                {
                    yield return parent;
                }
                else
                {
                    break;
                }
            }
        }

        public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }

            DependencyObject parent = dependencyObject;
            while (true)
            {
                if (parent != null)
                {
                    yield return parent;
                }
                else
                {
                    break;
                }
                parent = GetParent(parent);
            }
        }

        public static DependencyObject GetParent(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }

            ContentElement ce = dependencyObject as ContentElement;
            if (ce != null)
            {
                DependencyObject parent = ContentOperations.GetParent(ce);
                if (parent != null)
                {
                    return parent;
                }

                FrameworkContentElement fce = ce as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            return VisualTreeHelper.GetParent(dependencyObject);
        }

        public static DependencyObject GetParent(this DependencyObject fe, Type lookForType)
        {
            fe = VisualTreeHelper.GetParent(fe);
            while (fe != null)
            {
                if (lookForType.IsInstanceOfType(fe))
                    return fe;

                fe = VisualTreeHelper.GetParent(fe);
            }
            return null;
        }
    }
}
