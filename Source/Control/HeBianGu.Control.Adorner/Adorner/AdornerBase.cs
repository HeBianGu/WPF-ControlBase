// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Adorner
{
    public abstract class AdornerBase : System.Windows.Documents.Adorner
    {
        public AdornerBase(UIElement adornedElement) : base(adornedElement)
        {

        }

        public static Pen GetPen(DependencyObject obj)
        {
            return (Pen)obj.GetValue(PenProperty);
        }

        public static void SetPen(DependencyObject obj, Pen value)
        {
            obj.SetValue(PenProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty PenProperty =
            DependencyProperty.RegisterAttached("Pen", typeof(Pen), typeof(AdornerBase), new PropertyMetadata(new Pen(Brushes.DeepSkyBlue, 1), OnPenChanged));

        static public void OnPenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Pen n = (Pen)e.NewValue;

            Pen o = (Pen)e.OldValue;
        }


        public static Brush GetFill(DependencyObject obj)
        {
            return (Brush)obj.GetValue(FillProperty);
        }

        public static void SetFill(DependencyObject obj, Brush value)
        {
            obj.SetValue(FillProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.RegisterAttached("Fill", typeof(Brush), typeof(AdornerBase), new PropertyMetadata(default(Brush), OnFillChanged));

        static public void OnFillChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Brush n = (Brush)e.NewValue;

            Brush o = (Brush)e.OldValue;
        }




    }
}
