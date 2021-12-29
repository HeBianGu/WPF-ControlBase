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
using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;

namespace HeBianGu.App.Touch.View.Share
{
    /// <summary>
    /// AdminLoginControl.xaml 的交互逻辑
    /// </summary>
    public partial class AdminLoginControl : UserControl
    {
        public AdminLoginControl()
        {
            InitializeComponent();

            this.Loaded += (l, k) =>
              {
                  this.pb_password.Focus();
              };

        }
        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.pb_password.Password == "111111")
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("密码不正确，请输入111111！");

                this.pb_password.Focus();
            }


        }
    }
}
