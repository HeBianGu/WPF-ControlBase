using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// KeyBoardControl.xaml 的交互逻辑
    /// </summary>
    public partial class KeyBoardControl : UserControl
    {
        public KeyBoardControl()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button == null) return;

            Debug.WriteLine(button.Content);

            string content = button.Content.ToString();

            string tag = button.Tag.ToString();

            byte b = Convert.ToByte(tag);

            KeyHelper.OnKeyPress(b);
        }
    }
}
