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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            XmlTextReader reader = new XmlTextReader(new StringReader(tb.Text));
            boder.Child = (UIElement)XamlReader.Load(reader);
        }
    }
}
