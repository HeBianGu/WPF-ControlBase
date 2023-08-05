// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

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
                if (parent == null) break;
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

        public static T GetValueSync<T>(this DependencyObject obj, DependencyProperty property)
        {
            if (obj.CheckAccess())
                return (T)obj.GetValue(property);
            else
                return (T)obj.Dispatcher.Invoke(new Func<object>(() => obj.GetValue(property)));
        }

        public static void SetValueSync<T>(this DependencyObject obj, DependencyProperty property, T value)
        {
            if (obj.CheckAccess())
                obj.SetValue(property, value);
            else
                obj.Dispatcher.Invoke(new Action(() => obj.SetValue(property, value)));
        }

        public static BitmapSource GetImage(this UIElement element)
        {
            RenderTargetBitmap targetBitmap = new RenderTargetBitmap(
                                         (int)element.RenderSize.Width,
                                         (int)element.RenderSize.Height,
                                         96d,
                                         96d,
                                         PixelFormats.Default);
            targetBitmap.Render(element);
            return targetBitmap;
        }


        public static IEnumerable<Adorner> GetAdorners(this UIElement element, Predicate<Adorner> predicate = null)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            if (layer == null)
                return null;
            //var sss = layer.GetAdorners(element);
            return layer.GetAdorners(element)?.Where(l => predicate?.Invoke(l) != false);
        }

        public static Adorner GetAdorner(this UIElement element, Predicate<Adorner> predicate = null)
        {
            return element.GetAdorners(predicate)?.FirstOrDefault();
        }

        public static bool AddAdorner(this UIElement element, Adorner adorner)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            if (layer == null)
                return false;
            layer.Add(adorner);
            return true;
        }

        public static bool ClearAdorner(this UIElement element, Predicate<Adorner> predicate = null)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);
            if (layer == null)
                return false;
            var adorners = element.GetAdorners(predicate);
            if (adorners == null)
                return true;
            foreach (var item in adorners)
            {
                layer.Remove(item);
            }
            return true;
        }


        public static T HitTest<T>(this UIElement element, Point point, Predicate<T> predicate = null) where T : DependencyObject
        {
            T result = null;
            HitTestFilterCallback hitTestFilterCallback = x =>
            {
                Type type = x.GetType();
                if (type.Name == element.GetType().Name)
                    return HitTestFilterBehavior.ContinueSkipSelf;
                if (x is T t)
                {
                    if (predicate?.Invoke(t) != false)
                    {
                        result = t;
                    }
                }
                return HitTestFilterBehavior.Continue;
            };

            HitTestResultCallback hitTestResultCallback = x =>
            {
                if (x.VisualHit is T t)
                {
                    if (predicate?.Invoke(t) != false)
                    {
                        return HitTestResultBehavior.Stop;
                    }
                }
                return HitTestResultBehavior.Continue;
            };
            //Point point = Mouse.GetPosition(element);
            PointHitTestParameters parameters = new PointHitTestParameters(point);
            VisualTreeHelper.HitTest(element, hitTestFilterCallback, hitTestResultCallback, parameters);
            return result;
        }

        public static T HitTest<T>(this UIElement element, Point point) where T : DependencyObject
        {
            //Point point = Mouse.GetPosition(element);
            var result = VisualTreeHelper.HitTest(element, point);
            return result.VisualHit as T;
        }

        public static T HitTest<T>(this UIElement element, Geometry geometry, Predicate<T> predicate = null) where T : DependencyObject
        {
            T result = null;
            Point point = Mouse.GetPosition(element);
            PointHitTestParameters parameters = new PointHitTestParameters(point);
            HitTestFilterCallback hitTestFilterCallback = x =>
            {
                Type type = x.GetType();
                if (type.Name == element.GetType().Name)
                    return HitTestFilterBehavior.ContinueSkipSelf;
                if (x is T t)
                {
                    if (predicate?.Invoke(t) != false)
                    {
                        result = t;
                    }
                }
                return HitTestFilterBehavior.Continue;
            };

            HitTestResultCallback hitTestResultCallback = x =>
            {
                if (x.VisualHit is T t)
                {
                    if (predicate?.Invoke(t) != false)
                    {
                        return HitTestResultBehavior.Stop;
                    }
                }
                return HitTestResultBehavior.Continue;
            };
            VisualTreeHelper.HitTest(element, hitTestFilterCallback, hitTestResultCallback, new GeometryHitTestParameters(geometry));
            return result;
        }

        public static object GetDataContext(this UIElement element)
        {
            return element is FrameworkElement framework ? framework.DataContext : null;
        }

        public static object GetContent(this UIElement element)
        {
            if (element is ContentPresenter cp)
                return cp.Content;
            if (element is ContentControl cc)
                return cc.Content;
            if (element is FrameworkElement fe)
                return fe.DataContext;
            return null;
        }

        public static T GetItemsSource<T>(this UIElement element) where T : IEnumerable
        {
            if (element is ItemsControl items)
                return (T)items.ItemsSource;
            return default(T);
        }
    }


    public static class GridExtension
    {
        public static bool HitTestRow(this Grid grid, out int row)
        {
            var p = Mouse.GetPosition(grid);
            System.Diagnostics.Debug.WriteLine(p);
            return grid.HitTestRow(p, out row);
        }

        public static bool HitTestColumn(this Grid grid, out int column)
        {
            var p = Mouse.GetPosition(grid);
            System.Diagnostics.Debug.WriteLine(p);
            return grid.HitTestColumn(p, out column);
        }

        public static bool HitTestRow(this Grid grid, Point point, out int row)
        {
            row = 0;
            if (point.Y < 0)
            {
                row = 0;
                return false;
            }
            if (point.Y > grid.ActualHeight)
            {
                row = grid.RowDefinitions.Count;
                return false;
            }

            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                if (grid.RowDefinitions[i].Offset > point.Y)
                    return true;
                row = i;
            }
            row = grid.RowDefinitions.Count;
            return true;
        }

        public static bool HitTestColumn(this Grid grid, Point point, out int column)
        {
            column = 0;
            if (point.Y < 0)
            {
                column = 0;
                return false;
            }
            if (point.Y > grid.ActualWidth)
            {
                column = grid.ColumnDefinitions.Count;
                return false;
            }

            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                if (grid.ColumnDefinitions[i].Offset > point.X)
                    return true;
                column = i;
            }
            column = grid.ColumnDefinitions.Count;
            return true;
        }

        public static bool HitTestRowColumn(this Grid grid, Point point, out int row, out int column)
        {
            bool r = true;
            if (HitTestRow(grid, point, out row))
                r = false;
            if (HitTestColumn(grid, point, out column))
                r = false;
            return r;
        }
    }
}
