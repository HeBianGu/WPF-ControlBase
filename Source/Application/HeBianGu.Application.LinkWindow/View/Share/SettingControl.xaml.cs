
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// SettingControl.xaml 的交互逻辑
    /// </summary>
    partial class SettingControl : UserControl
    {
        public SettingControl()
        {
            InitializeComponent();
        }

        Random random = new Random();
        private async void FButton_Click(object sender, RoutedEventArgs e)
        {
            await Message.Instance.ShowWaittingMessge(() =>
            {
                Thread.Sleep(1000);
            });

            if (random.Next(5) == 1)
            {
                await Message.Instance.ShowSumitMessge("查询错误，请检查！");
            }
            else
            {
                Message.Instance.CloseLayer();
                Message.Instance.ShowSnackMessageWithNotice("查询完成");
            }
        }
    }
}
