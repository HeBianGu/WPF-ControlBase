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

namespace HeBianGu.Applications.ControlBase.LinkWindow.View.Switch
{
    /// <summary>
    /// CenterControl.xaml 的交互逻辑
    /// </summary>
    public partial class CenterControl : UserControl
    {
        public CenterControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = new Button();
            button.Content = "第一個";
            button.Width = 1000;
            button.Height = 1000;
            button.Background = Brushes.Red; 
            transitioner.OldContent = button;

            button = new Button();
            button.Content = "第二個";
            button.Background = Brushes.Blue;
            button.Width = 1000;
            button.Height = 1000;
            transitioner.NewContent = button;

            transitioner.RefreshSwitch();
        }
    }
}
