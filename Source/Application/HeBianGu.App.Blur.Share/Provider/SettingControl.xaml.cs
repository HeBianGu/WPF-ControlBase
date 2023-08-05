
using HeBianGu.Base.WpfBase;
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
            await Messager.Instance.ShowWaitter(() =>
            {
                Thread.Sleep(1000);
            });

            if (random.Next(5) == 1)
            {
                await Messager.Instance.ShowSumit("查询错误，请检查！");
            }
            else
            {
                MessageProxy.Container.Close();
                MessageProxy.Snacker.ShowTime("查询完成");
            }
        }
    }
}
