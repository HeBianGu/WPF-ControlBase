using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.FontEditor
{
    public class FontEditor : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(FontEditor), "S.FontEditor.Default");
        public static ComponentResourceKey ListKey => new ComponentResourceKey(typeof(FontEditor), "S.FontEditor.List");
        public static ComponentResourceKey CardKey => new ComponentResourceKey(typeof(FontEditor), "S.FontEditor.Card");


        static FontEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontEditor), new FrameworkPropertyMetadata(typeof(FontEditor)));
        }

        public FontEditor()
        {
            this.FontSizes = Enumerable.Range(8, 128 - 7).Select(n => (double)n).ToObservable();
            this.FontWeights = typeof(FontWeights).GetProperties().Select(x => (FontWeight)x.GetValue(null)).ToObservable();
            this.FontStretches = typeof(FontStretches).GetProperties().Select(x => (FontStretch)x.GetValue(null)).ToObservable();
            this.FontFamilys = Fonts.SystemFontFamilies.ToObservable();
            this.FontStyles = typeof(FontStyles).GetProperties().Select(x => (FontStyle)x.GetValue(null)).ToObservable();

            this.Loaded += FontEditor_Loaded;
        }

        private void FontEditor_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsBlack = System.Windows.FontWeights.Normal == this.FontWeight;
            this.IsItalic = System.Windows.FontStyles.Italic == this.FontStyle;
        }

        public Brush FontForeground
        {
            get { return (Brush)GetValue(FontForegroundProperty); }
            set { SetValue(FontForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontForegroundProperty =
            DependencyProperty.Register("FontForeground", typeof(Brush), typeof(FontEditor), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        public bool IsBlack
        {
            get { return (bool)GetValue(IsBlackProperty); }
            set { SetValue(IsBlackProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBlackProperty =
            DependencyProperty.Register("IsBlack", typeof(bool), typeof(FontEditor), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.FontWeight=n? System.Windows.FontWeights.Bold:System.Windows.FontWeights.Normal;   
                 }

             }));


        public bool IsUnderLine
        {
            get { return (bool)GetValue(IsUnderLineProperty); }
            set { SetValue(IsUnderLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUnderLineProperty =
            DependencyProperty.Register("IsUnderLine", typeof(bool), typeof(FontEditor), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.FontWeight = n ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
                 }

             }));


        public bool IsItalic
        {
            get { return (bool)GetValue(IsItalicProperty); }
            set { SetValue(IsItalicProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsItalicProperty =
            DependencyProperty.Register("IsItalic", typeof(bool), typeof(FontEditor), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     control.FontStyle = n ? System.Windows.FontStyles.Italic : System.Windows.FontStyles.Normal;
                 }
             }));


        public Brush FontBackground
        {
            get { return (Brush)GetValue(FontBackgroundProperty); }
            set { SetValue(FontBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontBackgroundProperty =
            DependencyProperty.Register("FontBackground", typeof(Brush), typeof(FontEditor), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));



        public ObservableCollection<FontFamily> FontFamilys
        {
            get { return (ObservableCollection<FontFamily>)GetValue(FontFamilysProperty); }
            set { SetValue(FontFamilysProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontFamilysProperty =
            DependencyProperty.Register("FontFamilys", typeof(ObservableCollection<FontFamily>), typeof(FontEditor), new FrameworkPropertyMetadata(default(ObservableCollection<FontFamily>), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<FontFamily> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<FontFamily> n)
                 {

                 }

             }));



        public ObservableCollection<FontStyle> FontStyles
        {
            get { return (ObservableCollection<FontStyle>)GetValue(FontStylesProperty); }
            set { SetValue(FontStylesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontStylesProperty =
            DependencyProperty.Register("FontStyles", typeof(ObservableCollection<FontStyle>), typeof(FontEditor), new FrameworkPropertyMetadata(default(ObservableCollection<FontStyle>), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<FontStyle> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<FontStyle> n)
                 {

                 }

             }));


        public ObservableCollection<FontStretch> FontStretches
        {
            get { return (ObservableCollection<FontStretch>)GetValue(FontStretchesProperty); }
            set { SetValue(FontStretchesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontStretchesProperty =
            DependencyProperty.Register("FontStretches", typeof(ObservableCollection<FontStretch>), typeof(FontEditor), new FrameworkPropertyMetadata(default(ObservableCollection<FontStretch>), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<FontStretch> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<FontStretch> n)
                 {

                 }

             }));



        public ObservableCollection<FontWeight> FontWeights
        {
            get { return (ObservableCollection<FontWeight>)GetValue(FontWeightsProperty); }
            set { SetValue(FontWeightsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontWeightsProperty =
            DependencyProperty.Register("FontWeights", typeof(ObservableCollection<FontWeight>), typeof(FontEditor), new FrameworkPropertyMetadata(default(ObservableCollection<FontWeight>), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<FontWeight> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<FontWeight> n)
                 {

                 }

             }));

        public ObservableCollection<double> FontSizes
        {
            get { return (ObservableCollection<double>)GetValue(FontSizesProperty); }
            set { SetValue(FontSizesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizesProperty =
            DependencyProperty.Register("FontSizes", typeof(ObservableCollection<double>), typeof(FontEditor), new FrameworkPropertyMetadata(default(ObservableCollection<double>), (d, e) =>
             {
                 FontEditor control = d as FontEditor;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<double> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<double> n)
                 {

                 }
             }));
    }
}
