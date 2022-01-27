using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Touch.View.Share
{
    /// <summary>
    /// AgrControl.xaml 的交互逻辑
    /// </summary>
    public partial class AgrControl : UserControl
    {
        public AgrControl()
        {
            InitializeComponent();

            this.Loaded += (l, k) =>
              {
                  this.txt_value.Focus();
              };

        }

        /// <summary> 1 男 2 女 </summary>
        public int GetResult()
        {
            return Convert.ToInt32(this.txt_value.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool check = int.TryParse(this.txt_value.Text, out int value);

            if (check)
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("请输入有效年龄数据");
                this.txt_value.Focus();
            }


        }
    }
}
