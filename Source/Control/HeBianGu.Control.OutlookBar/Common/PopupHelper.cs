using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.OutlookBar
{
    public static class PopupHelper
    {
        public static bool IsLogicalAncestorOf(this UIElement ancestor, UIElement child)
        {
            if (child != null)
            {
                FrameworkElement obj = child as FrameworkElement;
                while (obj != null)
                {
                    FrameworkElement parent = VisualTreeHelper.GetParent(obj) as FrameworkElement;
                    obj = parent == null ? obj.Parent as FrameworkElement : parent as FrameworkElement;
                    if (obj == ancestor) return true;
                }
            }
            return false;
        }
    }
}
