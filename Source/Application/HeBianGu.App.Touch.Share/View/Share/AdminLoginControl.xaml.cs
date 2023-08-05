using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using System.Windows.Controls;

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

            Loaded += (l, k) =>
              {
                  pb_password.Focus();
              };

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pb_password.Password == "111111")
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("密码不正确，请输入111111！");

                pb_password.Focus();
            }


        }
    }
}
