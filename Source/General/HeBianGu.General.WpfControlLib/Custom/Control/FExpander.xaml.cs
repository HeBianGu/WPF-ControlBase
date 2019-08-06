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
    public class FExpander : ContentControl
    {
      
        public static RoutedUICommand ButtonClickCommand = new RoutedUICommand("ButtonClickCommand", "ButtonClickCommand", typeof(FExpander));


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(FExpander), new PropertyMetadata(default(string), (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(FExpander), new PropertyMetadata(null, (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 ImageSource config = e.NewValue as ImageSource;

             }));



        public string IconFont
        {
            get { return (string)GetValue(IconFontProperty); }
            set { SetValue(IconFontProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontProperty =
            DependencyProperty.Register("IconFont", typeof(string), typeof(FExpander), new PropertyMetadata(null, (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));



        public Brush HintForeground
        {
            get { return (Brush)GetValue(HintForegroundProperty); }
            set { SetValue(HintForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintForegroundProperty =
            DependencyProperty.Register("HintForeground", typeof(Brush), typeof(FExpander), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public Brush HintBackground
        {
            get { return (Brush)GetValue(HintBackgroundProperty); }
            set { SetValue(HintBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintBackgroundProperty =
            DependencyProperty.Register("HintBackground", typeof(Brush), typeof(FExpander), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(FExpander), new PropertyMetadata(default(string), (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public bool CanHide
        {
            get { return (bool)GetValue(CanHideProperty); }
            set { SetValue(CanHideProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CanHideProperty =
            DependencyProperty.Register("CanHide", typeof(bool), typeof(FExpander), new PropertyMetadata(default(bool), (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(FExpander), new PropertyMetadata(default(bool), (d, e) =>
             {
                 FExpander control = d as FExpander;

                 if (control == null) return;
                 

                 control.GoToState((bool)e.NewValue ? "Expand" : "Normal");

             }));

        public event EventHandler Click;

        public FExpander()
        {
            //Utility.Refresh(this);

            Loaded += delegate
            {
                if (Content == null)
                {
                    IsExpanded = false;
                }
                else if (!CanHide)
                {
                    IsExpanded = true;
                }
                this.GoToState(IsExpanded ? "StartExpand" : "StartNormal");
            };

            CommandBindings.Add(new CommandBinding(ButtonClickCommand, delegate
            {
                if (CanHide && Content != null)
                {
                    IsExpanded = !IsExpanded;
                }
                if (Click != null) { Click(this, null); }
            }));
        }

        public void Clear()
        {
            Content = new StackPanel();
        }

        public UIElementCollection Children
        {
            get
            {
                if ((Content is StackPanel))
                {
                    return (Content as StackPanel).Children;
                }
                else if ((Content is Grid))
                {
                    return (Content as Grid).Children;
                }
                return null;
            }
        }

        public void Add(FrameworkElement element)
        {
            if (!(Content is StackPanel))
            {
                Content = new StackPanel();
            }
            (Content as StackPanel).Children.Add(element);
        }

        static FExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FExpander), new FrameworkPropertyMetadata(typeof(FExpander)));

        }

        void GoToState(string value)
        {
            VisualStateManager.GoToState(this, value, false);
        }
    }
}
