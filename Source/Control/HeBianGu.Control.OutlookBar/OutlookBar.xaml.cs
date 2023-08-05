using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

#region changelog
// Version 1.3.17
//
// -  do not catch the SizeChanged event in design mode:
//     suggested by maze1610 http://www.codeplex.com/odyssey/WorkItem/View.aspx?WorkItemId=1074
// - renamed Property Dock to DockPosition.
//     suggested by maze1610 http://www.codeplex.com/odyssey/WorkItem/View.aspx?WorkItemId=1075
//     thanx for your help.
#endregion
namespace HeBianGu.Control.OutlookBar
{
    //UNDONE: Section.Content sometimes not visible when IsMaximized has changed. 
    [TemplatePart(Name = partMinimizedButtonContainer)]
    public class OutlookBar : HeaderedItemsControl, IKeyTipControl
    {
        const string partMinimizedButtonContainer = "PART_MinimizedContainer";
        const string partPopup = "PART_Popup";
        static OutlookBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OutlookBar), new FrameworkPropertyMetadata(typeof(OutlookBar)));
        }

        private Collection<OutlookSection> maximizedSections;
        private Collection<OutlookSection> minimizedSections;
        private FrameworkElement minimizedButtonContainer;


        public OutlookBar()
            : base()
        {
            overflowMenu = new Collection<object>();
            SetValue(OutlookBar.OverflowMenuItemsPropertyKey, overflowMenu);
            SetValue(OutlookBar.OptionButtonsPropertyKey, new Collection<ButtonBase>());

            CommandBindings.Add(new CommandBinding(CollapseCommand, CollapseCommandExecuted));
            CommandBindings.Add(new CommandBinding(StartDraggingCommand, StartDraggingCommandExecuted));
            CommandBindings.Add(new CommandBinding(ShowPopupCommand, ShowPopupCommandExecuted));
            CommandBindings.Add(new CommandBinding(ResizeCommand, ResizeCommandExecuted));
            CommandBindings.Add(new CommandBinding(CloseCommand, CloseCommandExecuted));

            maximizedSections = new Collection<OutlookSection>();
            minimizedSections = new Collection<OutlookSection>();
            sections = new ObservableCollection<OutlookSection>();
            sections.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SectionsCollectionChanged);

            // do not catch the SizeChanged event in design mode:
            // suggested by maze1610 http://www.codeplex.com/odyssey/WorkItem/View.aspx?WorkItemId=1074:

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.SizeChanged += new SizeChangedEventHandler(OutlookBar_SizeChanged);
            }
        }

        void OutlookBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ApplySections();
        }



        private void CollapseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            IsMaximized ^= true;
        }

        private void ShowPopupCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsMaximized)
            {
                IsPopupVisible = true;
            }
        }

        private void ResizeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Controls.Control c = e.OriginalSource as System.Windows.Controls.Control;
            if (c != null)
            {
                c.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(DragMouseLeftButtonUp);
            }
            this.PreviewMouseMove += new MouseEventHandler(PreviewMouseMoveResize);
        }

        private void CloseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        void PreviewMouseMoveResize(object sender, MouseEventArgs e)
        {
            System.Windows.Controls.Control c = e.OriginalSource as System.Windows.Controls.Control;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (DockPosition == HorizontalAlignment.Left)
                {
                    ResizeFromRight(e);
                }
                else
                {
                    ResizeFromLeft(e);
                }
            }
            else this.PreviewMouseMove -= PreviewMouseMoveResize;
        }

        private void ResizeFromLeft(MouseEventArgs e)
        {
            Point pos = e.GetPosition(this);
            double w = this.ActualWidth - pos.X;

            if (w < 80)
            {
                w = double.NaN;
                IsMaximized = false;
            }
            else
            {
                IsMaximized = true;
            }
            if (MaxWidth != double.NaN && w > MaxWidth) w = MaxWidth;
            Width = w;
        }
        private void ResizeFromRight(MouseEventArgs e)
        {
            Point pos = e.GetPosition(this);
            double w = pos.X;

            if (w < 80)
            {
                w = double.NaN;
                IsMaximized = false;
            }
            else
            {
                IsMaximized = true;
            }
            if (MaxWidth != double.NaN && w > MaxWidth) w = MaxWidth;
            Width = w;
        }

        private void StartDraggingCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Controls.Control c = e.OriginalSource as System.Windows.Controls.Control;
            if (c != null)
            {
                c.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(DragMouseLeftButtonUp);
            }
            this.PreviewMouseMove += new MouseEventHandler(PreviewMouseMoveButtons);
        }

        /// <summary>
        /// Remove all PreviewMouseMove events from the outlookbar that have been possible set at the beginning of a drag command.
        /// </summary>
        void DragMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Control c = e.OriginalSource as System.Windows.Controls.Control;
            if (c != null)
            {
                c.PreviewMouseLeftButtonUp -= DragMouseLeftButtonUp;
            }
            this.PreviewMouseMove -= PreviewMouseMoveButtons;
            this.PreviewMouseMove -= PreviewMouseMoveResize;
        }

        void PreviewMouseMoveButtons(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point pos = e.GetPosition(this);
                double h = this.ActualHeight - 1 - ButtonHeight - pos.Y;
                MaxNumberOfButtons = (int)(h / ButtonHeight);
            }
            else this.PreviewMouseMove -= PreviewMouseMoveButtons;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ApplySections();
        }



        /// <summary>
        /// Determine the collection of MinimizedSections and MaximizedSections depending on the MaxVisibleButtons Property.
        /// </summary>
        protected virtual void ApplySections()
        {
            if (this.IsInitialized)
            {
                maximizedSections = new Collection<OutlookSection>();
                minimizedSections = new Collection<OutlookSection>();
                int max = MaxNumberOfButtons;
                int index = 0;
                int selectedIndex = SelectedSectionIndex;
                OutlookSection selectedContent = null;

                int n = GetNumberOfMinimizedButtons();

                foreach (OutlookSection e in sections)
                {
                    e.OutlookBar = this;
                    e.Height = ButtonHeight;
                    if (max-- > 0)
                    {
                        e.IsMaximized = true;
                        maximizedSections.Add(e);
                    }
                    else
                    {
                        e.IsMaximized = false;
                        if (minimizedSections.Count < n)
                        {
                            minimizedSections.Add(e);
                        }
                    }
                    bool selected = index++ == selectedIndex;
                    e.IsSelected = selected;
                    if (selected) selectedContent = e;
                }
                SetValue(OutlookBar.MaximizedSectionsPropertyKey, maximizedSections);
                SetValue(OutlookBar.MinimizedSectionsPropertyKey, minimizedSections);
                SelectedSection = selectedContent;
            }

        }


        private Collection<object> overflowMenu;

        /// <summary>
        /// Gets or sets the default items for the overflow menu.
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<object> OverflowMenuItems
        {
            get { return overflowMenu; }
            //    private set { overflowMenu = value; }
        }

        public static readonly DependencyProperty OverflowMenuProperty =
            DependencyProperty.Register("OverflowMenu", typeof(ItemCollection), typeof(OutlookBar), new UIPropertyMetadata(null));




        private void ApplyOverflowMenu()
        {
            Collection<object> overflowItems = new Collection<object>();
            if (OverflowMenuItems.Count > 0)
            {
                foreach (object item in OverflowMenuItems)
                {
                    overflowItems.Add(item);
                }
            }

            bool separatorAdded = false;
            int visibleButtons = maximizedSections.Count + (IsMaximized ? minimizedSections.Count : 0);

            for (int i = visibleButtons; i < sections.Count; i++)
            {
                //if (!separatorAdded)
                //{
                //    overflowItems.Add(new Separator());
                //    separatorAdded = true;
                //}
                OutlookSection section = sections[i];
                MenuItem item = new MenuItem();
                item.Header = section.Header;
                Image image = new Image();
                image.Source = section.Image;
                item.Icon = image;
                item.Icon = Cattach.GetIcon(section);
                item.Tag = section;
                item.Click += new RoutedEventHandler(item_Click);
                overflowItems.Add(item);
            }

            SetValue(OutlookBar.OverflowMenuItemsPropertyKey, overflowItems);
        }





        private int GetNumberOfMinimizedButtons()
        {
            if (minimizedButtonContainer != null)
            {
                const double width = 32;
                const double overflowWidth = 18;
                double fraction = (minimizedButtonContainer.ActualWidth - overflowWidth) / width;
                int minimizedButtons = (int)Math.Truncate(fraction);
                int visibleButtons = MaxNumberOfButtons + minimizedButtons;
                return visibleButtons;
            }
            return 0;
        }

        public event EventHandler<OverflowMenuCreatedEventArgs> OverflowMenuCreated;

        protected virtual void OnOverflowMenuCreated(Collection<object> menuItems)
        {
            if (OverflowMenuCreated != null)
            {
                OverflowMenuCreatedEventArgs e = new OverflowMenuCreatedEventArgs(menuItems);
                OverflowMenuCreated(this, e);
            }
        }

        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.OriginalSource as MenuItem;
            OutlookSection section = item.Tag as OutlookSection;
            this.SelectedSection = section;
        }


        void SectionsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ApplySections();
        }

        private Popup popup;

        public override void OnApplyTemplate()
        {
            if (popup != null)
            {
                popup.Closed -= OnPopupClosed;
                popup.Opened -= OnPopupOpened;
            }
            minimizedButtonContainer = this.GetTemplateChild(partMinimizedButtonContainer) as FrameworkElement;
            popup = this.GetTemplateChild(partPopup) as Popup;
            if (popup != null)
            {
                popup.Closed += new EventHandler(OnPopupClosed);
                popup.Opened += new EventHandler(OnPopupOpened);
            }

            ToggleButton btn = GetTemplateChild("PART_ToggleButton") as ToggleButton;
            if (btn != null)
            {
                btn.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(btn_PreviewMouseLeftButtonUp);
            }

            base.OnApplyTemplate();
        }

        void btn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            popup.StaysOpen = false;
        }



        protected virtual void OnPopupOpened(object sender, EventArgs e)
        {
            IsPopupVisible = true;
            Mouse.Capture(this, CaptureMode.SubTree);
        }

        protected virtual void OnPopupClosed(object sender, EventArgs e)
        {
            IsPopupVisible = false;
            Mouse.Capture(null);
        }



        private ObservableCollection<OutlookSection> sections;

        /// <summary>
        /// Gets the collection of sections.
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<OutlookSection> Sections
        {
            get { return sections; }
            //            private set { sections = value as ObservableCollection<OutlookSection>; }
        }


        private ObservableCollection<OutlookSection> sideButtons = new ObservableCollection<OutlookSection>();

        /// <summary>
        /// Gets the buttons on the side when IsExpanded is set to false and ShowSideButtons is set to true.
        /// </summary>
        public Collection<OutlookSection> SideButtons
        {
            get { return sideButtons; }
        }



        /// <summary>
        /// Gets or sets whether to show the SideButtons when IsExpanded is set to false.
        /// </summary>
        public bool ShowSideButtons
        {
            get { return (bool)GetValue(ShowSideButtonsProperty); }
            set { SetValue(ShowSideButtonsProperty, value); }
        }

        public static readonly DependencyProperty ShowSideButtonsProperty =
            DependencyProperty.Register("ShowSideButtons", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(true));


        /// <summary>
        /// Gets or sets whether the Outlookbar is Maximized or Minimized.
        /// </summary>
        public bool IsMaximized
        {
            get { return (bool)GetValue(IsMaximizedProperty); }
            set { SetValue(IsMaximizedProperty, value); }
        }

        public static readonly DependencyProperty IsMaximizedProperty =
            DependencyProperty.Register("IsMaximized", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(true, MaximizedPropertyChanged));

        private static void MaximizedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlookBar bar = (OutlookBar)d;
            bar.OnMaximizedChanged((bool)e.NewValue);
        }

        private double previousMaxWidth = double.PositiveInfinity;

        /// <summary>
        /// Occurs when the IsMaximized property has changed.
        /// </summary>
        /// <param name="isExpanded"></param>
        protected virtual void OnMaximizedChanged(bool isExpanded)
        {
            if (isExpanded) IsPopupVisible = false;
            EnsureSectionContentIsVisible();

            if (isExpanded)
            {
                MaxWidth = previousMaxWidth;
                RaiseEvent(new RoutedEventArgs(ExpandedEvent));
            }
            else
            {
                previousMaxWidth = MaxWidth;
                MaxWidth = MinimizedWidth + (CanResize ? 4 : 0);
                RaiseEvent(new RoutedEventArgs(CollapsedEvent));
            }
        }



        /// <summary>
        /// Occurs after the OutlookBar has collapsed.
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(OutlookBar.CollapsedEvent, value); }
            remove { RemoveHandler(OutlookBar.CollapsedEvent, value); }
        }

        /// <summary>
        /// Occurs after the OutlookBar has expanded.
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(OutlookBar.ExpandedEvent, value); }
            remove { RemoveHandler(OutlookBar.ExpandedEvent, value); }
        }

        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent("CollapsedEvent",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutlookBar));

        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent("ExpandedEvent",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutlookBar));

        /// <summary>
        /// This code ensures that the section content is visible when the IsExpanded has changed,
        /// since the parent of the content could have changed either.
        /// </summary>
        private void EnsureSectionContentIsVisible()
        {
            object content = SelectedSection != null ? SelectedSection.Content : null;
            SectionContent = null;  // set temporarily to null, so resetting to the current content will have an effect.
            CollapsedSectionContent = IsMaximized ? null : content;
            SectionContent = IsMaximized ? content : null;
        }


        /// <summary>
        /// Gets or sets the width when IsExpanded is set to false.
        /// </summary>
        public double MinimizedWidth
        {
            get { return (double)GetValue(MinimizedWidthProperty); }
            set { SetValue(MinimizedWidthProperty, value); }
        }

        public static readonly DependencyProperty MinimizedWidthProperty =
            DependencyProperty.Register("MinimizedWidth", typeof(double), typeof(OutlookBar), new UIPropertyMetadata((double)32));




        /// <summary>
        /// Gets or sets how to align template of the OutlookBar.
        /// Currently, only Left or Right is supported!
        /// </summary>
        /// <remarks>
        /// This property has been renamed from Dock to DockPosition due to a suggestion from maze6210:
        /// http://www.codeplex.com/odyssey/WorkItem/View.aspx?WorkItemId=1075
        /// </remarks>
        public HorizontalAlignment DockPosition
        {
            get { return (HorizontalAlignment)GetValue(DockPositionProperty); }
            set { SetValue(DockPositionProperty, value); }
        }

        public static readonly DependencyProperty DockPositionProperty =
            DependencyProperty.Register("DockPosition", typeof(HorizontalAlignment), typeof(OutlookBar), new UIPropertyMetadata(HorizontalAlignment.Left));

        private static readonly DependencyPropertyKey MaximizedSectionsPropertyKey =
            DependencyProperty.RegisterReadOnly("MaximizedSections", typeof(Collection<OutlookSection>), typeof(OutlookBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty MaximizedSectionsProperty = MaximizedSectionsPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey MinimizedSectionsPropertyKey =
            DependencyProperty.RegisterReadOnly("MinimizedSections", typeof(Collection<OutlookSection>), typeof(OutlookBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty MinimizedSectionsProperty = MinimizedSectionsPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey OverflowMenuItemsPropertyKey =
            DependencyProperty.RegisterReadOnly("OverflowMenuItems", typeof(Collection<object>), typeof(OutlookBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty OverflowMenuItemsProperty = OverflowMenuItemsPropertyKey.DependencyProperty;


        /// <summary>
        /// Gets or sets how many buttons are completely visible.
        /// </summary>
        public int MaxNumberOfButtons
        {
            get { return (int)GetValue(MaxNumberOfButtonsProperty); }
            set { SetValue(MaxNumberOfButtonsProperty, value); }
        }

        public static readonly DependencyProperty MaxNumberOfButtonsProperty =
            DependencyProperty.Register("MaxNumberOfButtons", typeof(int), typeof(OutlookBar),
            new FrameworkPropertyMetadata(int.MaxValue,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                MaxNumberOfButtonsPropertyChanged));

        private static void MaxNumberOfButtonsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlookBar bar = (OutlookBar)d;
            bar.ApplySections();
        }


        /// <summary>
        /// Gets or sets whether the popup panel is visible.
        /// </summary>
        public bool IsPopupVisible
        {
            get { return (bool)GetValue(IsPopupVisibleProperty); }
            set { SetValue(IsPopupVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsPopupVisibleProperty =
            DependencyProperty.Register("IsPopupVisible", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(false, PopupVisiblePropertyChanged));

        private static void PopupVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlookBar bar = (OutlookBar)d;
            bar.OnPopupVisibleChanged((bool)e.NewValue);
        }

        /// <summary>
        /// Occurs when the IsPopupVisible has changed.
        /// </summary>
        /// <param name="isPopupVisible"></param>
        protected virtual void OnPopupVisibleChanged(bool isPopupVisible)
        {
            if (popup != null)
            {
                popup.StaysOpen = true;
                popup.IsOpen = isPopupVisible;
            }
            if (isPopupVisible)
            {
                RaiseEvent(new RoutedEventArgs(PopupOpenedEvent));
            }
            else
            {
                RaiseEvent(new RoutedEventArgs(PopupClosedEvent));
            }
        }

        /// <summary>
        /// Occurs after the Popup has opened.
        /// </summary>
        public event RoutedEventHandler PopupOpened
        {
            add { AddHandler(OutlookBar.PopupOpenedEvent, value); }
            remove { RemoveHandler(OutlookBar.PopupOpenedEvent, value); }
        }

        /// <summary>
        /// Occurs after the Popup has closed.
        /// </summary>
        public event RoutedEventHandler PopupClosed
        {
            add { AddHandler(OutlookBar.PopupClosedEvent, value); }
            remove { RemoveHandler(OutlookBar.PopupClosedEvent, value); }
        }

        public static readonly RoutedEvent PopupOpenedEvent = EventManager.RegisterRoutedEvent("PopupOpenedEvent",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutlookBar));

        public static readonly RoutedEvent PopupClosedEvent = EventManager.RegisterRoutedEvent("PopupClosedEvent",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutlookBar));

        /// <summary>
        /// Gets or sets the index of the selected section.
        /// </summary>
        public int SelectedSectionIndex
        {
            get { return (int)GetValue(SelectedSectionIndexProperty); }
            set { SetValue(SelectedSectionIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedSectionIndexProperty =
            DependencyProperty.Register("SelectedSectionIndex", typeof(int), typeof(OutlookBar), new
                FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                SelectedIndexPropertyChanged));

        private static void SelectedIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlookBar bar = (OutlookBar)d;
            bar.ApplySections();
        }



        /// <summary>
        /// Gets or sets the selected section.
        /// </summary>
        public OutlookSection SelectedSection
        {
            get { return (OutlookSection)GetValue(SelectedSectionProperty); }
            set { SetValue(SelectedSectionProperty, value); }
        }

        public static readonly DependencyProperty SelectedSectionProperty =
            DependencyProperty.Register("SelectedSection", typeof(OutlookSection), typeof(OutlookBar),
            new UIPropertyMetadata(null, SelectedSectionPropertyChanged));


        private static void SelectedSectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlookBar bar = (OutlookBar)d;
            bar.OnSelectedSectionChanged((OutlookSection)e.OldValue, (OutlookSection)e.NewValue);
        }

        /// <summary>
        /// Occurs when the SelectedSection has changed.
        /// </summary>
        protected virtual void OnSelectedSectionChanged(OutlookSection oldSection, OutlookSection newSection)
        {

            for (int index = 0; index < sections.Count; index++)
            {
                OutlookSection section = sections[index];
                bool selected = newSection == section;
                section.IsSelected = newSection == section;
                if (selected)
                {
                    SelectedSectionIndex = index;
                    SectionContent = IsMaximized ? section.Content : null;
                    CollapsedSectionContent = IsMaximized ? null : section.Content;
                }
            }
            RaiseEvent(new RoutedPropertyChangedEventArgs<OutlookSection>(oldSection, newSection, SelectedSectionChangedEvent));
        }


        /// <summary>
        /// Occurs when the SelectedSection has changed.
        /// </summary>
        public event RoutedPropertyChangedEventHandler<OutlookSection> SelectedSectionChanged
        {
            add { AddHandler(OutlookBar.SelectedSectionChangedEvent, value); }
            remove { RemoveHandler(OutlookBar.SelectedSectionChangedEvent, value); }
        }

        public static readonly RoutedEvent SelectedSectionChangedEvent = EventManager.RegisterRoutedEvent("SelectedSectionChangedEvent",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<OutlookSection>), typeof(OutlookBar));




        /// <summary>
        /// Gets the content of the selected section.
        /// </summary>
        internal object SectionContent
        {
            get { return (object)GetValue(SectionContentProperty); }
            set { SetValue(SectionContentPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey SectionContentPropertyKey =
            DependencyProperty.RegisterReadOnly("SectionContent", typeof(object), typeof(OutlookBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty SectionContentProperty = SectionContentPropertyKey.DependencyProperty;




        /// <summary>
        /// Gets or sets the content for the popup.
        /// </summary>
        internal object CollapsedSectionContent
        {
            get { return (object)GetValue(CollapsedSectionContentProperty); }
            set { SetValue(CollapsedSectionContentPropertyKey, value); }
        }


        private static readonly DependencyPropertyKey CollapsedSectionContentPropertyKey =
            DependencyProperty.RegisterReadOnly("CollapsedSectionContent", typeof(object), typeof(OutlookBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty CollapsedSectionContentProperty = SectionContentPropertyKey.DependencyProperty;



        /// <summary>
        /// Gets or sets whether the overflow menu of the available sections is visible.
        /// </summary>
        public bool IsOverflowVisible
        {
            get { return (bool)GetValue(IsOverflowVisibleProperty); }
            set { SetValue(IsOverflowVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsOverflowVisibleProperty =
            DependencyProperty.Register("IsOverflowVisible", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(false, OverflowVisiblePropertyChanged));


        private static void OverflowVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlookBar bar = (OutlookBar)d;
            bool newValue = (bool)e.NewValue;
            bar.OnOverflowVisibleChanged(newValue);
        }

        /// <summary>
        /// Occurs when the IsOverflowVisible has changed.
        /// </summary>
        /// <param name="newValue"></param>
        protected virtual void OnOverflowVisibleChanged(bool newValue)
        {

            if (newValue == true)
            {
                ApplyOverflowMenu();
            }
        }

        /// <summary>
        /// Toggles the IsExpanded property.
        /// </summary>
        public static RoutedUICommand CollapseCommand
        {
            get { return collapseCommand; }
        }

        /// <summary>
        /// Starts dragging the splitter for the visible section buttons (used for the xaml template).
        /// </summary>
        public static RoutedUICommand StartDraggingCommand
        {
            get { return startDraggingCommand; }
        }

        /// <summary>
        /// Shows the popup window.
        /// </summary>
        public static RoutedUICommand ShowPopupCommand
        {
            get { return showPopupCommand; }
        }

        /// <summary>
        /// Start to resize the Width of the OutlookBar (used for the xaml template to initiate resizing).
        /// </summary>
        public static RoutedUICommand ResizeCommand
        {
            get { return resizeCommand; }
        }
        private static RoutedUICommand resizeCommand = new RoutedUICommand("Resize", "ResizeCommand", typeof(OutlookBar));

        /// <summary>
        /// Close the OutlookBar
        /// </summary>
        public static RoutedUICommand CloseCommand
        {
            get { return closeCommand; }
        }
        private static RoutedUICommand collapseCommand = new RoutedUICommand("Collapse", "CollapseCommand", typeof(OutlookBar));
        private static RoutedUICommand startDraggingCommand = new RoutedUICommand("Drag", "StartDraggingCommand", typeof(OutlookBar));
        private static RoutedUICommand showPopupCommand = new RoutedUICommand("ShowPopup", "ShowPopupCommand", typeof(OutlookBar));
        private static RoutedUICommand closeCommand = new RoutedUICommand("Close", "CloseCommand", typeof(OutlookBar));


        /// <summary>
        /// Gets or sets the height of the section buttons.
        /// </summary>
        public double ButtonHeight
        {
            get { return (double)GetValue(ButtonHeightProperty); }
            set { SetValue(ButtonHeightProperty, value); }
        }

        public static readonly DependencyProperty ButtonHeightProperty =
            DependencyProperty.Register("ButtonHeight", typeof(double), typeof(OutlookBar), new UIPropertyMetadata((double)28.0));





        /// <summary>
        /// Gets or sets the with of the popup window.
        /// </summary>
        public double PopupWidth
        {
            get { return (double)GetValue(PopupWidthProperty); }
            set { SetValue(PopupWidthProperty, value); }
        }

        public static readonly DependencyProperty PopupWidthProperty =
            DependencyProperty.Register("PopupWidth", typeof(double), typeof(OutlookBar), new UIPropertyMetadata((double)double.NaN));




        /// <summary>
        /// Gets or sets whether the splitter for the section buttons is visible
        /// </summary>
        public bool IsButtonSplitterVisible
        {
            get { return (bool)GetValue(IsButtonSplitterVisibleProperty); }
            set { SetValue(IsButtonSplitterVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsButtonSplitterVisibleProperty =
            DependencyProperty.Register("IsButtonSplitterVisible", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(true));


        /// <summary>
        /// Gets or sets whether the section buttons are visible.
        /// </summary>
        public bool ShowButtons
        {
            get { return (bool)GetValue(ShowButtonsProperty); }
            set { SetValue(ShowButtonsProperty, value); }
        }

        public static readonly DependencyProperty ShowButtonsProperty =
            DependencyProperty.Register("ShowButtons", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(true));


        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<ButtonBase> OptionButtons
        {
            get { return (Collection<ButtonBase>)GetValue(OptionButtonsProperty); }
            //  private set { SetValue(OptionButtonsProperty, value); }
        }

        private static readonly DependencyPropertyKey OptionButtonsPropertyKey =
            DependencyProperty.RegisterReadOnly("OptionButtons", typeof(Collection<ButtonBase>), typeof(OutlookBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty OptionButtonsProperty = OptionButtonsPropertyKey.DependencyProperty;




        /// <summary>
        /// Gets or sets wether the width of the OutlookBar can be manually resized by a gripper at the right (or left).
        /// </summary>
        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(true));



        /// <summary>
        /// Gets or sets wether the close button is visible.
        /// </summary>
        public bool IsCloseButtonVisible
        {
            get { return (bool)GetValue(IsCloseButtonVisibleProperty); }
            set { SetValue(IsCloseButtonVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsCloseButtonVisibleProperty =
            DependencyProperty.Register("IsCloseButtonVisible", typeof(bool), typeof(OutlookBar), new UIPropertyMetadata(false));



        /// <summary>
        /// Gets or sets the text or content that is displayed on the minimized OutlookBar at the Button to open up the Navigation Pane.
        /// </summary>
        public object NavigationPaneText
        {
            get { return (object)GetValue(NavigationPaneTextProperty); }
            set { SetValue(NavigationPaneTextProperty, value); }
        }

        public static readonly DependencyProperty NavigationPaneTextProperty =
            DependencyProperty.Register("NavigationPaneText", typeof(object), typeof(OutlookBar), new UIPropertyMetadata("Navigation Pane"));


        protected override IEnumerator LogicalChildren
        {
            get
            {
                return GetLogicalChildren().GetEnumerator();
            }
        }

        protected virtual IEnumerable GetLogicalChildren()
        {
            foreach (var section in Sections) yield return section;
            if (SelectedSection != null) yield return SelectedSection.Content;
        }


        #region IKeyTipControl Members

        void IKeyTipControl.ExecuteKeyTip()
        {
            this.IsMaximized ^= true;
        }

        #endregion
    }
}
