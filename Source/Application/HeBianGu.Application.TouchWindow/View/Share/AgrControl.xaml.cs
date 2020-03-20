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
using HeBianGu.General.WpfControlLib;

namespace HeBianGu.Application.TouchWindow.View.Share
{
    /// <summary>
    /// AgrControl.xaml 的交互逻辑
    /// </summary>
    public partial class AgrControl : UserControl
    {
        public AgrControl()
        {
            InitializeComponent();

            this.Loaded +=(l, k) =>
             {
                 this.txt_value.Focus();
             };

        }

        /// <summary> 1 男 2 女 </summary>
        public int GetResult()
        {
            return Convert.ToInt32(this.txt_value.Text);
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {

            bool check = int.TryParse(this.txt_value.Text, out int value);

            if(check)
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                MessageService.ShowSnackMessageWithNotice("请输入有效年龄数据");
                this.txt_value.Focus();
            }

      
        }
    }
}
