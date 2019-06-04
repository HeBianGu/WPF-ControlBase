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
    public class MetroTextBox : TextBox
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MetroTextBox), new PropertyMetadata(default(CornerRadius), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 //CornerRadius config = e.NewValue as CornerRadius;

             }));


        public bool IsPassWordBox
        {
            get { return (bool)GetValue(IsPassWordBoxProperty); }
            set { SetValue(IsPassWordBoxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPassWordBoxProperty =
            DependencyProperty.Register("IsPassWordBox", typeof(bool), typeof(MetroTextBox), new PropertyMetadata(default(bool), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        public bool MultipleLine
        {
            get { return (bool)GetValue(MultipleLineProperty); }
            set { SetValue(MultipleLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MultipleLineProperty =
            DependencyProperty.Register("MultipleLine", typeof(bool), typeof(MetroTextBox), new PropertyMetadata(default(bool), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(string), typeof(MetroTextBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string IconFont
        {
            get { return (string)GetValue(IconFontProperty); }
            set { SetValue(IconFontProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconFontProperty =
            DependencyProperty.Register("IconFont", typeof(string), typeof(MetroTextBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));



        public string ButtonTitle
        {
            get { return (string)GetValue(ButtonTitleProperty); }
            set { SetValue(ButtonTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTitleProperty =
            DependencyProperty.Register("ButtonTitle", typeof(string), typeof(MetroTextBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));



        public string PopupHint
        {
            get { return (string)GetValue(PopupHintProperty); }
            set { SetValue(PopupHintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupHintProperty =
            DependencyProperty.Register("PopupHint", typeof(string), typeof(MetroTextBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string InputHint
        {
            get { return (string)GetValue(InputHintProperty); }
            set { SetValue(InputHintProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputHintProperty =
            DependencyProperty.Register("InputHint", typeof(string), typeof(MetroTextBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public Brush MouseMoveBackground
        {
            get { return (Brush)GetValue(MouseMoveBackgroundProperty); }
            set { SetValue(MouseMoveBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseMoveBackgroundProperty =
            DependencyProperty.Register("MouseMoveBackground", typeof(Brush), typeof(MetroTextBox), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(MetroTextBox), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public double TitleMinWidth
        {
            get { return (double)GetValue(TitleMinWidthProperty); }
            set { SetValue(TitleMinWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleMinWidthProperty =
            DependencyProperty.Register("TitleMinWidth", typeof(double), typeof(MetroTextBox), new PropertyMetadata(0.0, (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MetroTextBox), new PropertyMetadata(null, (d, e) =>
             {
                 MetroTextBox control = d as MetroTextBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public static RoutedUICommand ButtonClickCommand  = new RoutedUICommand("ButtonClickCommand", "ButtonClickCommand", typeof(MetroTabControl));

        //public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
        //public double TitleMinWidth { get { return (double)GetValue(TitleMinWidthProperty); } set { SetValue(TitleMinWidthProperty, value); } }
        //public Brush TitleForeground { get { return (Brush)GetValue(TitleForegroundProperty); } set { SetValue(TitleForegroundProperty, value); } }
        //public Brush MouseMoveBackground { get { return (Brush)GetValue(MouseMoveBackgroundProperty); } set { SetValue(MouseMoveBackgroundProperty, value); } }
        //public string InputHint { get { return (string)GetValue(InputHintProperty); } set { SetValue(InputHintProperty, value); } }
        //public string PopupHint { get { return (string)GetValue(PopupHintProperty); } set { SetValue(PopupHintProperty, value); } }
        //public string ButtonTitle { get { return (string)GetValue(ButtonTitleProperty); } set { SetValue(ButtonTitleProperty, value); } }
        //public ImageSource Icon { get { return (ImageSource)GetValue(IconProperty); } set { SetValue(IconProperty, value); } }
        //public string State { get { return (string)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }
        //public bool MultipleLine { get { return (bool)GetValue(MultipleLineProperty); } set { SetValue(MultipleLineProperty, value); } }
        //public bool IsPassWordBox { get { return (bool)GetValue(IsPassWordBoxProperty); } set { SetValue(IsPassWordBoxProperty, value); } }
        //public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }



        public Func<string, bool> ErrorCheckAction { get; set; }
        public event EventHandler ButtonClick;

        public MetroTextBox()
        {
            ContextMenu = null;
            Loaded += delegate { ErrorCheck(); };
            TextChanged += delegate { ErrorCheck(); };
            CommandBindings.Add(new CommandBinding(ButtonClickCommand, delegate { if (ButtonClick != null) { ButtonClick(this, null); } }));
            //Utility.Refresh(this);
        }

        void ErrorCheck()
        {
            // if (PopupHint == null || PopupHint == "") { PopupHint = InputHint; }
            if (ErrorCheckAction != null) { State = ErrorCheckAction(Text) ? "true" : "false"; }
        }

        static MetroTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroTextBox), new FrameworkPropertyMetadata(typeof(MetroTextBox)));

        }
    }
}
