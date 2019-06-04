using System;
using System.Collections.Generic;
using System.IO;
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

namespace HeBianGu.General.WpfControlLib.Controls
{
    /// <summary>
    /// BindRichTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class BindRichTextBox : UserControl
    {
        public BindRichTextBox()
        {
            InitializeComponent();
        }

        private void rtb_all_TextChanged(object sender, TextChangedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in this.rtb_all.Document.Blocks.OfType<Paragraph>().SelectMany(b => b.Inlines))
            {
                Run r = c as Run;
                if (r == null) continue;
                System.Diagnostics.Debug.WriteLine(r.Text);
                sb.AppendLine(r.Text);
            }


            if (enbleBindTextChangeflag1)
            {
                enbleTextChangeflag = false;
                this.BindText = sb.ToString();
                enbleTextChangeflag = true;
            }
        }

        /// <summary> 绑定的字符串 </summary>
        public string BindText
        {
            get { return (string)GetValue(BindTextProperty); }
            set { SetValue(BindTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindTextProperty =
            DependencyProperty.Register("BindText", typeof(string), typeof(BindRichTextBox), new PropertyMetadata(null, BindTextChangedCallback));


        // Todo ：互斥 不可同时执行 
        bool enbleTextChangeflag = true;

        // Todo ：互斥 不可同时执行 
        bool enbleBindTextChangeflag1 = true;

        public static void BindTextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var richTextBox = (BindRichTextBox)d;

            if (richTextBox.enbleTextChangeflag)
            {
                richTextBox.enbleBindTextChangeflag1 = false;

                var xaml = e.NewValue == null ? string.Empty : e.NewValue.ToString();

                var doc = new FlowDocument();

                doc.LineHeight = 2;

                var range = new TextRange(doc.ContentStart, doc.ContentEnd);

                range.Load(new MemoryStream(Encoding.UTF8.GetBytes(xaml)), DataFormats.Text);

                richTextBox.rtb_all.Document = doc;

                richTextBox.enbleBindTextChangeflag1 = true;
            }

        }

    }
}
