using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Applications.ControlBase.LinkWindow.View.Loyout
{
    /// <summary>
    /// BookControl.xaml 的交互逻辑
    /// </summary>
    public partial class BookControl : UserControl
    {
        public BookControl()
        {
            InitializeComponent();
        }




        private void AutoNextClick(object sender, RoutedEventArgs e)
        {
            myBook.AnimateToNextPage(cbFromTop.SelectedIndex == 0, 700);
            myBook.Focus();
        }

        private void AutoPreviousClick(object sender, RoutedEventArgs e)
        {
            myBook.AnimateToPreviousPage(cbFromTop.SelectedIndex == 0, 700);
            myBook.Focus();
        }
    }
}
