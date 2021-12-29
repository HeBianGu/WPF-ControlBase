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
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Application.TouchWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow
    {
        public ShellWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
#if DEBUG

            this.WindowState = WindowState.Normal;
#endif

            this.Loaded += ShellWindow_Loaded;
        }

        private void ShellWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Delay(3000).ContinueWith(l=>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Background = System.Windows.Application.Current.FindResource(BrushKeys.BackgroundDefault) as Brush;
                });

            });
        }

        private void LinkWindowBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
           try
            {
                this.DragMove();
            }
            catch
            {

            }
        }
    }
}
