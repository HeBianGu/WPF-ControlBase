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
            return this.r_man.IsChecked.Value ? 1 : 2;
        }
    }
}
