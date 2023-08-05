
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Control.OutlookBar
{
    public class OutlookSection : HeaderedContentControl, IKeyTipControl
    {
        static OutlookSection()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OutlookSection), new FrameworkPropertyMetadata(typeof(OutlookSection)));
        }

        public OutlookSection() : base()
        {
            AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(buttonClickedEvent));
        }

        private void buttonClickedEvent(object sender, RoutedEventArgs e)
        {
            OutlookBar bar = OutlookBar;
            ToggleButton b = e.OriginalSource as ToggleButton;
            if (b != null) b.IsChecked = true;
            if (bar != null)
            {
                bar.SelectedSection = this;
            }
            OnClick();
        }

        /// <summary>
        /// Gets or sets the Image for the Section Button.
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(OutlookSection), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets  wether the Section is the selected section of the OutlookBar.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            internal set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(OutlookSection), new UIPropertyMetadata(false, IsSelectedPropertyChanged));

        private static void IsSelectedPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((OutlookSection)o).OnSelectedPropertyChanged((bool)e.OldValue, (bool)e.NewValue);
        }

        protected virtual void OnSelectedPropertyChanged(bool oldValue, bool newValue)
        {
            if (newValue) OutlookBar.SelectedSection = this;
        }

        /// <summary>
        /// Occurs when the section button is clicked,.
        /// </summary>
        protected virtual void OnClick()
        {
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        /// <summary>
        /// Occurs when the section button is clicked,.
        /// </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutlookSection));




        /// <summary>
        /// Gets or sets whether the Section is shown as a complete button with text (true), otherwise as small button with image only.
        /// </summary>
        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            set { SetValue(IsMaximizedProperty, value); }
        }

        public static readonly DependencyProperty IsMaximizedProperty =
            DependencyProperty.Register("IsMaximized", typeof(bool), typeof(OutlookSection), new UIPropertyMetadata(true));

        /// <summary>
        /// Gets or sets the OutlookBar to which this Section is assigned.
        /// </summary>
        internal OutlookBar OutlookBar { get; set; }

        #region IKeyTipControl Members

        void IKeyTipControl.ExecuteKeyTip()
        {
            IsSelected = true;
        }

        #endregion
    }
}
