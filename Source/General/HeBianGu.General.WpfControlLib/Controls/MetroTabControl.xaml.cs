using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// AnimatedTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class MetroTabControl : TabControl
    {
        public bool IconMode
        {
            get { return (bool)GetValue(IconModeProperty); }
            set { SetValue(IconModeProperty, value); }
        }

        public static readonly DependencyProperty IconModeProperty =
            DependencyProperty.Register("IconMode", typeof(bool), typeof(MetroTabControl), new PropertyMetadata(default(bool), (d, e) =>
            {
                MetroTabControl control = d as MetroTabControl;

                if (control == null) return;

            }));


        public VerticalAlignment TabPanelVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(TabPanelVerticalAlignmentProperty); }
            set { SetValue(TabPanelVerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabPanelVerticalAlignmentProperty =
            DependencyProperty.Register("TabPanelVerticalAlignment", typeof(VerticalAlignment), typeof(MetroTabControl), new PropertyMetadata(VerticalAlignment.Top, (d, e) =>
            {
                MetroTabControl control = d as MetroTabControl;

                if (control == null) return;

                //VerticalAlignment config = e.NewValue as VerticalAlignment;

            }));



        public Thickness Offset
        {
            get { return (Thickness)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
            DependencyProperty.Register("Offset", typeof(Thickness), typeof(MetroTabControl), new PropertyMetadata(new Thickness(0), (d, e) =>
            {
                MetroTabControl control = d as MetroTabControl;

                if (control == null) return;

                //Thickness config = e.NewValue as Thickness;

            }));



        public static RoutedUICommand IconModeClickCommand = new RoutedUICommand("IconModeClickCommand", "IconModeClickCommand", typeof(MetroTabControl));

        void GoToState()
        {
            VisualStateManager.GoToState(this, IconMode ? "EnterIconMode" : "ExitIconMode", false);

        }
        void GoToState(string value)
        {
            VisualStateManager.GoToState(this, value, false);
        }

        void SelectionState()
        {
            if (IconMode)
            {
                this.GoToState("SelectionStartIconMode");
                this.GoToState("SelectionEndIconMode");
            }
            else
            {
                this.GoToState("SelectionStart");
                this.GoToState("SelectionEnd");
            }
        }

        public MetroTabControl()
        {
            Loaded += delegate
            {
                GoToState();

                VisualStateManager.GoToState(this, IconMode ? "SelectionLoadedIconMode" : "SelectionLoaded", false);
            };


            SelectionChanged += delegate (object sender, SelectionChangedEventArgs e)
            {
                if (e.Source is MetroTabControl)
                {
                    SelectionState();
                }
            };


            CommandBindings.Add(new CommandBinding(IconModeClickCommand, delegate { IconMode = !IconMode; GoToState(); }));

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (this.IsLoaded)
                {
                    SetColor(this);
                }
                else
                {
                    this.Loaded += delegate { SetColor(this); };
                }
            }
        }

        static void SetColor(FrameworkElement control)
        {
            //var mw = Window.GetWindow(control) is MetroWindow ? Window.GetWindow(control) as MetroWindow : null;
            //if (mw != null)
            //{
            //    if (control is MetroTitleMenu) { (control as MetroTitleMenu).Background = mw.BorderBrush; }
            //    if (control is MetroTitleMenuItem) { (control as MetroTitleMenuItem).Background = mw.BorderBrush; }
            //    if (control is MetroMenuItem) { (control as MetroMenuItem).Background = mw.BorderBrush; }
            //    if (control is MetroContextMenu) { (control as MetroContextMenu).Background = mw.BorderBrush; }
            //    if (control is MetroTextBox) { (control as MetroTextBox).BorderBrush = mw.BorderBrush; }
            //    if (control is MetroButton) { (control as MetroButton).Background = mw.BorderBrush; }
            //    if (control is MetroMenuTabControl) { (control as MetroMenuTabControl).BorderBrush = mw.BorderBrush; }
            //    if (control is MetroRichTextBox) { (control as MetroRichTextBox).MouseMoveThemeBorderBrush = mw.BorderBrush; }
            //    if (control is MetroCanvasGrid) { if ((control as MetroCanvasGrid).IsApplyTheme) (control as MetroCanvasGrid).Background = new RgbaColor(mw.BorderBrush).OpaqueSolidColorBrush; }
            //    if (control is MetroColorPicker) { (control as MetroColorPicker).BorderBrush = mw.BorderBrush; }
            //}
        }

        static MetroTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTabControl), new FrameworkPropertyMetadata(typeof(MetroTabControl)));

        }
    }
}
