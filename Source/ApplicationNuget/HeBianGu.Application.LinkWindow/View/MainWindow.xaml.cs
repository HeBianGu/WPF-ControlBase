using HeBianGu.Window.Link;
using System.Windows;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : LinkWindowBase
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
