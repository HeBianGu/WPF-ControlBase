using HeBianGu.Base.WpfBase;
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

            Loaded += (l, k) =>
              {
                  txt_value.Focus();
              };

        }

        /// <summary> 获取身份证号 </summary>
        public string GetResult()
        {
            return txt_value.Text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool check = AssemblyDomain.Instance.IsCardID(txt_value.Text);

            if (check)
            {
                DialogHost.CloseDialogCommand.Execute(true, this);
            }
            else
            {
                MessageProxy.Snacker.ShowTime("身份证号不合法，情检查！");
                txt_value.Focus();
            }


        }
    }
}
