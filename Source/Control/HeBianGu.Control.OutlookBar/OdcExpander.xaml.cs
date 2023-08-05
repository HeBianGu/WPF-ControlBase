
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

#region Licence
// Copyright (c) 2008 Thomas Gerber
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
// to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS
// BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
#endregion
namespace HeBianGu.Control.OutlookBar
{
    /// <summary>
    /// An Expander with animation.
    /// </summary>
    public class OdcExpander : HeaderedContentControl, IKeyTipControl
    {
        static OdcExpander()
        {
            MarginProperty.OverrideMetadata(
                typeof(OdcExpander),
                new FrameworkPropertyMetadata(new Thickness(10, 10, 10, 2)));

            FocusableProperty.OverrideMetadata(typeof(OdcExpander),
                new FrameworkPropertyMetadata(false));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(OdcExpander),
                new FrameworkPropertyMetadata(typeof(OdcExpander)));
        }


        public Brush HeaderBorderBrush
        {
            get { return (Brush)GetValue(HeaderBorderBrushProperty); }
            set { SetValue(HeaderBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty HeaderBorderBrushProperty =
            DependencyProperty.Register("HeaderBorderBrush",
            typeof(Brush), typeof(OdcExpander), new UIPropertyMetadata(Brushes.Gray));

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground",
            typeof(Brush), typeof(OdcExpander), new UIPropertyMetadata(Brushes.Silver));


        public bool IsMinimized
        {
            get { return (bool)GetValue(IsMinimizedProperty); }
            set { SetValue(IsMinimizedProperty, value); }
        }

        public static readonly DependencyProperty IsMinimizedProperty =
            DependencyProperty.Register("IsMinimized",
            typeof(bool), typeof(OdcExpander),
            new UIPropertyMetadata(false, OnMinimizedPropertyChanged));

        public static void OnMinimizedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bool minimized = (bool)e.NewValue;
            OdcExpander expander = d as OdcExpander;
            RoutedEventArgs args = new RoutedEventArgs(minimized ? MinimizedEvent : MaximizedEvent);
            expander.IsEnabled = !minimized;
            expander.RaiseEvent(args);
        }


        /// <summary>
        /// Gets or sets the ImageSource for the image in the header.
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(OdcExpander), new UIPropertyMetadata(null));



        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }

        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        public event RoutedEventHandler Minimized
        {
            add { AddHandler(MinimizedEvent, value); }
            remove { RemoveHandler(MinimizedEvent, value); }
        }

        public event RoutedEventHandler Maximized
        {
            add { AddHandler(MaximizedEvent, value); }
            remove { RemoveHandler(MaximizedEvent, value); }
        }

        #region dependency properties and routed events definition

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
            "IsExpanded",
            typeof(bool),
            typeof(OdcExpander),
            new UIPropertyMetadata(true, IsExpandedChanged));


        public static void IsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcExpander expander = d as OdcExpander;
            RoutedEventArgs args = new RoutedEventArgs((bool)e.NewValue ? ExpandedEvent : CollapsedEvent);
            expander.RaiseEvent(args);
        }

        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent(
            "ExpandedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcExpander));


        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent(
            "CollapsedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcExpander));

        public static readonly RoutedEvent MinimizedEvent = EventManager.RegisterRoutedEvent(
             "MinimizedEvent",
             RoutingStrategy.Bubble,
             typeof(RoutedEventHandler),
             typeof(OdcExpander));


        public static readonly RoutedEvent MaximizedEvent = EventManager.RegisterRoutedEvent(
            "MaximizedEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(OdcExpander));

        #endregion


        /// <summary>
        /// Gets or sets the corner radius for the header.
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(OdcExpander), new UIPropertyMetadata(null));



        /// <summary>
        /// Gets or sets the background color of the header on mouse over.
        /// </summary>
        public Brush MouseOverHeaderBackground
        {
            get { return (Brush)GetValue(MouseOverHeaderBackgroundProperty); }
            set { SetValue(MouseOverHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverHeaderBackgroundProperty =
            DependencyProperty.Register("MouseOverHeaderBackground", typeof(Brush), typeof(OdcExpander), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets whether the PressedBackground is not null.
        /// </summary>
        public bool HasPressedBackground
        {
            get { return (bool)GetValue(HasPressedBackgroundProperty); }
            set { SetValue(HasPressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HasPressedBackgroundProperty =
            DependencyProperty.Register("HasPressedBackground", typeof(bool), typeof(OdcExpander), new UIPropertyMetadata(false));



        /// <summary>
        /// Gets or sets the background color of the header in pressed mode.
        /// </summary>
        public Brush PressedHeaderBackground
        {
            get { return (Brush)GetValue(PressedHeaderBackgroundProperty); }
            set { SetValue(PressedHeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedHeaderBackgroundProperty =
            DependencyProperty.Register("PressedHeaderBackground", typeof(Brush), typeof(OdcExpander),
            new UIPropertyMetadata(null, PressedHeaderBackgroundPropertyChangedCallback));


        public static void PressedHeaderBackgroundPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OdcExpander expander = (OdcExpander)d;
            expander.HasPressedBackground = e.NewValue != null;
        }

        public Thickness HeaderBorderThickness
        {
            get { return (Thickness)GetValue(HeaderBorderThicknessProperty); }
            set { SetValue(HeaderBorderThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderBorderThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBorderThicknessProperty =
            DependencyProperty.Register("HeaderBorderThickness", typeof(Thickness), typeof(OdcExpander), new UIPropertyMetadata(null));




        /// <summary>
        /// Gets or sets the foreground color of the header on mouse over.
        /// </summary>
        public Brush MouseOverHeaderForeground
        {
            get { return (Brush)GetValue(MouseOverHeaderForegroundProperty); }
            set { SetValue(MouseOverHeaderForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverHeaderForegroundProperty =
            DependencyProperty.Register("MouseOverHeaderForeground", typeof(Brush), typeof(OdcExpander), new UIPropertyMetadata(null));



        /// <summary>
        /// Specifies whether to show a elipse with the expanded/collapsed image.
        /// </summary>
        public bool ShowEllipse
        {
            get { return (bool)GetValue(ShowEllipseProperty); }
            set { SetValue(ShowEllipseProperty, value); }
        }

        public static readonly DependencyProperty ShowEllipseProperty =
            DependencyProperty.Register("ShowEllipse", typeof(bool), typeof(OdcExpander), new UIPropertyMetadata(false));




        /// <summary>
        /// Gets or sets whether animation is possible
        /// </summary>
        public bool CanAnimate
        {
            get { return (bool)GetValue(CanAnimateProperty); }
            set { SetValue(CanAnimateProperty, value); }
        }

        public static readonly DependencyProperty CanAnimateProperty =
            DependencyProperty.Register("CanAnimate", typeof(bool), typeof(OdcExpander), new UIPropertyMetadata(true));




        public bool IsHeaderVisible
        {
            get { return (bool)GetValue(IsHeaderVisibleProperty); }
            set { SetValue(IsHeaderVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsHeaderVisibleProperty =
            DependencyProperty.Register("IsHeaderVisible", typeof(bool), typeof(OdcExpander), new UIPropertyMetadata(true));



        #region IKeyTipControl Members

        void IKeyTipControl.ExecuteKeyTip()
        {
            this.IsExpanded ^= true;
            if (this.IsExpanded)
            {
                FrameworkElement e = Content as FrameworkElement;
                if (e != null && e.Focusable) e.Focus();
            }
        }

        #endregion
    }
}
