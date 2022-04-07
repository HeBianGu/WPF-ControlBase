using System.Windows.Controls;

namespace HeBianGu.App.Touch.View.Share
{
    /// <summary>
    /// SexControl.xaml 的交互逻辑
    /// </summary>
    public partial class SexControl : UserControl
    {
        public SexControl()
        {
            InitializeComponent();
        }

        /// <summary> 1 男 2 女 </summary>
        public int GetResult()
        {
            return r_man.IsChecked.Value ? 1 : 2;
        }
    }
}
