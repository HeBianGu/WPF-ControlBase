using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace HeBianGu.Applications.ControlBase.LinkWindow.View.Loyout
{
    /// <summary>
    /// XamlControl.xaml 的交互逻辑
    /// </summary>
    public partial class XamlControl : UserControl
    {
        public XamlControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XmlTextReader reader = new XmlTextReader(new StringReader(tb.Text));
            boder.Child = (UIElement)XamlReader.Load(reader);
        }
    }
}
