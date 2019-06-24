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
    /// ButtonControl.xaml 的交互逻辑
    /// </summary>
    public partial class ButtonControl : UserControl
    {
        public ButtonControl()
        {
            InitializeComponent();
        }

        Random random = new Random();
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           await MessageService.ShowWaittingMessge(() =>
            {
                Thread.Sleep(2000);
            });


            if (random.Next(2) == 1)
            {
                await MessageService.ShowSumitMessge("查询错误，请检查！");
            }
            else
            {
                MessageService.ShowSnackMessageWithNotice("查询完成");
            }
        }
    }
}
