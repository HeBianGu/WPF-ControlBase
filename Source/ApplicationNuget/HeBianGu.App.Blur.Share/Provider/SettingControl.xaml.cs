
using HeBianGu.General.WpfControlLib;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// SettingControl.xaml 的交互逻辑
    /// </summary>
    public partial class SettingControl : UserControl
    {
        public SettingControl()
        {
            InitializeComponent();
        }

        private Random random = new Random();
        private async void Button_Click(object sender, RoutedEventArgs e)
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
