using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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

namespace HeBianGu.Application.MainWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }


        public void Method()
        {
            Ping ping = new Ping();

            Task.Run(async () =>
            {
                while (true)
                {
                    var reply = await ping.SendPingAsync("www.baidu.com");

                    if (reply.Status == IPStatus.Success)
                    {
                        Debug.WriteLine(reply.RoundtripTime);
                    }
                    else
                    {
                        Debug.WriteLine("无法连接");
                    }

                    Thread.Sleep(1000);
                }
            });
        }

    }
}
