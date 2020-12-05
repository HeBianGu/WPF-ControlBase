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

namespace HeBianGu.Application.ManagerWindow.View.Panel
{
    /// <summary>
    /// ContainControl.xaml 的交互逻辑
    /// </summary>
    public partial class ContainControl : UserControl
    {
        public ContainControl()
        {
            InitializeComponent();
        }

        private readonly Random _rng = new Random();

        private void Button_CP_Click(object sender, RoutedEventArgs e)
        {
            ContentControl content = new ContentControl();
            content.Width = _rng.Next(100, (int)ActualWidth);
            content.Height = _rng.Next(100, (int)ActualHeight);

            content.Content = new ContainPanelSample();

            this.cp.Add(content);
        }

        private void Button_CP_Click_Close(object sender, RoutedEventArgs e)
        {
            this.cp.Remove();
        }

    }

    class ContainPanelSample
    {

    }
}
