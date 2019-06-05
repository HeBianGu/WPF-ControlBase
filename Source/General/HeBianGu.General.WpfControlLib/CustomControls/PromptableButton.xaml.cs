using System;
using System.Collections.Generic;
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

namespace HeBianGu.General.WpfControlLib
{
    public class PromptableButton : Button
    {

        public ImageSource CoverImageSource
        {
            get { return (ImageSource)GetValue(CoverImageSourceProperty); }
            set { SetValue(CoverImageSourceProperty, value); }
        }
        //来_自_5_1_a_s_p__x
        public static readonly DependencyProperty CoverImageSourceProperty =
            DependencyProperty.Register("CoverImageSource", typeof(ImageSource), typeof(PromptableButton), new UIPropertyMetadata(null));


        public string PromptCount
        {
            get { return (string)GetValue(PromptCountProperty); }
            set { SetValue(PromptCountProperty, value); }
        }

        public static readonly DependencyProperty PromptCountProperty =
            DependencyProperty.Register("PromptCount", typeof(string), typeof(PromptableButton),
            new FrameworkPropertyMetadata("0", new PropertyChangedCallback(PromptCountChangedCallBack), new CoerceValueCallback(CoercePromptCountCallback)));


        public PromptableButton()
        {

        }

        static PromptableButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PromptableButton), new FrameworkPropertyMetadata(typeof(PromptableButton)));
        }

        private static object CoercePromptCountCallback(DependencyObject d, object value)
        {
            string promptCount = (string)value;

            int v = int.Parse(promptCount);

            v = Math.Max(0, v);

            return v.ToString();
        }

        public static void PromptCountChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

    }

    public class PromptChrome : Control
    {
        static PromptChrome()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PromptChrome), new FrameworkPropertyMetadata(typeof(PromptChrome)));
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {

            this.Width = 34;
            this.Height = 34;

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            TranslateTransform tt = new TranslateTransform();
            tt.X = 10;
            tt.Y = -10;
            this.RenderTransform = tt;
            //来_自_5_1_a_s_p__x

            return base.ArrangeOverride(arrangeBounds);
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PromptChrome), new PropertyMetadata(default(string), (d, e) =>
             {
                 PromptChrome control = d as PromptChrome;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


    }
}
