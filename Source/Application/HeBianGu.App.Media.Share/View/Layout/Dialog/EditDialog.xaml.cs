using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Media.View.Layout.Dialog
{
    /// <summary>
    /// Interaction logic for EditDialog.xaml
    /// </summary>
    public partial class EditDialog : UserControl
    {
        public EditDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageProxy.Container.Close();
            MessageProxy.Snacker.ShowTime("查询完成");
        }
    }
}
