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
