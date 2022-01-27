using HeBianGu.Control.Message;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Touch.View.Share
{
    /// <summary>
    /// AgrControl.xaml 的交互逻辑
    /// </summary>
    public partial class CardIDControl : UserControl
    {
        public CardIDControl()
        {
            InitializeComponent();

            this.Loaded += (l, k) =>
              {
                  this.txt_value.Focus();
              };

        }

        /// <summary> 获取身份证号 </summary>
        public string GetResult()
        {
            return this.txt_value.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool check = AssemblyDomain.Instance.IsCardID(this.txt_value.Text);

            if (check)
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                Message.Instance.ShowSnackMessageWithNotice("身份证号不合法，情检查！");
                this.txt_value.Focus();
            }


        }
    }
}
