using HeBianGu.Application.LinkWindow;
using HeBianGu.Base.WpfBase;
using System.Windows.Controls;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// TreeListControl.xaml 的交互逻辑
    /// </summary>
    public partial class TreeListControl : UserControl
    {
        public TreeListControl()
        {
            InitializeComponent();
        }

        private void Txt_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiceRegistry.Instance.GetInstance<TreeListViewModel>().RefreshFilter(((TextBox)sender).Text);
        }
    }

}
