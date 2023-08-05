using HeBianGu.Base.WpfBase;
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

            Loaded += (l, k) =>
              {
                  txt_value.Focus();
              };

        }

        /// <summary> 1 男 2 女 </summary>
        public int GetResult()
        {
            return Convert.ToInt32(txt_value.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool check = int.TryParse(txt_value.Text, out int value);

            if (check)
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("请输入有效年龄数据");
                txt_value.Focus();
            }


        }
    }
}
