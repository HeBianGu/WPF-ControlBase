using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.App.Disk
{
    /// <summary>
    /// ShareControl.xaml 的交互逻辑
    /// </summary>
    public partial class ShareControl : UserControl
    {
        public ShareControl()
        {
            InitializeComponent();
        }

        private void Explorer_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
